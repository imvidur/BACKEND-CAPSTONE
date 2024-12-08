using FitnessWorkoutMgmnt.Models;

namespace FitnessWorkoutMgmnt.Services
{
    public interface IChallengeService
    {
        Task<IEnumerable<Challenge>> GetAllChallenges();
        Task<Challenge> AddChallenge(Challenge challenge);
        Task<Challenge> GetUserChallenges(int userId);
        Task<Challenge> UpdateChallenge(int challengeId, Challenge challenge);

        Task DeleteChallenge(int challengeId);
    }
}
