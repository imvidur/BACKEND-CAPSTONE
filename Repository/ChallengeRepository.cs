using FitnessWorkoutMgmnt.Data;
using FitnessWorkoutMgmnt.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessWorkoutMgmnt.Repository
{
    public class ChallengeRepository : IChallengeRepository
    {
        private readonly FitnessDbContext _context;

        public ChallengeRepository(FitnessDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Challenge>> GetAllChallenges()
        {
            return await _context.Challenges.ToListAsync();
        }

        public async Task<Challenge> CreateChallenge(Challenge challenge)
        {
            _context.Challenges.Add(challenge);
            await _context.SaveChangesAsync();
            return challenge;
        }

        public async Task<Challenge> GetChallengeByIdAsync(int id)
        {
            return await _context.Challenges.FirstOrDefaultAsync(s => s.ChallengeId == id);
        }


        public async Task<Challenge> UpdateChallenge(int challengeId, Challenge challenge)
        {
            var existingSubscription = await GetChallengeByIdAsync(challengeId);
            if (existingSubscription == null)
                return null;

            existingSubscription.Name = challenge.Name;
            existingSubscription.StartDate = challenge.StartDate;
            existingSubscription.EndDate = challenge.EndDate;
            existingSubscription.UserId = challenge.UserId;
            existingSubscription.Goal = challenge.Goal;
            existingSubscription.Status = challenge.Status;

            await _context.SaveChangesAsync();
            return existingSubscription;
        }


        public async Task DeleteChallengeAsync(int id)
        {
            var challenge = await GetChallengeByIdAsync(id);
            if (challenge != null)
            {
                _context.Challenges.Remove(challenge);
                await _context.SaveChangesAsync();
            }
        }
    }
}