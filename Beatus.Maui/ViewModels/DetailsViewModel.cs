using Beatus.Maui.Models;
using Beatus.Maui.Services;
using Beatus.Maui.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Beatus.Maui.ViewModels;

[QueryProperty(nameof(PredictionDetails), "PredictionDetails")]
[QueryProperty(nameof(IsOpenedFromMainPage), "IsOpenedFromMainPage")]
public partial class DetailsViewModel : ObservableObject
{
    private PredictionDetails predictionDetails;
    private readonly IDataService _dataService;
    public ImageSource PlantImage => ImageSource.FromStream(() => new MemoryStream(PredictionDetails.PlantImage));

    public PredictionDetails PredictionDetails
    {
        get { return predictionDetails; }
        set
        {
            predictionDetails = value;
            OnPropertyChanged(nameof(PredictionDetails));
            OnPropertyChanged(nameof(PlantImage));
        }
    }

    [ObservableProperty]
    private bool isOpenedFromMainPage;

    public DetailsViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }


    [RelayCommand]
    public void PreviousPage() => Shell.Current.GoToAsync("..");

    [RelayCommand]
    public async Task SavePrediction()
    {
        PredictionDetailsEntity entity = new PredictionDetailsEntity()
        {
            PlantImage = PredictionDetails.PlantImage,
            TagName = PredictionDetails.TagName,
            Probability = PredictionDetails.Probability,
            Recommendation = PredictionDetails.Recommendation
        };

        await Shell.Current.DisplayAlert("Success", "Prediction added to database.", "OK");

        try
        {
            await _dataService.SavePrediction(entity);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
