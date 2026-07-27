using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanaTakmicenje22.Core.Models
{
    public class UserChallenge
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }

        public User User { get; set; } = null!;
        public Challenge Challenge { get; set; } = null!;
    }
}
