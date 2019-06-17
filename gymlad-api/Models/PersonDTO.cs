using System;
using System.Collections.Generic;

namespace GymLad.Models
{
    public class PersonDTO
    {
        public long id { get; set; }
        public string userName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float height { get; set; }
        public float weight { get; set; }
        public DateTime DoB { get; set; }
    }
}