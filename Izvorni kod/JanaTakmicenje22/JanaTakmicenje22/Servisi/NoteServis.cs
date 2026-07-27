using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JanaTakmicenje22.Core.Models;
using JanaTakmicenje22.Core.Data;
using Microsoft.EntityFrameworkCore;
namespace JanaTakmicenje22.Servisi
{
    public class NoteServis
    {
        private readonly AppDbContext _db;
        private readonly AuthServis _auth;

        public NoteServis(AppDbContext db, AuthServis auth)
        {
            _db = db;
            _auth = auth;
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            if (_auth.CurrentUser == null) return new();
            return await _db.Notes
                .Where(n => n.UserId == _auth.CurrentUser.Id)
                .OrderByDescending(n => n.UpdatedAt)
                .ToListAsync();
        }

        public async Task<Note> CreateNoteAsync(string title = "Nova beleška")
        {
            if (_auth.CurrentUser == null) throw new InvalidOperationException("Not logged in");
            var note = new Note
            {
                UserId = _auth.CurrentUser.Id,
                Title = title,
                Content = string.Empty,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _db.Notes.Add(note);
            await _db.SaveChangesAsync();
            return note;
        }

        public async Task SaveNoteAsync(Note note)
        {
            note.UpdatedAt = DateTime.UtcNow;
            _db.Notes.Update(note);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int noteId)
        {
            var note = await _db.Notes.FindAsync(noteId);
            if (note != null)
            {
                _db.Notes.Remove(note);
                await _db.SaveChangesAsync();
            }
        }
    }

}
