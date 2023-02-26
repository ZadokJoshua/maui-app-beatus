#nullable enable

using Beatus.Maui.Models;
using Beatus.Maui.Services.Interfaces;
using Beatus.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Beatus.Maui.ViewModels;

public partial class SavedViewModel : ObservableObject
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private PredictionDetailsEntity selectedPrediction;
    [ObservableProperty]
    private bool isBusy;
    [ObservableProperty]
    private bool isPredictionEmpty;
    [ObservableProperty]
    private ObservableCollection<PredictionDetailsEntity>? savedPredictions;

    public SavedViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SavedPredictions = new ObservableCollection<PredictionDetailsEntity>();
    }

    public async Task LoadSavedPredictions()
    {
        var predictions = await _dataService.GetAllSavedPredictionsAsync();
        
        if (!predictions.Any())
        {
            IsPredictionEmpty = true;
            return;
        }
        
        SavedPredictions?.Clear();
        
        foreach (var prediction in predictions)
        {
            SavedPredictions?.Add(prediction);
        }
    }

    [RelayCommand]
    async Task ViewPredictionDetails()
    {
        IsBusy = true;
        
        if (SelectedPrediction != null)
        {
            PredictionDetails? prediction = new()
            {
                PlantImage = SelectedPrediction.PlantImage,
                TagName = SelectedPrediction.TagName,
                Probability = SelectedPrediction.Probability,
                Recommendation = SelectedPrediction.Recommendation
            };

        
            await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
            {
                {
                    "PredictionDetails", prediction
                },
                {
                    "IsOpenedFromMainPage", false
                },
                {
                    "EntityId", SelectedPrediction.Id
                }
            }); 
        }

        IsBusy = false;
    }
}
