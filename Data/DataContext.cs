using Microsoft.EntityFrameworkCore;
using DotNetAPI.Models;

namespace TheGymAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=thegymdb;Trusted_Connection=true;TrustServerCertificate=true");
        }

        public DbSet<Exercise> Exercises { get; set; }
    }
}
