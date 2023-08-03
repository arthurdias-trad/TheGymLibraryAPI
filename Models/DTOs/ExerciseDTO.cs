namespace TheGymAPI.Models.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? VideoURL { get; set; }
        public List<string> MuscleGroups { get; set; } = new List<string>();
        public ExerciseType Type { get; set; }
    }


}
