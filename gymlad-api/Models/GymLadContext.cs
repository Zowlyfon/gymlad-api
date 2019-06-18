using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using System;

namespace GymLad.Models
{
    public class GymLadContext : DbContext
    {
        public GymLadContext(DbContextOptions<GymLadContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }

        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<WorkoutTemplate> WorkoutTemplates { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<SetTemplate> SetTemplates { get; set; }
    }
}