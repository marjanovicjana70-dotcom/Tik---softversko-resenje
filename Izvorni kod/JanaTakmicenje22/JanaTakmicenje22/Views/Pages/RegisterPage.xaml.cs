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
    public partial class RegisterPage : UserControl
    {
        private readonly RegisterViewModel _vm;
        public Action? OnRegisterSuccess { get; set; }
        public Action? OnGoToLogin { get; set; }

        public RegisterPage(RegisterViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            _vm.OnSuccess = () => Dispatcher.Invoke(() => OnRegisterSuccess?.Invoke());
            _vm.OnGoToLogin = () => Dispatcher.Invoke(() => OnGoToLogin?.Invoke());
            DataContext = _vm;
        }

        private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            _vm.Username = UsernameBox.Text;
            _vm.Email = EmailBox.Text;
            ErrorText.Visibility = Visibility.Collapsed;
            await _vm.RegisterCommand.ExecuteAsync(PasswordBox.Password);
            if (!string.IsNullOrEmpty(_vm.ErrorMessage))
            {
                ErrorText.Text = _vm.ErrorMessage;
                ErrorText.Visibility = Visibility.Visible;
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e) => OnGoToLogin?.Invoke();
    }


}
