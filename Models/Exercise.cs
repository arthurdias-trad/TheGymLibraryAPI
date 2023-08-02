using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace TheGymAPI.Models;

public class Exercise
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? VideoURL { get; set; }
    [NotMapped]
    public string[]? MusclesWorked { get; set; }
    public string MusclesWorkedJson
    {
        get => JsonSerializer.Serialize(MusclesWorked);
        set => MusclesWorked = JsonSerializer.Deserialize<string[]>(value);
    }
    public ExerciseType Type { get; set; }

}