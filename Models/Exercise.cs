using System;
using System.Collections.Generic;
using System.Linq;

namespace GymLad.Models
{
    public class Exercise
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string Name { get; set; }
        public float TrainingMax { get; set; }

        public Person Person { get; set; }
        public IQueryable<Set> Sets { get; set; }
    }
}