using System;
using System.Collections.Generic;

namespace UntitledFitnessTracker.Models;

public partial class MealsInLog
{
    public int LogId { get; set; }

    public int MealId { get; set; }

    public string MealType { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual DailyLog Log { get; set; } = null!;

    public virtual Meal Meal { get; set; } = null!;
}
