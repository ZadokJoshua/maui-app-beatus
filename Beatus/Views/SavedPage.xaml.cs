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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            _savedViewModel.LoadSavedPredictions();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

}