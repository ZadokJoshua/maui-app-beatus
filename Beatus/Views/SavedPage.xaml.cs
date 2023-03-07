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

        using var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(5));

        try
        {
            await (BindingContext as SavedViewModel)?.LoadSavedPredictions();
        }
        catch (OperationCanceledException ex)
        {
            Debug.WriteLine(ex);
        }
    }

}