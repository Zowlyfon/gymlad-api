using AutoMapper;
using GymLad.Models;

namespace GymLad {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<Workout, WorkoutDTO>();
            CreateMap<Set, SetDTO>();
            CreateMap<Person, PersonDTO>();
            CreateMap<Exercise, ExerciseDTO>();
        }
    }

}