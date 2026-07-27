using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JanaTakmicenje22.Core.Models;
using JanaTakmicenje22.Servisi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JanaTakmicenje22.ViewModels
{
    public class ChallengeItem : ObservableObject
    {
        public Challenge Challenge { get; set; } = null!;

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                SetProperty(ref _isCompleted, value);
                OnPropertyChanged(nameof(StatusEmoji));
                OnPropertyChanged(nameof(StatusText));
            }
        }

        private bool _isUnlocked;
        public bool IsUnlocked
        {
            get => _isUnlocked;
            set
            {
                SetProperty(ref _isUnlocked, value);
                OnPropertyChanged(nameof(StatusEmoji));
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public string StatusEmoji => IsCompleted ? "✅" : IsUnlocked ? "♡🔐" : "♡🔐";
        public string StatusText => IsCompleted ? "Završeno!" : IsUnlocked ? "Otključano" : "Zaključano";
    }

    public partial class ChallengesViewModel : ObservableObject
    {
        private readonly ChallengeServis _challengeService;

        [ObservableProperty] private ObservableCollection<ChallengeItem> _challenges = new();
        [ObservableProperty] private ChallengeItem? _selectedChallenge;
        [ObservableProperty] private bool _isLoading = false;
        [ObservableProperty] private bool _showRewardPopup = false;
        [ObservableProperty] private string _rewardMessage = string.Empty;
        [ObservableProperty] private string _rewardXP = string.Empty;
        [ObservableProperty] private string _rewardBadge = string.Empty;

        public event Action<int, Postignuca?>? RewardEarned;

        public ChallengesViewModel(ChallengeServis challengeService)
        {
            _challengeService = challengeService;
        }

        public async Task LoadAsync()
        {
            IsLoading = true;
            var all = await _challengeService.GetAllChallengesAsync();
            var userChallenges = await _challengeService.GetUserChallengesAsync();
            var unlockedUpTo = await _challengeService.GetUnlockedUpToAsync();

            var completedIds = userChallenges
                .Where(uc => uc.IsCompleted)
                .Select(uc => uc.ChallengeId)
                .ToHashSet();

            Challenges.Clear();
            foreach (var c in all)
            {
                var item = new ChallengeItem
                {
                    Challenge = c,
                    IsCompleted = completedIds.Contains(c.Id),
                    IsUnlocked = c.Order <= unlockedUpTo
                };
                Challenges.Add(item);
            }
            IsLoading = false;
        }

        [RelayCommand]
        private async Task CompleteChallengeAsync(ChallengeItem item)
        {
            if (item.IsCompleted || !item.IsUnlocked) return;

            Postignuca? earnedBadge = null;
            void onBadge(Postignuca b) => earnedBadge = b;
            _challengeService.BadgeEarned += onBadge;

            var success = await _challengeService.CompleteChallengAsync(item.Challenge.Id);
            _challengeService.BadgeEarned -= onBadge;

            if (success)
            {
                item.IsCompleted = true;
                item.IsUnlocked = true;
                var nextItem = Challenges.FirstOrDefault(c => c.Challenge.Order == item.Challenge.Order + 1);
                if (nextItem != null)
                    nextItem.IsUnlocked = true;

                RewardEarned?.Invoke(item.Challenge.XPReward, earnedBadge);
            }
        }
    }

}
