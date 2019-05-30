using System;
using System.Collections.Generic;

namespace GymLad.Models
{
    public class WorkoutDTO
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public DateTime Time { get; set; }
    }
}