using Beatus.Maui.Models;
using Beatus.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Beatus.Maui.ViewModels;

[QueryProperty(nameof(PredictionDetails), "PredictionDetails")]
public partial class DetailsViewModel : ObservableObject
{
    private PredictionDetails predictionDetails;
    private readonly OpenAiService _openAiService;

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

    private string recommendation;

    public string Recommendation
    {
        get { return recommendation; }
        set
        {
            recommendation = value;
            OnPropertyChanged(nameof(Recommendation));
        }
    }


    public DetailsViewModel(OpenAiService openAiService)
    {
        _openAiService = openAiService;

        GetRecommendation();
    }

    public ImageSource PlantImage => ImageSource.FromStream(() => new MemoryStream(PredictionDetails.PlantImage));

    [RelayCommand]
    public void PreviousPage() => Shell.Current.GoToAsync("..");
    
    private async Task GetRecommendation() => Recommendation = await _openAiService.GetPlantTips(PredictionDetails.TagName);
}
