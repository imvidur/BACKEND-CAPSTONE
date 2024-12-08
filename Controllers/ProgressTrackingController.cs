using FitnessWorkoutMgmnt.Data;
using FitnessWorkoutMgmnt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FitnessWorkoutMgmnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressTrackingController : ControllerBase
    {
        private readonly FitnessDbContext _context;


        public ProgressTrackingController(FitnessDbContext context)
        {
            _context = context;
        }

        // GET: api/progresstracking/my-progress
        [HttpGet("my-progress")]
        //[Authorize(Policy = "ClientTrainerOnly")]
        public async Task<ActionResult<IEnumerable<ProgressTracking>>> GetMyProgress([FromQuery] int clientId)
        {
            if (clientId <= 0)
            {
                return BadRequest("Invalid client ID.");
            }

            var progress = await _context.ProgressTrackings
                                         .Where(p => p.UserId == clientId)
                                         .ToListAsync();

            if (!progress.Any())
            {
                return NotFound("No progress data found for the specified client.");
            }

            return Ok(progress);
        }


        [HttpPost("update-progress")]
        //[Authorize(Policy = "TrainerNutritionistOnly")]
        public async Task<IActionResult> UpdateClientProgress([FromBody] ProgressTracking progress)
        {
            if (progress == null || progress.UserId <= 0)
            {
                return BadRequest("Invalid progress data.");
            }

            // Check if a progress record for this client already exists for the given date.
            var existingProgress = await _context.ProgressTrackings
                                                 .FirstOrDefaultAsync(p => p.UserId == progress.UserId && p.Date.Date == progress.Date.Date);

            if (existingProgress != null)
            {
                // Update existing record
                existingProgress.Weight = progress.Weight;
                existingProgress.BodyFatPercentage = progress.BodyFatPercentage;
                existingProgress.MuscleMass = progress.MuscleMass;
                existingProgress.Notes = progress.Notes;

                _context.ProgressTrackings.Update(existingProgress);
                await _context.SaveChangesAsync();

                return Ok(existingProgress);
            }
            else
            {
                // No existing record, create a new one
                _context.ProgressTrackings.Add(progress);
                await _context.SaveChangesAsync();

                return Ok(progress);
            }
        }
    }
}
