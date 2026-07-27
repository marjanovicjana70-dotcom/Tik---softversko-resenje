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

namespace JanaTakmicenje22.ViewModels
{
    public partial class NotesViewModel : ObservableObject
    {
        private readonly NoteServis _noteService;

        [ObservableProperty] private ObservableCollection<Note> _notes = new();
        [ObservableProperty] private Note? _selectedNote;
        [ObservableProperty] private string _editTitle = string.Empty;
        [ObservableProperty] private string _editContent = string.Empty;
        [ObservableProperty] private string _saveStatus = string.Empty;
        [ObservableProperty] private bool _hasSelectedNote = false;

        public NotesViewModel(NoteServis noteService)
        {
            _noteService = noteService;
        }

        public async Task LoadAsync()
        {
            var notes = await _noteService.GetNotesAsync();
            Notes = new ObservableCollection<Note>(notes);
        }

        public void SelectNote(Note note)
        {
            SelectedNote = note;
            EditTitle = note.Title;
            EditContent = note.Content;
            HasSelectedNote = true;
        }

        [RelayCommand]
        private async Task NewNoteAsync()
        {
            var note = await _noteService.CreateNoteAsync("Nova beleška");
            Notes.Insert(0, note);
            SelectNote(note);
        }

        [RelayCommand]
        private async Task SaveNoteAsync()
        {
            if (SelectedNote == null) return;
            SelectedNote.Title = EditTitle;
            SelectedNote.Content = EditContent;
            SelectedNote.UpdatedAt = DateTime.UtcNow;

            await _noteService.SaveNoteAsync(SelectedNote);
            var current = SelectedNote;
            Notes.Remove(current);
            Notes.Insert(0, current);
            SelectedNote = current;

            SaveStatus = "Sačuvano -`♡´-";
            await Task.Delay(2000);
            SaveStatus = string.Empty;
        }

        [RelayCommand]
        private async Task DeleteNoteAsync()
        {
            if (SelectedNote == null) return;
            await _noteService.DeleteNoteAsync(SelectedNote.Id);
            Notes.Remove(SelectedNote);
            SelectedNote = null;
            HasSelectedNote = false;
        }
    }
}
