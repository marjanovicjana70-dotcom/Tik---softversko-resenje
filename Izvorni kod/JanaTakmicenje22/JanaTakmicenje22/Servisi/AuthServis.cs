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
    public class AuthServis
    {
        private readonly AppDbContext _db;
        public User? CurrentUser { get; private set; }

        public AuthServis(AppDbContext db)
        {
            _db = db;
        }
        public async Task<(bool Success, string Message)> RegisterAsync(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
                return (false, "Korisničko ime mora da ima najmanje 3 karaktera.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                return (false, "Unesi validan email.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return (false, "Lozinka mora imati najmanje 6 karaktera.");

            var exists = await _db.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower() || u.Email.ToLower() == email.ToLower());
            if (exists)
                return (false, "Korisnik sa tim imenom ili emailom već postoji.");

            var user = new User
            {
                Username = username.Trim(),
                Email = email.Trim().ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            CurrentUser = user;
            return (true, "Registracija uspešna!");
        }

        public async Task<(bool Success, string Message)> LoginAsync(string usernameOrEmail, string password)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(password))
                return (false, "Unesi korisničko ime i lozinku.");

            var input = usernameOrEmail.Trim().ToLower();
            var user = await _db.Users.FirstOrDefaultAsync(u =>
                u.Username.ToLower() == input || u.Email.ToLower() == input);

            if (user == null)
                return (false, "Korisnik nije pronađen.");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return (false, "Pogrešna lozinka.");

            CurrentUser = user;
            return (true, "Dobrodošao!");
        }
        public void Logout()
        {
            CurrentUser = null;
        }
    }

}
