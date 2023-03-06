using Beatus.ViewModels;
using System.Diagnostics;

namespace Beatus.Views;

public partial class SavedPage : ContentPage
{
    private readonly SavedViewModel _savedViewModel;

    public SavedPage(SavedViewModel savedViewModel)
	{
		InitializeComponent();

        _savedViewModel = savedViewModel;
        BindingContext = _savedViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            _savedViewModel.IsBusy = true;
            await _savedViewModel.LoadSavedPredictions();
            _savedViewModel.IsBusy = false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

}