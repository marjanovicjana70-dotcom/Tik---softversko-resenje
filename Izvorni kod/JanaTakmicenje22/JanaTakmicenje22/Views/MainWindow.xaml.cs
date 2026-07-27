using JanaTakmicenje22.Servisi;
using JanaTakmicenje22.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JanaTakmicenje22.Views
{
    public partial class MainWindow : Window
    {
        private readonly AuthServis _auth;

        public MainWindow(AuthServis auth)
        {
            InitializeComponent();
            _auth = auth;
            ShowLogin();
        }

        public void ShowLogin()
        {
            Width = 820;
            Height = 560;
            MinWidth = 820;
            MinHeight = 560;
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var loginPage = App.Services.GetRequiredService<LoginPage>();
            loginPage.OnLoginSuccess = ShowShell;
            loginPage.OnGoToRegister = ShowRegister;
            RootGrid.Children.Clear();
            RootGrid.Children.Add(loginPage);
        }

        public void ShowRegister()
        {
            Width = 820;
            Height = 600;
            MinWidth = 820;
            MinHeight = 600;
            ResizeMode = ResizeMode.NoResize;

            var registerPage = App.Services.GetRequiredService<RegisterPage>();
            registerPage.OnRegisterSuccess = ShowShell;
            registerPage.OnGoToLogin = ShowLogin;
            RootGrid.Children.Clear();
            RootGrid.Children.Add(registerPage);
        }

        public void ShowShell()
        {
            Width = 1100;
            Height = 720;
            MinWidth = 900;
            MinHeight = 600;
            ResizeMode = ResizeMode.CanResize;

            var shell = App.Services.GetRequiredService<ShellView>();
            shell.OnLogout = ShowLogin;
            RootGrid.Children.Clear();
            RootGrid.Children.Add(shell);
        }
    }

}