using JanaTakmicenje22.ViewModels;
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
using Microsoft.Extensions.DependencyInjection;
namespace JanaTakmicenje22.Views.Pages
{
    public partial class TikChatPage : UserControl
    {
        private readonly TikChatViewModel _vm;

        public TikChatPage()
        {
            InitializeComponent();
            _vm = App.Services.GetRequiredService<TikChatViewModel>();
            DataContext = _vm;

            _vm.Messages.CollectionChanged += (_, __) => RenderMessages();
            _vm.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(TikChatViewModel.IsTyping))
                    TypingIndicator.Visibility = _vm.IsTyping ? Visibility.Visible : Visibility.Collapsed;
            };

            RenderMessages();
        }
        private void RenderMessages()
        {
            MessagesPanel.Children.Clear();
            foreach (var msg in _vm.Messages)
            {
                MessagesPanel.Children.Add(CreateBubble(msg));
            }
            MessagesScroll.ScrollToBottom();
        }
        private UIElement CreateBubble(ChatBubble msg)
        {
            var outerGrid = new Grid { Margin = new Thickness(0, 0, 0, 16) };

            var bubble = new Border
            {
                MaxWidth = 480,
                Padding = new Thickness(16, 12, 16, 12),
                CornerRadius = msg.IsUser ? new CornerRadius(18, 18, 4, 18) : new CornerRadius(4, 18, 18, 18),
                Background = msg.IsUser ? new SolidColorBrush(Color.FromRgb(85, 136, 171)) : Brushes.White
            };

            var sp = new StackPanel();
            var textBlock = new TextBlock
            {
                Text = msg.Content,
                Foreground = msg.IsUser ? Brushes.White : Brushes.Black,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14
            };

            sp.Children.Add(textBlock);

            sp.Children.Add(new TextBlock
            {
                Text = msg.Time,
                FontSize = 10,
                Foreground = Brushes.Gray,
                HorizontalAlignment = msg.IsUser ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                Margin = new Thickness(0, 4, 0, 0)
            });

            bubble.Child = sp;
            outerGrid.HorizontalAlignment = msg.IsUser ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            outerGrid.Children.Add(bubble);

            return outerGrid;
        }

        private async void SendBtn_Click(object sender, RoutedEventArgs e) => await SendAsync();

        private async void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                await SendAsync();
            }
        }

        private async Task SendAsync()
        {
            _vm.InputText = InputBox.Text;
            InputBox.Clear();
            await _vm.SendCommand.ExecuteAsync(null);
        }

        private void ClearChat_Click(object sender, RoutedEventArgs e)
        {
            _vm.ClearCommand.Execute(null);
            RenderMessages();
        }
    }

}
