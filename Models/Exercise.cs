using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace TheGymAPI.Models;

public class Exercise
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? VideoURL { get; set; }
    public ICollection<MuscleGroup>? MuscleGroups { get; set; }
    public ExerciseType Type { get; set; }

}