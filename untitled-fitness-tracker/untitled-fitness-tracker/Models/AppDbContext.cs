using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UntitledFitnessTracker.Models;

namespace Components;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyLog> DailyLogs { get; set; }

    public virtual DbSet<DailyWorkout> DailyWorkouts { get; set; }

    public virtual DbSet<DailyWorkoutBreakdown> DailyWorkoutBreakdowns { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<MealsInLog> MealsInLogs { get; set; }

    public virtual DbSet<PersonalRecord> PersonalRecords { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=fitness-tracker-database;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("Daily_Log");

            entity.Property(e => e.LogId).HasColumnName("Log_ID");
            entity.Property(e => e.CaffeineIntake).HasColumnName("Caffeine_Intake");
            entity.Property(e => e.LogDate).HasColumnName("Log_Date");
            entity.Property(e => e.SleepHours)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("Sleep_Hours");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<DailyWorkout>(entity =>
        {
            entity.HasKey(e => e.DworkoutId).HasName("PK_Daily_Workout_1");

            entity.ToTable("Daily_Workout");

            entity.Property(e => e.DworkoutId)
                .ValueGeneratedNever()
                .HasColumnName("DWorkout_ID");
            entity.Property(e => e.LogId).HasColumnName("Log_ID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PreWorkout).HasColumnName("Pre_Workout");

            entity.HasOne(d => d.Log).WithMany(p => p.DailyWorkouts)
                .HasForeignKey(d => d.LogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Daily_Workout_Daily_Log1");
        });

        modelBuilder.Entity<DailyWorkoutBreakdown>(entity =>
        {
            entity.HasKey(e => new { e.BreakdownId, e.DworkoutId });

            entity.ToTable("Daily_Workout_Breakdown");

            entity.Property(e => e.BreakdownId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Breakdown_ID");
            entity.Property(e => e.DworkoutId).HasColumnName("DWorkout_ID");
            entity.Property(e => e.ExerciseId).HasColumnName("Exercise_ID");
            entity.Property(e => e.PreWorkout).HasColumnName("Pre-Workout");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.WorkoutId).HasColumnName("Workout_ID");

            entity.HasOne(d => d.Dworkout).WithMany(p => p.DailyWorkoutBreakdowns)
                .HasForeignKey(d => d.DworkoutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Daily_Workout_Breakdown_Daily_Workout");

            entity.HasOne(d => d.Exercise).WithMany(p => p.DailyWorkoutBreakdowns)
                .HasForeignKey(d => d.ExerciseId)
                .HasConstraintName("FK_Daily_Workout_Breakdown_Exercises");

            entity.HasOne(d => d.Workout).WithMany(p => p.DailyWorkoutBreakdowns)
                .HasForeignKey(d => d.WorkoutId)
                .HasConstraintName("FK_Daily_Workout_Breakdown_Workouts");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.Property(e => e.ExerciseId).HasColumnName("Exercise_ID");
            entity.Property(e => e.ExerciseName)
                .HasMaxLength(50)
                .HasColumnName("Exercise_Name");
            entity.Property(e => e.ExerciseType)
                .HasMaxLength(50)
                .HasColumnName("Exercise_Type");
            entity.Property(e => e.Goal).HasMaxLength(50);
            entity.Property(e => e.MuscleGroup)
                .HasMaxLength(50)
                .HasColumnName("Muscle_Group");
            entity.Property(e => e.MuscleSubGroup)
                .HasMaxLength(50)
                .HasColumnName("Muscle_SubGroup");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.ReccommendedRepsSets)
                .HasMaxLength(50)
                .HasColumnName("Reccommended_Reps_Sets");

            entity.HasMany(d => d.Workouts).WithMany(p => p.Exercises)
                .UsingEntity<Dictionary<string, object>>(
                    "ExercisesInWorkout",
                    r => r.HasOne<Workout>().WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Exercises_in_Workout_Workouts"),
                    l => l.HasOne<Exercise>().WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Exercises_in_Workout_Exercises"),
                    j =>
                    {
                        j.HasKey("ExerciseId", "WorkoutId");
                        j.ToTable("Exercises_in_Workout");
                        j.IndexerProperty<int>("ExerciseId").HasColumnName("Exercise_ID");
                        j.IndexerProperty<int>("WorkoutId").HasColumnName("Workout_ID");
                    });
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("PK_Foods_1");

            entity.Property(e => e.FoodId).HasColumnName("Food_ID");
            entity.Property(e => e.FoodGroup)
                .HasMaxLength(50)
                .HasColumnName("Food_Group");
            entity.Property(e => e.FoodName)
                .HasMaxLength(50)
                .HasColumnName("Food_Name");
            entity.Property(e => e.FoodSubGroup)
                .HasMaxLength(50)
                .HasColumnName("Food_SubGroup");
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasMany(d => d.Meals).WithMany(p => p.Foods)
                .UsingEntity<Dictionary<string, object>>(
                    "FoodsInMeal",
                    r => r.HasOne<Meal>().WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Foods_in_Meal_Meals"),
                    l => l.HasOne<Food>().WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Foods_in_Meal_Foods"),
                    j =>
                    {
                        j.HasKey("FoodId", "MealId");
                        j.ToTable("Foods_in_Meal");
                        j.IndexerProperty<int>("FoodId").HasColumnName("Food_ID");
                        j.IndexerProperty<int>("MealId").HasColumnName("Meal_ID");
                    });
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.Property(e => e.GoalId).HasColumnName("Goal_ID");
            entity.Property(e => e.GoalType)
                .HasMaxLength(50)
                .HasColumnName("Goal_Type");
            entity.Property(e => e.MuscleGroup)
                .HasMaxLength(50)
                .HasColumnName("Muscle_Group");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Period).HasMaxLength(20);
            entity.Property(e => e.TargetValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Target_Value");
            entity.Property(e => e.Unit).HasMaxLength(20);
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.Property(e => e.MealId).HasColumnName("Meal_ID");
            entity.Property(e => e.MealName)
                .HasMaxLength(50)
                .HasColumnName("Meal_Name");
            entity.Property(e => e.Method).HasMaxLength(500);
            entity.Property(e => e.Notes).HasMaxLength(500);
        });

        modelBuilder.Entity<MealsInLog>(entity =>
        {
            entity.HasKey(e => new { e.LogId, e.MealId });

            entity.ToTable("Meals_in_Log");

            entity.Property(e => e.LogId).HasColumnName("Log_ID");
            entity.Property(e => e.MealId).HasColumnName("Meal_ID");
            entity.Property(e => e.MealType)
                .HasMaxLength(50)
                .HasColumnName("Meal_Type");
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Log).WithMany(p => p.MealsInLogs)
                .HasForeignKey(d => d.LogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meals_in_Log_Daily_Log");

            entity.HasOne(d => d.Meal).WithMany(p => p.MealsInLogs)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meals_in_Log_Meals");
        });

        modelBuilder.Entity<PersonalRecord>(entity =>
        {
            entity.ToTable("Personal_Records");

            entity.Property(e => e.PersonalRecordId).HasColumnName("Personal_Record_ID");
            entity.Property(e => e.ExerciseId).HasColumnName("Exercise_ID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Exercise).WithMany(p => p.PersonalRecords)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personal_Records_Exercises");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("User_Profile");

            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("User_Name");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.Property(e => e.WorkoutId).HasColumnName("Workout_ID");
            entity.Property(e => e.MuscleGroups)
                .HasMaxLength(100)
                .HasColumnName("Muscle_Groups");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.OverallGoal)
                .HasMaxLength(50)
                .HasColumnName("Overall_Goal");
            entity.Property(e => e.WorkoutName)
                .HasMaxLength(50)
                .HasColumnName("Workout_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
