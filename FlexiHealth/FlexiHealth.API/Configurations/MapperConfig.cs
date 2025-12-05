using AutoMapper;
using FlexiHealth.API.Data;
using FlexiHealth.API.Models;
using FlexiHealth.API.Models.Doctor;

namespace FlexiHealth.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<AddDoctorDto, Doctor>().ReverseMap();
            CreateMap<UpdateDoctorDto, Doctor>().ReverseMap();
        }
    }
}
