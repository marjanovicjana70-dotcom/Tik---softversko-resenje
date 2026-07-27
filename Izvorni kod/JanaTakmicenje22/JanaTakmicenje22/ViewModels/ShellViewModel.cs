using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JanaTakmicenje22.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace JanaTakmicenje22.ViewModels
{
    public partial class ShellViewModel : ObservableObject
    {
        private readonly NavigationServis _nav;
        private readonly AuthServis _auth;

        [ObservableProperty] private string _activeRoute = "home";
        [ObservableProperty] private string _username = string.Empty;

        public Action? OnLogout { get; set; }

        public ShellViewModel(NavigationServis nav, AuthServis auth)
        {
            _nav = nav;
            _auth = auth;
            Username = auth.CurrentUser?.Username ?? "Korisnik";
        }

        [RelayCommand] private void GoHome() { ActiveRoute = "home"; _nav.NavigateTo("home"); }
        [RelayCommand] private void GoChallenges() { ActiveRoute = "challenges"; _nav.NavigateTo("challenges"); }
        [RelayCommand] private void GoNotes() { ActiveRoute = "notes"; _nav.NavigateTo("notes"); }
        [RelayCommand] private void GoChat() { ActiveRoute = "chat"; _nav.NavigateTo("chat"); }

        [RelayCommand]
        private void Logout()
        {
            _auth.Logout();
            OnLogout?.Invoke();
        }
    }

}
