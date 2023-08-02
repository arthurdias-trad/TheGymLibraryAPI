using TheGymAPI.Models;
using Microsoft.EntityFrameworkCore;
using TheGymAPI.Data;

namespace TheGymAPI.Services;

public class ExerciseService : IExerciseService
{
    private readonly DataContext _context;

    public ExerciseService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Exercise>> GetAllAsync()
    {
        var exercises = await _context.Exercises.ToListAsync();
        return exercises;
    }

    public async Task<Exercise?> Get(int id)
    {
        return await _context.Exercises.FindAsync(id);
    }

    public async Task<Exercise?> GetByName(string name)
    {
        Exercise? exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Name == name);
        return exercise;
    }

    public async Task<Exercise> Add(Exercise newExercise)
    {
        await _context.Exercises.AddAsync(newExercise);
        await _context.SaveChangesAsync();
        return newExercise;
    }

    public async Task<Exercise?> Update(int id, Exercise request)
    {
        Exercise? exercise = await _context.Exercises.FindAsync(id);
        if (exercise is null) return null;

        exercise.Name = request.Name;
        exercise.MuscleGroups = request.MuscleGroups;
        exercise.Description = request.Description;
        exercise.VideoURL = request.VideoURL;
        exercise.Type = request.Type;

        await _context.SaveChangesAsync();

        return exercise;
    }

    public async Task<Exercise?> Delete(int id)
    {
        Exercise? exercise = await _context.Exercises.FindAsync(id);
        if (exercise is null) return null;

        _context.Exercises.Remove(exercise);

        await _context.SaveChangesAsync();
        return exercise;
    }

}

