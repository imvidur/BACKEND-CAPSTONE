﻿using FitnessWorkoutMgmnt.Models;
using FitnessWorkoutMgmnt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessWorkoutMgmnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FitnessClassController(IFitnessClassService fitnessClassService) : ControllerBase
    {
        private readonly IFitnessClassService _fitnessClassService = fitnessClassService;

        [HttpGet]
        //[Authorize(Policy ="AdminTrainerOnly")]
        public async Task<ActionResult<IEnumerable<FitnessClass>>> GetAllFitnessClasses()
        {
            var fitnessClasses = await _fitnessClassService.GetAllFitnessClassesAsync();
            return Ok(fitnessClasses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FitnessClass>> GetFitnessClassById(int id)
        {
            var fitnessClass = await _fitnessClassService.GetFitnessClassByIdAsync(id);
            if (fitnessClass == null)
                return NotFound();
            return Ok(fitnessClass);
        }

        [HttpPost]
        //[Authorize(Policy ="AdminTrainerOnly")]

        public async Task<ActionResult<FitnessClass>> CreateFitnessClass(FitnessClass fitnessClass)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdFitnessClass = await _fitnessClassService.CreateFitnessClassAsync(fitnessClass);
            return CreatedAtAction(nameof(GetFitnessClassById), new { id = createdFitnessClass.ClassId }, createdFitnessClass);
        }

        [HttpPut("{id}")]
        //[Authorize(Policy ="AdminTrainerOnly")]
        public async Task<IActionResult> UpdateFitnessClass(int id, FitnessClass fitnessClass)
        {
            if (id != fitnessClass.ClassId)
                return BadRequest();
            await _fitnessClassService.UpdateFitnessClassAsync(id, fitnessClass);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Policy ="AdminTrainerOnly")]
        public async Task<IActionResult> DeleteFitnessClass(int id)
        {
            await _fitnessClassService.DeleteFitnessClassAsync(id);
            return NoContent();
        }
    }
}
