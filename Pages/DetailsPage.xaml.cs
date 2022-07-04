using monkeyfinder.Models;
using monkeyfinder.ViewModel;

namespace monkeyfinder.Pages;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(MonkeyDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}