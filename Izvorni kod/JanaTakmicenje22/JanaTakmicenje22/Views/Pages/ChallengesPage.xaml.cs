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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using JanaTakmicenje22.Core.Models;
namespace JanaTakmicenje22.Views.Pages
{
    public partial class ChallengesPage : UserControl
    {
        private readonly ChallengesViewModel _vm;
        private ChallengeItem? _selectedItem;

        public ChallengesPage(ChallengesViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            _vm.RewardEarned += ShowReward;
            DataContext = _vm;
        }

        public async Task RefreshAsync()
        {
            await _vm.LoadAsync();
            ChallengesList.ItemsSource = _vm.Challenges;
            ProgressSummary.Text = $"{_vm.Challenges.Count(c => c.IsCompleted)} / 40 završeno";
        }

        private void ChallengesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedItem = ChallengesList.SelectedItem as ChallengeItem;
            if (_selectedItem == null) return;

            EmptyState.Visibility = Visibility.Collapsed;
            DetailPanel.Visibility = Visibility.Visible;

            var c = _selectedItem.Challenge;

            if (!_selectedItem.IsUnlocked)
            {
                DetailNumber.Text = $"Izazov #{c.Order}";
                DetailTitle.Text = "(ᴗ˳ᴗ)ᶻ𝗓𐰁 - Zaključano";
                DetailDescription.Text = "Nisi još uvek otključao ovaj izazov. Moraš da završiš zadnji izazov kako bi pristupio ovom..";
                DetailXP.Text = "??? XP";

                CompleteBtn.Content = "(ᴗ˳ᴗ)ᶻ𝗓𐰁 - Zaključano";
                CompleteBtn.IsEnabled = false;
            }
            else
            {
                DetailNumber.Text = $"Izazov #{c.Order}";
                DetailTitle.Text = c.Title;
                DetailDescription.Text = c.Description;
                DetailXP.Text = $"+{c.XPReward} XP za završetak";

                if (_selectedItem.IsCompleted)
                {
                    CompleteBtn.Content = "✅ Već završeno!";
                    CompleteBtn.IsEnabled = false;
                }
                else
                {
                    CompleteBtn.Content = "✔ Označi kao završeno";
                    CompleteBtn.IsEnabled = true;
                }
            }

    
            var fadeAnim = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(400));
            var slideAnim = new ThicknessAnimation(new Thickness(0, 30, 0, 0), new Thickness(0), TimeSpan.FromMilliseconds(400))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DetailPanel.BeginAnimation(UIElement.OpacityProperty, fadeAnim);
            DetailPanel.BeginAnimation(FrameworkElement.MarginProperty, slideAnim);
        }

        private async void CompleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem == null) return;
            await _vm.CompleteChallengeCommand.ExecuteAsync(_selectedItem);
            ChallengesList_SelectionChanged(sender, null!);
            ProgressSummary.Text = $"{_vm.Challenges.Count(c => c.IsCompleted)} / 40 završeno";
        }

        private void ShowReward(int xp, Postignuca? badge)
        {
            Dispatcher.Invoke(() =>
            {
                RewardEmoji.Text = badge != null ? badge.Emoji : "ദ്ദി(˵ •̀ ᴗ - ˵ ) ✧";
                RewardTitle.Text = badge != null ? $"Novi bedž: {badge.Name}!" : "Izazov završen!";
                RewardXPLabel.Text = $"+{xp} XP";

                if (badge != null)
                {
                    RewardBadgeLabel.Text = badge.Description;
                    RewardBadgeLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    RewardBadgeLabel.Visibility = Visibility.Collapsed;
                }

                RewardOverlay.Visibility = Visibility.Visible;

         
                var scaleAnim = new DoubleAnimation(0.7, 1.0, TimeSpan.FromMilliseconds(400))
                {
                    EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.5 }
                };
                var st = new ScaleTransform(0.7, 0.7);
                RewardOverlay.RenderTransform = st;
                RewardOverlay.RenderTransformOrigin = new Point(0.5, 0.5);
                st.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnim);
                st.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);

                var fadeAnim = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(250));
                RewardOverlay.BeginAnimation(UIElement.OpacityProperty, fadeAnim);
            });
        }

        private void CloseReward_Click(object sender, RoutedEventArgs e)
        {
            RewardOverlay.Visibility = Visibility.Collapsed;
        }
    }

}
