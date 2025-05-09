using System;
using System.Collections.Generic;

namespace UntitledFitnessTracker.Models;

public partial class DailyWorkoutBreakdown
{
    public int BreakdownId { get; set; }

    public int DworkoutId { get; set; }

    public int? WorkoutId { get; set; }

    public int? ExerciseId { get; set; }

    public int? Sets { get; set; }

    public int? Reps { get; set; }

    public decimal? Weight { get; set; }

    public bool PreWorkout { get; set; }

    public virtual DailyWorkout Dworkout { get; set; } = null!;

    public virtual Exercise? Exercise { get; set; }

    public virtual Workout? Workout { get; set; }
}
