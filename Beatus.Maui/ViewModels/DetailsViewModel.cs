using Beatus.Maui.Models;
using Beatus.Maui.Services;
using Beatus.Maui.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Beatus.Maui.ViewModels;

[QueryProperty(nameof(PredictionDetails), "PredictionDetails")]
public partial class DetailsViewModel : ObservableObject
{
    private PredictionDetails predictionDetails;
    private readonly OpenAiService _openAiService;
    private readonly IDataService _dataService;

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

    public DetailsViewModel(OpenAiService openAiService, IDataService dataService)
    {
        _openAiService = openAiService;
        _dataService = dataService;
    }

    public ImageSource PlantImage => ImageSource.FromStream(() => new MemoryStream(PredictionDetails.PlantImage));

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

        await _dataService.SavePrediction(entity);
    }
}
