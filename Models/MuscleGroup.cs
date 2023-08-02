using Microsoft.EntityFrameworkCore;

namespace TheGymAPI.Models
{
    public class MuscleGroup
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
