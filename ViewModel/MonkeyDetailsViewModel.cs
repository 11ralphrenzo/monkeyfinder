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
        [ObservableProperty]
        Monkey monkey;

        public MonkeyDetailsViewModel()
        {

        }

        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
