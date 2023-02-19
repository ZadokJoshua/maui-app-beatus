using Beatus.Maui.ViewModels;

namespace Beatus.Maui.Views;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel detailsViewModel)
	{
		InitializeComponent();

		BindingContext = detailsViewModel;
	}
}