using FitnessWorkoutMgmnt.Models;
using FitnessWorkoutMgmnt.Repository;

namespace FitnessWorkoutMgmnt.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<IEnumerable<Challenge>> GetAllChallenges()
        {
            return await _challengeRepository.GetAllChallenges();
        }

        public async Task<Challenge> AddChallenge(Challenge challenge)
        {
            return await _challengeRepository.CreateChallenge(challenge);
        }

        public async Task<Challenge> GetUserChallenges(int userId)
        {
            return await _challengeRepository.GetChallengeByIdAsync(userId);
        }

        public async Task<Challenge> UpdateChallenge(int challengeId, Challenge challenge)
        {
            return await _challengeRepository.UpdateChallenge(challengeId, challenge);
        }

        public async Task DeleteChallenge(int challengeId)
        {
            await _challengeRepository.DeleteChallengeAsync(challengeId);
        }


    }
}
