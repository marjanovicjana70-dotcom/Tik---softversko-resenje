using JanaTakmicenje22.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JanaTakmicenje22.Views.Pages
{
    public partial class LoginPage : UserControl
    {
        private readonly LoginViewModel _vm;
        public Action? OnLoginSuccess { get; set; }
        public Action? OnGoToRegister { get; set; }

        public LoginPage(LoginViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            _vm.OnSuccess = () => Dispatcher.Invoke(() => OnLoginSuccess?.Invoke());
            _vm.OnGoToRegister = () => Dispatcher.Invoke(() => OnGoToRegister?.Invoke());
            DataContext = _vm;
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            _vm.UsernameOrEmail = UsernameBox.Text;
            ErrorText.Visibility = Visibility.Collapsed;
            await _vm.LoginCommand.ExecuteAsync(PasswordBox.Password);
            if (!string.IsNullOrEmpty(_vm.ErrorMessage))
            {
                ErrorText.Text = _vm.ErrorMessage;
                ErrorText.Visibility = Visibility.Visible;
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            OnGoToRegister?.Invoke();
        }
    }


}
