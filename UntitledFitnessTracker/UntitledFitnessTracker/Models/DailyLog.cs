using System;
using System.Collections.Generic;

namespace UntitledFitnessTracker.Models;

public partial class DailyLog
{
    public int LogId { get; set; }

    public DateOnly LogDate { get; set; }

    public decimal Weight { get; set; }

    public int? CaffeineIntake { get; set; }

    public decimal? SleepHours { get; set; }

    public int? Steps { get; set; }

    public virtual ICollection<DailyWorkout> DailyWorkouts { get; set; } = new List<DailyWorkout>();

    public virtual ICollection<MealsInLog> MealsInLogs { get; set; } = new List<MealsInLog>();
}
