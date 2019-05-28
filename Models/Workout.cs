using System;
using System.Collections.Generic;

namespace GymLad.Models
{
    public class Workout
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public DateTime Time { get; set; }

        public Person Person { get; set; } 
        public IEnumerable<Set> Sets { get; set; }
    }
}