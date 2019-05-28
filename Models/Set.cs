namespace GymLad.Models
{
    public class Set
    {
        public long Id { get; set; }
        public long ExerciseId { get; set; }
        public long WorkoutId { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }

        public Exercise Exercise { get; set; }
        public Workout Workout { get; set; }
    }
}