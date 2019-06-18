using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymLad.Models
{
    public class Set
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ExerciseId { get; set; }
        public long WorkoutId { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }

        public virtual Exercise Exercise { get; set; }
        public virtual Workout Workout { get; set; }
    }
}