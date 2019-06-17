using System;
using System.Collections.Generic;

namespace GymLad.Models
{
    public class PersonWeightChange
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public float Weight { get; set; }

        public virtual Person Person { get; set; }
    }
}