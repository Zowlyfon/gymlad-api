using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace GymLad.Models
{
    public class WorkoutTemplate
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string TemplateName { get; set; }

        public virtual Person Person { get; set; } 
        public virtual List<SetTemplate> SetTemplates { get; set; }
    }
}