using JanaTakmicenje22.Servisi;
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
    public partial class ShellView : UserControl
    {
        private readonly AuthServis _auth;
        private readonly ShellViewModel _vm;
        private readonly IServiceProvider _serviceProvider; 
        public Action? OnLogout { get; set; }
        public ShellView(AuthServis auth, ShellViewModel vm, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _auth = auth;
            _vm = vm;
            _serviceProvider = serviceProvider; 

            _vm.OnLogout = () => Dispatcher.Invoke(() => OnLogout?.Invoke());
            DataContext = _vm;

            var user = _auth.CurrentUser;
            if (user != null)
            {
                UsernameLabel.Text = user.Username;
                AvatarLetter.Text = user.Username.Length > 0
                    ? user.Username[0].ToString().ToUpper() : "?";
            }

            NavigateTo("home");
        }

        private void NavigateTo(string route)
        {
            SetNavHighlight(route);

            switch (route)
            {
                case "home":
                    var homePage = _serviceProvider.GetRequiredService<HomePage>();
                    _ = homePage.RefreshAsync();
                    MainContent.Content = homePage;
                    break;

                case "challenges":
                    var challengesPage = _serviceProvider.GetRequiredService<ChallengesPage>();
                    _ = challengesPage.RefreshAsync();
                    MainContent.Content = challengesPage;
                    break;

                case "notes":
                    var notesPage = _serviceProvider.GetRequiredService<NotesPage>();
                    _ = notesPage.RefreshAsync(); 
                    MainContent.Content = notesPage;
                    break;

                case "chat":
                    var chatPage = _serviceProvider.GetRequiredService<TikChatPage>();
                    MainContent.Content = chatPage;
                    break;
            }
        }

        private void SetNavHighlight(string active)
        {
            var highlight = new SolidColorBrush(Color.FromRgb(61, 58, 54));
            var transparent = Brushes.Transparent;
            HomeBtn.Background = active == "home" ? highlight : transparent;
            ChallengesBtn.Background = active == "challenges" ? highlight : transparent;
            NotesBtn.Background = active == "notes" ? highlight : transparent;
            ChatBtn.Background = active == "chat" ? highlight : transparent;
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e) => NavigateTo("home");
        private void ChallengesBtn_Click(object sender, RoutedEventArgs e) => NavigateTo("challenges");
        private void NotesBtn_Click(object sender, RoutedEventArgs e) => NavigateTo("notes");
        private void ChatBtn_Click(object sender, RoutedEventArgs e) => NavigateTo("chat");
        private void LogoutBtn_Click(object sender, RoutedEventArgs e) => _vm.LogoutCommand.Execute(null);
    }
}


