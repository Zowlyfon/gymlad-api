using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace GymLad.Models
{
    public class Person : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public DateTime DoB { get; set; }

        public virtual List<Workout> Workouts { get; set; }
        public virtual List<WorkoutTemplate> WorkoutTemplates { get; set; }
        public virtual List<Exercise> Exercises { get; set; }
        public virtual List<PersonWeightChange> PersonWeightChanges { get; set; }
    }
}