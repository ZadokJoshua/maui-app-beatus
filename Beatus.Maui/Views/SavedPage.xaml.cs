using Beatus.Maui.ViewModels;

namespace Beatus.Maui.Views;

public partial class SavedPage : ContentPage
{
    private readonly SavedViewModel _savedViewModel;

    public SavedPage(SavedViewModel savedViewModel)
	{
		InitializeComponent();

        _savedViewModel = savedViewModel;
        BindingContext = savedViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _savedViewModel.LoadSavedPredictions();
    }
}