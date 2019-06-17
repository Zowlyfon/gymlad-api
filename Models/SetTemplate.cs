using AutoMapper;

namespace GymLad.Models
{
    public class SetTemplate
    {
        public long Id { get; set; }
        public long ExerciseId { get; set; }
        public long WorkoutTemplateId { get; set; }
        public int Reps { get; set; }
        public float Percentage { get; set; }

        public virtual Exercise Exercise { get; set; }
        public virtual WorkoutTemplate WorkoutTemplate { get; set; }
    }
}