using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;

namespace GymLad.Models
{
    public class GymLadContext : DbContext
    {
        public GymLadContext(DbContextOptions<GymLadContext> options) : base(options)
        {

        }

        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
    }
}