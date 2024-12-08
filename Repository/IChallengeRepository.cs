using FitnessWorkoutMgmnt.Models;

namespace FitnessWorkoutMgmnt.Repository
{
    public interface IChallengeRepository
    {
        Task<IEnumerable<Challenge>> GetAllChallenges();
        Task<Challenge> CreateChallenge(Challenge challenge);
        Task<Challenge> GetChallengeByIdAsync(int userId);
        Task<Challenge> UpdateChallenge(int challengeId, Challenge challenge);

        Task DeleteChallengeAsync(int id);

    }
}
