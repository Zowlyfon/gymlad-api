using System;
using System.Collections.Generic;

namespace GymLad.Models
{
    public class Exercise
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Set> Sets { get; set; }
    }
}