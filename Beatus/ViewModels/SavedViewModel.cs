#nullable enable

using Beatus.Models;
using Beatus.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace Beatus.ViewModels;

public partial class SavedViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private PredictionDetailsEntity? selectedPrediction;
    [ObservableProperty]
    private bool isBusy;
    [ObservableProperty]
    private bool isPredictionEmpty;

    public ObservableRangeCollection<PredictionDetailsEntity>? SavedPredictions { get; set; } = new();

    public SavedViewModel(IDataService dataService)
    {
        _dataService = dataService;

        LoadSavedPredictions();
    }

    public void LoadSavedPredictions()
    {
        SavedPredictions?.Clear();
        IsBusy = true;

        Task.Run(async () =>
        {
            var predictions = await _dataService.GetAllSavedPredictionsAsync();

            Shell.Current.Dispatcher.Dispatch(() =>
            {
                SavedPredictions?.ReplaceRange(predictions);

                if (SavedPredictions?.Count == 0)
                {
                    IsPredictionEmpty = true;
                    IsBusy = false;
                }
                else
                {
                    IsPredictionEmpty = false;
                    IsBusy = false;
                }

            });
        });
        
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

        
            await Shell.Current.GoToAsync("DetailsPage", new Dictionary<string, object>
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
