using Beatus.Models;
using Beatus.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkiaSharp;

namespace Beatus.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private const int ImageMaxSizeBytes = 4194304;
    private const int ImageMaxResolution = 1024;

    private readonly OpenAiService _openAi;
    private readonly CustomVisionAIService _customVisionAI;
    
    public MainViewModel(OpenAiService openAi, CustomVisionAIService customVisionAI)
    {
        _openAi = openAi;
        _customVisionAI = customVisionAI;
    }

    private ImageSource photo;

    public ImageSource Photo
    {
        get { return photo; }
        set
        {
            photo = value;
            OnPropertyChanged(nameof(Photo));
        }
    }


    private bool imageSelected;

    public bool ImageSelected
    {
        get { return imageSelected; }
        set
        {
            imageSelected = value;
            OnPropertyChanged(nameof(ImageSelected));
        }
    }


    private FileResult selectedPhoto;

    public FileResult SelectedPhoto
    {
        get { return selectedPhoto; }
        set
        {
            selectedPhoto = value;
            OnPropertyChanged(nameof(SelectedPhoto));
        }
    }

    [RelayCommand]
    private Task ExecutePickPhoto() => SelectPhotoAsync(false);

    [RelayCommand]
    private Task ExecuteTakePhoto() => SelectPhotoAsync(true);

    public async Task SelectPhotoAsync(bool useCamera)
    {
        SelectedPhoto = useCamera ? await MediaPicker.Default.CapturePhotoAsync()
            : await MediaPicker.Default.PickPhotoAsync();

        if (SelectedPhoto != null)
        {
            Photo = SelectedPhoto.FullPath;
            ImageSelected = true;
        }
    }

    
    [RelayCommand]
    private async Task MakePredictionAsync()
    {
        IsBusy = true;
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        
        try
        {
            if (ImageSelected)
            {
                if (accessType is NetworkAccess.None or NetworkAccess.Unknown)
                {
                    await Shell.Current.DisplayAlert("No Internet Connection", "Please check your Internet connection", "OK");
                    return; 
                }

                var resizedPhoto = await ResizeImage(SelectedPhoto);
                var customVisionAIResponse = await _customVisionAI.MakePredictionAsync(resizedPhoto);

                if (customVisionAIResponse is not null)
                {
                    if (customVisionAIResponse?.Probability < 0.7 || customVisionAIResponse?.TagName?.ToLower() == "negative")
                    {
                        await Shell.Current.DisplayAlert("No Plant Detected", "Please try again with a different image", "OK");
                        return;
                    }

                    var openAiResponse = await _openAi.GetPlantTips(customVisionAIResponse.TagName);
                    var details = new PredictionDetails
                    {
                        PlantImage = resizedPhoto,
                        TagName = customVisionAIResponse.TagName,
                        Probability = (int)(customVisionAIResponse.Probability * 100),
                        Recommendation = openAiResponse
                    };

                    await Shell.Current.GoToAsync("DetailsPage", new Dictionary<string, object>
                {
                    { nameof(PredictionDetails), details },
                    { "IsOpenedFromMainPage", true }
                });
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("No Image Selected", "Please select or capture an image", "OK");
            }
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "An error occurred.", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }


    [RelayCommand]
    public void Cancel()
    {
        Photo = null;
        SelectedPhoto = null;
        ImageSelected = false;
    }

    /// <summary>
    /// Resize selected image because Custom Vision AI only accepts images of 4MB or less and 1024x1024 pixels 
    /// </summary>
    /// <param name="photo"></param>
    /// <returns>Byte Array</returns>
    private async Task<byte[]> ResizeImage(FileResult photo)
    {
        byte[] result = null;

        using (var stream = await photo.OpenReadAsync())
        {
            if (stream.Length > ImageMaxSizeBytes)
            {
                using (var skiaStream = new SKManagedStream(stream))
                {
                    using (var original = SKBitmap.Decode(skiaStream))
                    {
                        var aspectRatio = (float)original.Width / original.Height;

                        var newWidth = ImageMaxResolution;
                        var newHeight = ImageMaxResolution / aspectRatio;

                        using (var resized = original.Resize(new SKImageInfo(newWidth, (int)newHeight), SKFilterQuality.High))
                        {
                            using (var image = SKImage.FromBitmap(resized))
                            {
                                using (var imageData = image.Encode(SKEncodedImageFormat.Jpeg, 100))
                                {
                                    result = imageData.ToArray();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    result = binaryReader.ReadBytes((int)stream.Length);
                }
            }
        }
        return result;
    }
}
