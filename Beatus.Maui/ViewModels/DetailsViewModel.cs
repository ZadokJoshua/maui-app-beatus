using Beatus.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Beatus.Maui.ViewModels;

[QueryProperty(nameof(PredictionDetails), "PredictionDetails")]
public partial class DetailsViewModel : ObservableObject
{
    private PredictionDetails predictionDetails;

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


    public ImageSource PlantImage => ImageSource.FromStream(() => new MemoryStream(PredictionDetails.PlantImage));

    [RelayCommand]
    public void PreviousPage() => Shell.Current.GoToAsync("..");
}
