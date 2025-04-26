using System;
using System.Collections.Generic;

namespace UntitledFitnessTracker.Models;

public partial class DailyWorkout
{
    public int DworkoutId { get; set; }

    public int LogId { get; set; }

    public bool PreWorkout { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<DailyWorkoutBreakdown> DailyWorkoutBreakdowns { get; set; } = new List<DailyWorkoutBreakdown>();

    public virtual DailyLog Log { get; set; } = null!;
}
