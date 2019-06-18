using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymLad.Models
{
    public class PersonWeightChange
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long PersonId { get; set; }
        public float Weight { get; set; }

        public virtual Person Person { get; set; }
    }
}