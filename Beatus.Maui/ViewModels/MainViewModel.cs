using Beatus.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using SkiaSharp;

namespace Beatus.Maui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private const int ImageMaxSizeBytes = 4194304;
    private const int ImageMaxResolution = 1024;

    [ObservableProperty] 
    private ImageSource photo;

    [ObservableProperty]
    private bool imageSelected;

    [RelayCommand]
    private Task ExecutePickPhoto() => ProcessSelectedPhotoAsync(false);

    [RelayCommand]
    private Task ExecuteTakePhoto() => ProcessSelectedPhotoAsync(true);

    public async Task ProcessSelectedPhotoAsync(bool useCamera)
    {
        var selectedPhoto = useCamera ? await MediaPicker.Default.CapturePhotoAsync() 
            : await MediaPicker.Default.PickPhotoAsync();

        if (selectedPhoto != null)
        {
            var resizedPhoto = await ResizeImage(selectedPhoto);
            Photo = ImageSource.FromStream(() => new MemoryStream(resizedPhoto));
            ImageSelected = true;
        }

        
        //await Shell.Current.GoToAsync(nameof(DetailsPage));
    }

    //private async Task<byte[]> ResizePhoto(FileResult photo)
    //{
    //    byte[] result = null;

    //    using (var stream = await photo.OpenReadAsync())
    //    {
    //        if (stream.Length > ImageMaxSizeBytes)
    //        {
    //            var image = PlatformImage.FromStream(stream);
    //            if (image != null)
    //            {
    //                var newImage = image.Downsize(ImageMaxResolution, true);
    //                result = newImage.AsBytes();
    //            }
    //        }
    //        else
    //        {
    //            using (var binaryReader = new BinaryReader(stream))
    //            {
    //                result = binaryReader.ReadBytes((int)stream.Length);
    //            }
    //        }
    //    }

    //    return result;
    //}

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
