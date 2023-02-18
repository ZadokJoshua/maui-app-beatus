using Beatus.Maui.Models;
using Beatus.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SkiaSharp;

namespace Beatus.Maui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel(IConfiguration config, OpenAiService openAi)
    {
        _config = config;
        _openAi = openAi;
    }

    private const int ImageMaxSizeBytes = 4194304;
    private const int ImageMaxResolution = 1024;

    private readonly IConfiguration _config;
    private readonly OpenAiService _openAi;
    [ObservableProperty] 
    private ImageSource photo;

    [ObservableProperty]
    private bool imageSelected;

    [ObservableProperty]
    private FileResult selectedPhoto;


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
        var resizedPhoto = await ResizeImage(SelectedPhoto);
        var result = await MakePredictionAsync(resizedPhoto);
        var openAiResponse = _openAi.GetPlantTips(result.TagName);
        //await Shell.Current.GoToAsync(nameof(DetailsPage));
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

    private async Task<Prediction> MakePredictionAsync(byte[] imageBytes)
    {
        if (ImageSelected)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Prediction-Key", _config["CustomVision:Key"]);

            using var content = new ByteArrayContent(imageBytes);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            var response = await client.PostAsync(_config["CustomVision:EndPoint"], content);

            var responseString = await response.Content.ReadAsStringAsync();
            Prediction prediction = (JsonConvert.DeserializeObject<CustomVisionResponse>(responseString)).Predictions?.OrderByDescending(x => x.Probability).FirstOrDefault();

            return prediction;
        }
        else
        {
            return null;
        }
    }
}
