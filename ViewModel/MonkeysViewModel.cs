using CommunityToolkit.Mvvm.Input;
using monkeyfinder.Models;
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

        public ObservableCollection<Monkey> Monkeys { get; } = new();

        public MonkeysViewModel(MonkeyService monkeyService)
        {
            Title = "Monkey Finder";
            this.monkeyService = monkeyService;
            //GetMonkeysCommand.Execute();
        }

        [RelayCommand]
        async Task GetMonkeysAsync()
        { 
            if(IsBusy)
                return;
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
            }
        }
    }
}
