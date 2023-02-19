using Beatus.Maui.Models;
using Beatus.Maui.Services;
using Beatus.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SkiaSharp;

namespace Beatus.Maui.ViewModels;

public partial class MainViewModel : ObservableObject
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
    public async Task ClassifySelectedPhotoAsync()
    {
        if (ImageSelected)
        {
            var resizedPhoto = await ResizeImage(SelectedPhoto);
            var customVisionAIResponse = await _customVisionAI.MakePredictionAsync(resizedPhoto);
            if (customVisionAIResponse is not null)
            {
                // The Null error was coming from here because the response was null and the TagName was null as well. So I have
                // commented out the line below and will add a new line to get the response from OpenAI in the DetailsViewModel. 
                //var openAiResponse = await _openAi.GetPlantTips(customVisionAIResponse.TagName);
                PredictionDetails details = new() { PlantImage = resizedPhoto, TagName = customVisionAIResponse.TagName, Probability = (int)(customVisionAIResponse.Probability * 100), Recommendation = "openAiResponse" };
                await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
            {
                {
                    "PredictionDetails", details
                }
            });
            }
        }
    }


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
                        // Calculate the aspect ratio of the original image
                        var aspectRatio = (float)original.Width / original.Height;

                        // Calculate the new width and height of the image
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
