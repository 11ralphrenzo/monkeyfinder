using monkeyfinder.ViewModel;

namespace monkeyfinder;

public partial class MainPage : ContentPage
{

	public MainPage(MonkeysViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

