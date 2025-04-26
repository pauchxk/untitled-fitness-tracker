using System;
using System.Collections.Generic;

namespace UntitledFitnessTracker.Models;

public partial class Workout
{
    public int WorkoutId { get; set; }

    public string WorkoutName { get; set; } = null!;

    public string OverallGoal { get; set; } = null!;

    public string MuscleGroups { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual ICollection<DailyWorkoutBreakdown> DailyWorkoutBreakdowns { get; set; } = new List<DailyWorkoutBreakdown>();

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
