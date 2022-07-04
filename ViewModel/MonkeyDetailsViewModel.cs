using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using monkeyfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monkeyfinder.ViewModel
{
    [QueryProperty("Monkey", "Monkey")]
    public partial class MonkeyDetailsViewModel : BaseViewModel
    {
        private readonly IMap map;
        [ObservableProperty]
        Monkey monkey;

        public MonkeyDetailsViewModel(IMap map)
        {
            this.map = map;
        }

        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task OpenMapAsync()
        {
            try
            {
                await map.OpenAsync(monkey.Latitude, monkey.Longitude,
                    new MapLaunchOptions
                    { 
                        Name = monkey.Name,
                        NavigationMode = NavigationMode.None
                    });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to open map: {ex.Message}", "OK");
            }
        }
    }
}
