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

        public IQueryable<Workout> Workouts { get; set; }
        public IQueryable<Exercise> Exercises { get; set; }
    }
}