using System;
using System.Collections.Generic;

namespace UntitledFitnessTracker.Models;

public partial class Food
{
    public int FoodId { get; set; }

    public string FoodName { get; set; } = null!;

    public string FoodGroup { get; set; } = null!;

    public string? FoodSubGroup { get; set; }

    public string? Notes { get; set; }

    public int Amount { get; set; }

    public int Calories { get; set; }

    public int Protein { get; set; }

    public int? Fat { get; set; }

    public int? Carbohydrates { get; set; }

    public int? Fiber { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
}
