using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JanaTakmicenje22.Servisi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanaTakmicenje22.ViewModels
{
    public record ChatBubble(string Content, bool IsUser, string Time);

    public partial class TikChatViewModel : ObservableObject
    {
        private readonly TikChatServis _chat;

        [ObservableProperty] private ObservableCollection<ChatBubble> _messages = new();
        [ObservableProperty] private string _inputText = string.Empty;
        [ObservableProperty] private bool _isTyping = false;

        public TikChatViewModel(TikChatServis chat)
        {
            _chat = chat;
            Messages.Add(new ChatBubble(
                "Zdravoooo!! Moje ime je Tik ૮(•͈⌔•͈)ა, tu sam kad god ti zatreba neko za razgovor...",
                false,
                DateTime.Now.ToString("HH:mm")));
        }

        [RelayCommand]
        private async Task SendAsync()
        {
            if (string.IsNullOrWhiteSpace(InputText)) return;

            var userMsg = InputText.Trim();
            InputText = string.Empty;

            Messages.Add(new ChatBubble(userMsg, true, DateTime.Now.ToString("HH:mm")));

            IsTyping = true;
            var response = await _chat.SendMessageAsync(userMsg);
            IsTyping = false;

            if (!string.IsNullOrEmpty(response))
            {
                Messages.Add(new ChatBubble(response, false, DateTime.Now.ToString("HH:mm")));
            }
        }

        [RelayCommand]
        private void Clear()
        {
            _chat.ClearHistory();
            Messages.Clear();
            Messages.Add(new ChatBubble("Tik ovde, već znaš. Kaži šta ti je na mislima..", false, DateTime.Now.ToString("HH:mm")));
        }
    }

}
