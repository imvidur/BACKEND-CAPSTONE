﻿using FitnessWorkoutMgmnt.Models;

namespace FitnessWorkoutMgmnt.Repository
{
    public interface IMealPlanRepository
    {
        Task<IEnumerable<MealPlan>> GetMealPlansByUserId(int userId);
        Task<MealPlan> AddMealPlan(MealPlan mealPlan);
        Task<IEnumerable<MealPlan>> GetAllFitnessClassesAsync();
        Task<MealPlan> UpdateMealPlan(MealPlan mealPlan);
        Task DeleteMealPlan(int mealPlanId);
    }
}
