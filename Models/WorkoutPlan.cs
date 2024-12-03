﻿using FitnessWorkoutMgmnt.Models;

namespace FitnessWorkoutMgmnt.Models
{
    public class WorkoutPlan
    {
        public int PlanId { get; set; }
        public string? Name { get; set; }
        public string? Goal { get; set; }  // e.g., Weight Loss, Muscle Gain
        public string? DifficultyLevel { get; set; }  // Beginner, Intermediate, Advanced
        public string? Description { get; set; }
        public int TrainerId { get; set; }
        public User? Trainer { get; set; }
    }
}
