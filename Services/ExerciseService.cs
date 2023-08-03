using TheGymAPI.Models;
using Microsoft.EntityFrameworkCore;
using TheGymAPI.Data;
using TheGymAPI.Models.DTOs;

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
        var exercises = await _context.Exercises.Include(e => e.MuscleGroups).ToListAsync();
        return exercises;
    }

    public async Task<Exercise?> Get(int id)
    {
        return await _context.Exercises
            .Include(e => e.MuscleGroups)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Exercise?> GetByName(string name)
    {
        Exercise? exercise = await _context.Exercises
            .Include(e => e.MuscleGroups)
            .FirstOrDefaultAsync(e => e.Name == name);
        return exercise;
    }

    public async Task<Exercise> Add(ExerciseDTO newExerciseDTO)
    {
        Exercise newExercise = new Exercise
        {
            Name = newExerciseDTO.Name,
            Description = newExerciseDTO.Description,
            Type = newExerciseDTO.Type,
            VideoURL = newExerciseDTO.VideoURL
        };

        newExercise.Name = newExerciseDTO.Name;
        newExercise.Description = newExerciseDTO.Description;
        newExercise.Type = newExerciseDTO.Type;
        newExercise.VideoURL = newExerciseDTO.VideoURL;

        foreach (var name in newExerciseDTO.MuscleGroups)
        {
            var muscleGroup = await _context.MuscleGroups.FirstOrDefaultAsync(mg => mg.Name == name);

            if (muscleGroup == null)
            {
                muscleGroup = new MuscleGroup { Name = name, Exercises = new List<Exercise> { newExercise } };
                await _context.MuscleGroups.AddAsync(muscleGroup);
                await _context.SaveChangesAsync();
            }

            await _context.Exercises.AddAsync(newExercise);

            newExercise.MuscleGroups.Add(muscleGroup);
        }

        
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

