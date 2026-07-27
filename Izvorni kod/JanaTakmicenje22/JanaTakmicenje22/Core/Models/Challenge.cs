using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanaTakmicenje22.Core.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Order { get; set; }
        public int XPReward { get; set; } = 100;

        public ICollection<UserChallenge> UserChallenges { get; set; } = new List<UserChallenge>();

    }
}
