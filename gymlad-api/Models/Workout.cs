using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace GymLad.Models
{
    public class Workout
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public DateTime Time { get; set; }

        public virtual Person Person { get; set; } 
        public virtual List<Set> Sets { get; set; }
    }
}