using AutoMapper;
using GymLad.Models;

namespace GymLad {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<Workout, WorkoutDTO>();
            CreateMap<WorkoutTemplate, WorkoutTemplateDTO>();
            CreateMap<Set, SetDTO>();
            CreateMap<SetTemplate, SetTemplateDTO>();
            CreateMap<Person, PersonDTO>();
            CreateMap<Exercise, ExerciseDTO>();
            CreateMap<CreatePersonDTO, Person>();
        }
    }

}