using Beatus.Maui.Models;
using Beatus.Maui.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Beatus.Maui.ViewModels;

public class SavedViewModel
{
    private readonly IDataService _dataService;
    public ObservableCollection<PredictionDetailsEntity> SavedPredictions { get; set; } = new ObservableCollection<PredictionDetailsEntity>();

    public SavedViewModel(IDataService dataService)
    {
        _dataService = dataService;
        LoadSavedPredictions().Wait();
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

}
