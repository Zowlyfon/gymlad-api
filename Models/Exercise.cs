using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymLad.Models
{
    public class Exercise
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string Name { get; set; }
        public float TrainingMax { get; set; }

        public virtual Person Person { get; set; }
        public virtual List<Set> Sets { get; set; }
        public virtual List<SetTemplate> SetTemplates { get; set; }
    }
}