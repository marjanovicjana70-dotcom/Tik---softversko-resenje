using JanaTakmicenje22.Core.Data;
using JanaTakmicenje22.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JanaTakmicenje22.Servisi
{
    public class ChallengeServis
    {
        private readonly AppDbContext _db;
        private readonly AuthServis _auth;

        public event Action<Postignuca>? BadgeEarned;
        public event Action<int>? XPGained;

        public ChallengeServis(AppDbContext db, AuthServis auth)
        {
            _db = db;
            _auth = auth;
        }

        public async Task<List<Challenge>> GetAllChallengesAsync()
        {
            return await _db.Challenges.OrderBy(c => c.Order).ToListAsync();
        }

        public async Task<List<UserChallenge>> GetUserChallengesAsync()
        {
            if (_auth.CurrentUser == null) return new();
            return await _db.UserChallenges
                .Include(uc => uc.Challenge)
                .Where(uc => uc.UserId == _auth.CurrentUser.Id)
                .ToListAsync();
        }

        public async Task<int> GetUnlockedUpToAsync()
        {
            if (_auth.CurrentUser == null) return 1;
            var completed = await _db.UserChallenges
                .Where(uc => uc.UserId == _auth.CurrentUser.Id && uc.IsCompleted)
                .Select(uc => uc.Challenge.Order)
                .ToListAsync();

            int unlocked = 1;
            while (completed.Contains(unlocked)) unlocked++;
            return unlocked; 
        }

        public async Task<bool> CompleteChallengAsync(int challengeId)
        {
            if (_auth.CurrentUser == null) return false;

            var existing = await _db.UserChallenges
                .FirstOrDefaultAsync(uc => uc.UserId == _auth.CurrentUser.Id && uc.ChallengeId == challengeId);

            if (existing != null && existing.IsCompleted) return false;

            var challenge = await _db.Challenges.FindAsync(challengeId);
            if (challenge == null) return false;

            if (existing == null)
            {
                existing = new UserChallenge
                {
                    UserId = _auth.CurrentUser.Id,
                    ChallengeId = challengeId,
                    IsCompleted = true,
                    CompletedAt = DateTime.UtcNow
                };
                _db.UserChallenges.Add(existing);
            }
            else
            {
                existing.IsCompleted = true;
                existing.CompletedAt = DateTime.UtcNow;
            }

         
            var user = await _db.Users.FindAsync(_auth.CurrentUser.Id);
            if (user != null)
            {
                user.XP += challenge.XPReward;
                user.TotalChallengesCompleted++;
                user.Level = CalculateLevel(user.XP);

              
                var today = DateTime.UtcNow.Date;
                if (user.LastActivityDate?.Date == today.AddDays(-1))
                    user.Streak++;
                else if (user.LastActivityDate?.Date != today)
                    user.Streak = 1;
                user.LastActivityDate = DateTime.UtcNow;

                _auth.CurrentUser.XP = user.XP;
                _auth.CurrentUser.Level = user.Level;
                _auth.CurrentUser.Streak = user.Streak;
                _auth.CurrentUser.TotalChallengesCompleted = user.TotalChallengesCompleted;

                await _db.SaveChangesAsync();
                XPGained?.Invoke(challenge.XPReward);

                await CheckAndAwardBadgesAsync(user);
            }

            return true;
        }

        private async Task CheckAndAwardBadgesAsync(User user)
        {
            var allBadges = await _db.Postignuca.ToListAsync();
            var earned = await _db.UserPostignuca.Where(ub => ub.UserId == user.Id).Select(ub => ub.BadgeId).ToListAsync();

            foreach (var badge in allBadges)
            {
                if (!earned.Contains(badge.Id) && user.TotalChallengesCompleted >= badge.RequiredChallenges)
                {
                    _db.UserPostignuca.Add(new UserPostignuca { UserId = user.Id, BadgeId = badge.Id });
                    await _db.SaveChangesAsync();
                    BadgeEarned?.Invoke(badge);
                }
            }
        }

        public async Task<List<UserPostignuca>> GetUserBadgesAsync()
        {
            if (_auth.CurrentUser == null) return new();
            return await _db.UserPostignuca
                .Include(ub => ub.Badge)
                .Where(ub => ub.UserId == _auth.CurrentUser.Id)
                .ToListAsync();
        }

        public static int CalculateLevel(int xp)
        {
          
            return Math.Max(1, (int)(xp / 500.0) + 1);
        }

        public static int XPForNextLevel(int level)
        {
            return level * 500;
        }
    }

}
