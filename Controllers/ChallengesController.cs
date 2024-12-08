using FitnessWorkoutMgmnt.Models;
using FitnessWorkoutMgmnt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessWorkoutMgmnt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public ChallengesController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChallenges()
        {
            var challenges = await _challengeService.GetAllChallenges();
            return Ok(challenges);
        }

        [HttpPost]
        //[Authorize(Policy ="AdminTrainerNutritionistOnly")]

        public async Task<IActionResult> CreateChallenge([FromBody] Challenge challenge)
        {
            var createdChallenge = await _challengeService.AddChallenge(challenge);
            return CreatedAtAction(nameof(GetChallengesById), new { challengeId = createdChallenge.ChallengeId }, createdChallenge);
        }

        [HttpGet("{challengeId}")]
        //[Authorize(Policy ="AdminTrainerNutritionistOnly")]

        public async Task<IActionResult> GetChallengesById(int challengeId)
        {
            var challenges = await _challengeService.GetUserChallenges(challengeId);
            return Ok(challenges);
        }

        [HttpPut("{challengeId}")]
        //[Authorize(Policy ="AdminTrainerNutritionistOnly")]

        public async Task<IActionResult> UpdateChallenge(int challengeId, [FromBody] Challenge challenge)
        {
            if (challengeId != challenge.ChallengeId)
                return BadRequest();

            var updatedChallenge = await _challengeService.UpdateChallenge(challengeId, challenge);
            return Ok(updatedChallenge);
        }

        [HttpDelete("{id}")]
        //[Authorize(Policy ="AdminTrainerNutritionistOnly")]

        public async Task<IActionResult> DeleteChallenge(int id)
        {
            await _challengeService.DeleteChallenge(id);
            return NoContent();
        }

    }

}
