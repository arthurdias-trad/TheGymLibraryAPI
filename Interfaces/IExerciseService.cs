using System.Collections.Generic;
using TheGymAPI.Models;
using TheGymAPI.Models.DTOs;

public interface IExerciseService
{
    Task<List<Exercise>> GetAllAsync(); // Corresponds to GET in REST, fetches all exercises
    Task<Exercise?> Get(int id); // Corresponds to GET/{id} in REST, fetches single exercise by ID
    Task<Exercise?> GetByName(string name); // Corresponds to GET/name/{name} in REST, fecthes single exercise by name
    Task<Exercise> Add(ExerciseDTO newExerciseDTO); // Corresponds to POST in REST, creates a new exercise
    Task<Exercise?> Update(int id, ExerciseDTO updatedExercise); // Corresponds to PUT in REST, updates an existing exercise
    Task<Exercise?> Delete(int id); // Corresponds to DELETE in REST, removes an exercise by ID
}
