using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace GymLad.Models
{
    public class WorkoutTemplateDTO
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string TemplateName { get; set; }
    }
}