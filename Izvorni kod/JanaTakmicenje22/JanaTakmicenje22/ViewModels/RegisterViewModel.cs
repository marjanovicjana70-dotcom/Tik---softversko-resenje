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
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly AuthServis _auth;

        [ObservableProperty] private string _username = string.Empty;
        [ObservableProperty] private string _email = string.Empty;
        [ObservableProperty] private string _errorMessage = string.Empty;
        [ObservableProperty] private bool _isLoading = false;

        public Action? OnSuccess { get; set; }
        public Action? OnGoToLogin { get; set; }

        public RegisterViewModel(AuthServis auth)
        {
            _auth = auth;
        }

        [RelayCommand]
        private async Task RegisterAsync(string password)
        {
            ErrorMessage = string.Empty;
            IsLoading = true;
            var (success, message) = await _auth.RegisterAsync(Username, Email, password);
            IsLoading = false;
            if (success)
                OnSuccess?.Invoke();
            else
                ErrorMessage = message;
        }

        [RelayCommand]
        private void GoToLogin() => OnGoToLogin?.Invoke();
    }

}
