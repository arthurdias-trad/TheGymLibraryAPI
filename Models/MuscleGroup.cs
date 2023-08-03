using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace TheGymAPI.Models
{
    public class MuscleGroup
    {
        public MuscleGroup()
        {
        }

        public int Id { get; set; }
        public required string Name { get; set; }
        [JsonIgnore]
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
