using Beatus.ViewModels;

namespace Beatus.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();

		BindingContext = mainViewModel;
	}
}

