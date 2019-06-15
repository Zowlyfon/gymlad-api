using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace GymLad.Models
{
    public class CreatePersonDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public DateTime DoB { get; set; }
    }
}