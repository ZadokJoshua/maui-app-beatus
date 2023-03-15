using Beatus.Models;
using Beatus.Services;
using Beatus.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Beatus.ViewModels;

[QueryProperty(nameof(PredictionDetails), "PredictionDetails")]
[QueryProperty(nameof(IsOpenedFromMainPage), "IsOpenedFromMainPage")]
[QueryProperty(nameof(EntityId), "EntityId")]
public partial class DetailsViewModel : BaseViewModel
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

    [ObservableProperty]
    private int entityId;

    public DetailsViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }


    [RelayCommand]
    private void PreviousPage() => Shell.Current.GoToAsync("..");

    [RelayCommand]
    private async Task SavePrediction()
    {
        PredictionDetailsEntity entity = new()
        {
            PlantImage = PredictionDetails.PlantImage,
            TagName = PredictionDetails.TagName,
            Probability = PredictionDetails.Probability,
            Recommendation = PredictionDetails.Recommendation
        };

        try
        {
            await _dataService.SavePrediction(entity);
            await Shell.Current.DisplayAlert("Success", "Prediction added to database.", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
    
    [RelayCommand]
    private async Task DeletePrediction()
    {
        bool deletePrediction = await Shell.Current.DisplayAlert("Delete", "Do you want to delete this prediction?", "Yes", "No");

        if (deletePrediction)
        {
            try
            {
                var entity = await _dataService.GetSavedPrediction(EntityId);
                await _dataService.DeletePrediction(entity);

                PreviousPage();
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong", "OK");    
            }
        }
    }
}
