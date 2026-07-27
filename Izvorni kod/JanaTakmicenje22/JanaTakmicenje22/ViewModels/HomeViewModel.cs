using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JanaTakmicenje22.Core.Models;
using JanaTakmicenje22.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanaTakmicenje22.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly AuthServis _auth;
        private readonly NoteServis _noteService;
        private readonly ChallengeServis _challengeService;

        [ObservableProperty] private string _greeting = string.Empty;
        [ObservableProperty] private int _userXP;
        [ObservableProperty] private int _userLevel;
        [ObservableProperty] private int _userStreak;
        [ObservableProperty] private int _completedChallenges;
        [ObservableProperty] private double _levelProgress;
        [ObservableProperty] private string _quickNoteContent = string.Empty;
        [ObservableProperty] private string _saveStatus = string.Empty;
        [ObservableProperty] private List<UserPostignuca> _recentBadges = new();

        private Note? _activeNote;

        public HomeViewModel(AuthServis auth, NoteServis noteService, ChallengeServis challengeService)
        {
            _auth = auth;
            _noteService = noteService;
            _challengeService = challengeService;
        }

        public async Task LoadAsync()
        {
            var user = _auth.CurrentUser!;
            var hour = DateTime.Now.Hour;
            var greetWord = hour < 12 ? "Dobro jutroooo" : hour < 18 ? "Dobar dannnn" : "Dobro večeee";
            Greeting = $"{greetWord}, {user.Username}! ദ്ദി ˉ͈̀꒳ˉ͈́ )✧";

            UserXP = user.XP;
            UserLevel = user.Level;
            UserStreak = user.Streak;
            CompletedChallenges = user.TotalChallengesCompleted;

            int xpForThisLevel = (UserLevel - 1) * 500;
            int xpForNextLevel = UserLevel * 500;
            LevelProgress = Math.Clamp((double)(UserXP - xpForThisLevel) / (xpForNextLevel - xpForThisLevel), 0, 1);

            RecentBadges = await _challengeService.GetUserBadgesAsync();
            var notes = await _noteService.GetNotesAsync();
            _activeNote = notes.FirstOrDefault();
            if (_activeNote == null)
                _activeNote = await _noteService.CreateNoteAsync("Prva beleška");
            QuickNoteContent = _activeNote.Content;
        }

        [RelayCommand]
        private async Task SaveQuickNoteAsync()
        {
            if (_activeNote == null) return;
            _activeNote.Content = QuickNoteContent;
            await _noteService.SaveNoteAsync(_activeNote);
            SaveStatus = "Sačuvano ✓";
            await Task.Delay(2000);
            SaveStatus = string.Empty;
        }
    }

}
