#nullable enable

using Beatus.Models;
using Beatus.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace Beatus.ViewModels;

public partial class SavedViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    public SavedViewModel(IDataService dataService)
    {
        _dataService = dataService;
        LoadSavedPredictions();
    }

    [ObservableProperty]
    private PredictionDetailsEntity? selectedPrediction;
    
    [ObservableProperty]
    private bool isPredictionEmpty;

    public ObservableRangeCollection<PredictionDetailsEntity> SavedPredictions { get; set; } = new();

    public void LoadSavedPredictions()
    {
        SavedPredictions.Clear();
        IsBusy = true;

        Task.Run(async () =>
        {
            try
            {
                var predictions = await _dataService.GetAllSavedPredictionsAsync();
                
                Shell.Current.Dispatcher.Dispatch(() =>
                {
                    if (predictions != null && predictions.Any())
                    {
                        SavedPredictions.ReplaceRange(predictions);
                        IsPredictionEmpty = false;
                    }
                    else
                    {
                        IsPredictionEmpty = true;
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
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
