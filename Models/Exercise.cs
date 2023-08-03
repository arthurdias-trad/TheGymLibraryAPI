using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace TheGymAPI.Models;

public class Exercise
{
    public Exercise()
    { 
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? VideoURL { get; set; }
    public ICollection<MuscleGroup> MuscleGroups { get; set; } = new List<MuscleGroup>();
    public ExerciseType Type { get; set; }

}

