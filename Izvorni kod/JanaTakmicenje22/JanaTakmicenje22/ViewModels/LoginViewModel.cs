using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JanaTakmicenje22.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanaTakmicenje22.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthServis _auth;

        [ObservableProperty] private string _usernameOrEmail = string.Empty;
        [ObservableProperty] private string _errorMessage = string.Empty;
        [ObservableProperty] private bool _isLoading = false;

        public Action? OnSuccess { get; set; }
        public Action? OnGoToRegister { get; set; }

        public LoginViewModel(AuthServis auth)
        {
            _auth = auth;
        }

        [RelayCommand]
        private async Task LoginAsync(string password)
        {
            ErrorMessage = string.Empty;
            IsLoading = true;

            var (success, message) = await _auth.LoginAsync(UsernameOrEmail, password);

            IsLoading = false;
            if (success)
                OnSuccess?.Invoke();
            else
                ErrorMessage = message;
        }

        [RelayCommand]
        private void GoToRegister() => OnGoToRegister?.Invoke();
    }

}
