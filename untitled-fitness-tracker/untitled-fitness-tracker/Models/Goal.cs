using System;
using System.Collections.Generic;

namespace UntitledFitnessTracker.Models;

public partial class Goal
{
    public int GoalId { get; set; }

    public string GoalType { get; set; } = null!;

    public decimal TargetValue { get; set; }

    public string Unit { get; set; } = null!;

    public string Period { get; set; } = null!;

    public string? MuscleGroup { get; set; }

    public string? Notes { get; set; }
}
