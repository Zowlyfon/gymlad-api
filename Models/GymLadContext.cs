using Microsoft.EntityFrameworkCore;

namespace GymLad.Models
{
    public class GymLadContext : DbContext
    {
        public GymLadContext(DbContextOptions<GymLadContext> options) : base(options)
        {

        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Set> Sets { get; set; }
    }
}