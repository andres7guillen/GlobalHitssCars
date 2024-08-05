using CarServiceAPI.Models;
using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;

namespace CarServiceAPI.Utilities
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CarModel, Car>()
                .ForMember(a => a.Id,
                    opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? Guid.Parse(src.Id) : Guid.Empty)
            );

            CreateMap<Car, CarModel>()
                .ForMember(c => c.Id,
                opt => opt.MapFrom(src => src.Id.ToString())
            );

            CreateMap<CarByFilterDTO, CarByFilterModel>();
            CreateMap<CarByFilterModel, CarByFilterDTO>();
        }
    }
}
