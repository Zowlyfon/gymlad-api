using AutoMapper;

namespace GymLad.Models
{
    public class Set
    {
        public long Id { get; set; }
        public long ExerciseId { get; set; }
        public long WorkoutId { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }

        public virtual Exercise Exercise { get; set; }
        public virtual Workout Workout { get; set; }
    }
}