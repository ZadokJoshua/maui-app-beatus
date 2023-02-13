using Beatus.Maui.ViewModels;

namespace Beatus.Maui.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();

		BindingContext = mainViewModel;
	}

}

