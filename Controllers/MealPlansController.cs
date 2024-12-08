﻿using FitnessWorkoutMgmnt.Models;
using FitnessWorkoutMgmnt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessWorkoutMgmnt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealPlansController : ControllerBase
    {
        private readonly IMealPlanService _mealPlanService;

        public MealPlansController(IMealPlanService mealPlanService)
        {
            _mealPlanService = mealPlanService;
        }


        [HttpGet]
        //[Authorize(Policy ="AdminTrainerNutritionistOnly")]
        public async Task<ActionResult<IEnumerable<MealPlan>>> GetAllFitnessClasses()
        {
            var fitnessClasses = await _mealPlanService.GetAllFitnessClassesAsync();
            return Ok(fitnessClasses);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetMealPlans(int userId)
        {
            var mealPlans = await _mealPlanService.GetMealPlansByUserId(userId);
            return Ok(mealPlans);
        }

        [HttpPost]
        //[Authorize(Policy ="AdminTrainerNutritionistOnly")]

        public async Task<IActionResult> CreateMealPlan([FromBody] MealPlan mealPlan)
        {
            var createdMealPlan = await _mealPlanService.CreateMealPlan(mealPlan);
            return CreatedAtAction(nameof(GetMealPlans), new { userId = mealPlan.UserId }, createdMealPlan);
        }

        [HttpPut("{mealPlanId}")]
        //[Authorize(Policy ="AdminTrainerNutritionistOnly")]

        public async Task<IActionResult> UpdateMealPlan(int mealPlanId, [FromBody] MealPlan mealPlan)
        {
            if (mealPlanId != mealPlan.MealPlanId)
                return BadRequest();

            var updatedMealPlan = await _mealPlanService.ModifyMealPlan(mealPlan);
            return Ok(updatedMealPlan);
        }

        [HttpDelete("{mealPlanId}")]
        //[Authorize(Policy ="AdminTrainerNutritionistOnly")]

        public async Task<IActionResult> DeleteMealPlan(int mealPlanId)
        {
            await _mealPlanService.RemoveMealPlan(mealPlanId);
            return NoContent();
        }
    }
}
