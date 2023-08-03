using TheGymAPI.Models;
using Microsoft.EntityFrameworkCore;
using TheGymAPI.Data;
using TheGymAPI.Models.DTOs;
using Microsoft.IdentityModel.Tokens;

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

        // Convert the Muscle Group from strings into objects
        foreach (var name in newExerciseDTO.MuscleGroups)
        {
            var muscleGroup = await _context.MuscleGroups.FirstOrDefaultAsync(mg => mg.Name == name);

            if (muscleGroup == null)
            {
                muscleGroup = new MuscleGroup { Name = name, Exercises = new List<Exercise> { newExercise } };
                await _context.MuscleGroups.AddAsync(muscleGroup);
            }


            newExercise.MuscleGroups.Add(muscleGroup);
        }

        await _context.Exercises.AddAsync(newExercise);
        await _context.SaveChangesAsync();
        return newExercise;
    }

    public async Task<Exercise?> Update(int id, ExerciseDTO request)
    {
        Exercise? exercise = await _context.Exercises.FindAsync(id);
        if (exercise is null) return null;

        exercise.Name = request.Name;
        exercise.Description = request.Description;
        exercise.VideoURL = request.VideoURL;
        exercise.Type = request.Type;

        if (!request.MuscleGroups.IsNullOrEmpty())
        {
            List<MuscleGroup> newMuscleGroups = new List<MuscleGroup>();

            List<string> currentMuscleGroupNames = exercise.MuscleGroups.Select(mg => mg.Name).ToList();

            foreach (string name in request.MuscleGroups)
            {
                // Checks if muscle group is already present in the exercise and adds it.
                if (currentMuscleGroupNames.Contains(name))
                {
                    newMuscleGroups.Add(exercise.MuscleGroups.First(mg => mg.Name == name));
                    continue;
                }

                // If muscle group is not present, either fetch it from DB or create it.
                else
                { 
                    MuscleGroup? muscleGroup = await _context.MuscleGroups.FirstOrDefaultAsync(mg => mg.Name == name);

                    if (muscleGroup == null)
                    {
                        muscleGroup = new MuscleGroup { Name = name, Exercises = new List<Exercise> { exercise } };
                        await _context.MuscleGroups.AddAsync(muscleGroup);
                    }

                    newMuscleGroups.Add(muscleGroup);
                }
                
            }
            exercise.MuscleGroups = newMuscleGroups;
        }

        

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

