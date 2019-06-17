namespace GymLad.Models
{
    public class SetDTO
    {
        public long Id { get; set; }
        public long ExerciseId { get; set; }
        public long WorkoutId { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
    }
}