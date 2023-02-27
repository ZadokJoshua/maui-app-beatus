using Beatus.ViewModels;

namespace Beatus.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _mainViewModel;

    public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();

        _mainViewModel = mainViewModel;
        BindingContext = mainViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _mainViewModel.Cancel();
    }
}

