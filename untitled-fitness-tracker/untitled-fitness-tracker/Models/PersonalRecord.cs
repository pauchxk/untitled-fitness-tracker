using System;
using System.Collections.Generic;

namespace UntitledFitnessTracker.Models;

public partial class PersonalRecord
{
    public int PersonalRecordId { get; set; }

    public int ExerciseId { get; set; }

    public decimal Weight { get; set; }

    public int Reps { get; set; }

    public DateOnly Date { get; set; }

    public string? Notes { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;
}
