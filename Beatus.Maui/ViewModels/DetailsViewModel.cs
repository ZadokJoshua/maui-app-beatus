using Beatus.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Beatus.Maui.ViewModels;

[QueryProperty(nameof(PredictionDetails), "PredictionDetails")]
public partial class DetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private PredictionDetails predictionDetails;
}
