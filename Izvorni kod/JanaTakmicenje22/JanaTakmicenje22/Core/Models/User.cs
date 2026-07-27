using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanaTakmicenje22.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

      
        public int XP { get; set; } = 0;
        public int Level { get; set; } = 1;
        public int Streak { get; set; } = 0;
        public DateTime? LastActivityDate { get; set; }
        public int TotalChallengesCompleted { get; set; } = 0;

        
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<UserChallenge> UserChallenges { get; set; } = new List<UserChallenge>();
        public ICollection<UserPostignuca> UserBadges { get; set; } = new List<UserPostignuca>();

    }
}
