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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Text.Json;
namespace JanaTakmicenje22.Views.Pages
{
    public partial class HomePage : UserControl
    {
        private readonly HomeViewModel _vm;
        private readonly HttpClient _httpClient = new HttpClient();

        public HomePage(HomeViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "C# WPF Application");

            _ = LoadDailyQuote();
        }

        public async Task RefreshAsync()
        {
            await _vm.LoadAsync();
            UpdateUI();
            await LoadDailyQuote();
        }

        private async Task LoadDailyQuote()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://www.affirmations.dev/");
                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    var root = doc.RootElement;
                    QuoteText.Text = $"\"{root.GetProperty("affirmation").GetString()}\"";
                }
            }
            catch
            {
                QuoteText.Text = "\"Lowkey moras biti opusten\"";
            }
        }

        private void UpdateUI()
        {
            GreetingText.Text = _vm.Greeting;
            XPText.Text = _vm.UserXP.ToString("N0");
            LevelText.Text = $"Nivo {_vm.UserLevel}";
            StreakText.Text = _vm.UserStreak.ToString();
            CompletedText.Text = _vm.CompletedChallenges.ToString();
            QuickNoteBox.Text = _vm.QuickNoteContent;

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, () => {
                var parent = XPProgressBar.Parent as Border;
                if (parent != null) XPProgressBar.Width = parent.ActualWidth * _vm.LevelProgress;
            });

            BadgesPanel.Children.Clear();
            foreach (var ub in _vm.RecentBadges)
            {
                var card = new Border { Background = Brushes.White, CornerRadius = new CornerRadius(12), Margin = new Thickness(0, 0, 12, 0) };
                card.Effect = new DropShadowEffect { BlurRadius = 10, Opacity = 0.06 };
                var sp = new StackPanel { Margin = new Thickness(16, 12, 16, 12) };
                sp.Children.Add(new TextBlock { Text = ub.Badge.Emoji, FontSize = 28, HorizontalAlignment = HorizontalAlignment.Center });
                sp.Children.Add(new TextBlock { Text = ub.Badge.Name, FontSize = 11, FontWeight = FontWeights.SemiBold, Margin = new Thickness(0, 6, 0, 0) });
                card.Child = sp;
                BadgesPanel.Children.Add(card);
            }
        }

        private async void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            _vm.QuickNoteContent = QuickNoteBox.Text;
            await _vm.SaveQuickNoteCommand.ExecuteAsync(null);
            SaveStatusText.Text = _vm.SaveStatus;
        }

        private async void RefreshQuote_Click(object sender, RoutedEventArgs e)
        {
            QuoteText.Text = "Tražim citat kapetaneee samoo sekunda..";
            await LoadDailyQuote();
        }
    }

}
