using System;
using System.Collections.Generic;
using System.Linq;

namespace GymLad.Models
{
    public class Person
    {
        public long id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float height { get; set; }
        public float weight { get; set; }
        public int age { get; set; }

        public IQueryable<Workout> Workouts { get; set; }
    }
}