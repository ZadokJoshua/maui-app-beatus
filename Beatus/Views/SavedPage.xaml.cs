using Beatus.ViewModels;

namespace Beatus.Views;

public partial class SavedPage : ContentPage
{
    private readonly SavedViewModel _savedViewModel;

    public SavedPage(SavedViewModel savedViewModel)
	{
		InitializeComponent();

        _savedViewModel = savedViewModel;
        BindingContext = savedViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        _savedViewModel.IsBusy = true;
        await _savedViewModel.LoadSavedPredictions();
        _savedViewModel.IsBusy = false;
    }
}