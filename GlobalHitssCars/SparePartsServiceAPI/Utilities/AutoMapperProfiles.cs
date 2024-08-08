using SparePartsServiceAPI.Models;
using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;

namespace SparePartsServiceAPI.Utilities
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SparePartModel, SparePart>()
                .ForMember(a => a.Id,
                    opt => opt.MapFrom(src => Guid.Parse(src.Id))
            );

            CreateMap<SparePart, SparePartModel>()
                .ForMember(c => c.Id,
                opt => opt.MapFrom(src => src.Id.ToString())
            );

            CreateMap<GetSparePartByFilterModel, GetSparePartByFilterDTO>();
        }
    }
}
