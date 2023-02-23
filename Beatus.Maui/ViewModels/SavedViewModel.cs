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
    
    
    public ObservableCollection<PredictionDetailsEntity> SavedPredictions { get; private set; }

    public SavedViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SavedPredictions = new ObservableCollection<PredictionDetailsEntity>();
    }

    public async Task LoadSavedPredictions()
    {
        var predictions = await _dataService.GetAllSavedPredictionsAsync();
        SavedPredictions.Clear();
        foreach (var prediction in predictions)
        {
            SavedPredictions.Add(prediction);
        }
    }

    [RelayCommand]
    public async Task ViewPredictionDetails()
    {
        IsBusy = true;
        if (SelectedPrediction != null)
        {
            PredictionDetails prediction = new()
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
                }
            });
        }
        IsBusy = false;
    }
}
