using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JanaTakmicenje22.Core.Models
{
    public class Postignuca
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Emoji { get; set; } = "ദ്ദി´▽`)";
        public int RequiredChallenges { get; set; }

        public ICollection<UserPostignuca> UserBadges { get; set; } = new List<UserPostignuca>();

    }
    public class UserPostignuca
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BadgeId { get; set; }
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Postignuca Badge { get; set; } = null!;
    }
}
