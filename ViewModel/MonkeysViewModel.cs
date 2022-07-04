using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using monkeyfinder.Models;
using monkeyfinder.Pages;
using monkeyfinder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monkeyfinder.ViewModel
{
    public partial class MonkeysViewModel : BaseViewModel
    {
        private readonly MonkeyService monkeyService;
        private readonly IConnectivity connectivity;
        private readonly IGeolocation geolocation;

        public ObservableCollection<Monkey> Monkeys { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Monkey Finder";
            this.monkeyService = monkeyService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;
            //GetMonkeysCommand.Execute();
        }

        [RelayCommand]
        async Task GetMonkeysAsync()
        { 
            if(IsBusy)
                return;

            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet Error!", $"Check your internet and try again!", "OK");
                return;
            }

            try
            {
                IsBusy = true;
                var monkeys = await monkeyService.GetMonkeys();

                if (monkeys.Count != 0)
                    Monkeys.Clear();

                foreach(var monkey in monkeys)
                    Monkeys.Add(monkey);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
                await Shell.Current.DisplayAlert("Error!", 
                    $"Unable to get monkeys: {ex.Message}", "OK");
            }
            finally
            { 
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GoToDetailsAsync(Monkey monkey)
        {
            if (monkey == null)
                return;
            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
                {
                    { "Monkey", monkey }
                });
        }

        [RelayCommand]
        async Task GetClosestMonkeyAsync()
        {
            if (IsBusy || !Monkeys.Any())
                return;

            try
            {
                var location = await geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await geolocation.GetLocationAsync(new GeolocationRequest
                                { 
                                    DesiredAccuracy = GeolocationAccuracy.Medium,
                                    Timeout = TimeSpan.FromSeconds(30),
                                });

                }

                if (location is null)
                    return;

                var first = Monkeys.OrderBy(m => location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Miles)).FirstOrDefault();

                if (first is null)
                    return;

                await Shell.Current.DisplayAlert("Closest Monkey", 
                    $"{first.Name} in {first.Location}", "OK");
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get closest monkeys: {ex.Message}", "OK");
            }
        }
    }
}
