using Beatus.ViewModels;

namespace Beatus.Views;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel detailsViewModel)
	{
		InitializeComponent();

		BindingContext = detailsViewModel;
	}
}