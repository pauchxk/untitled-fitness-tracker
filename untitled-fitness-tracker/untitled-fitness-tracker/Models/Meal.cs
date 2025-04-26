using System;
using System.Collections.Generic;

namespace UntitledFitnessTracker.Models;

public partial class Meal
{
    public int MealId { get; set; }

    public string MealName { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string? Notes { get; set; }

    public int Calories { get; set; }

    public int Protein { get; set; }

    public int? Fat { get; set; }

    public int? Carbohydrates { get; set; }

    public int? Fiber { get; set; }

    public virtual ICollection<MealsInLog> MealsInLogs { get; set; } = new List<MealsInLog>();

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
}
