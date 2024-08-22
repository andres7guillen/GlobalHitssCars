using CarServiceAPI.Models;
using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;

namespace CarServiceAPI.Utilities
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CarStockModel, CarStock>()
                .ForMember(a => a.Id,
                    opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? Guid.Parse(src.Id) : Guid.Empty))
                .ForMember(a => a.BrandId,
                    opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.BrandId) ? Guid.Parse(src.BrandId) : Guid.Empty))
                .ForMember(a => a.ReferenceId,
                    opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ReferenceId) ? Guid.Parse(src.ReferenceId) : Guid.Empty));

            CreateMap<CarStock, CarStockModel>()
                .ForMember(c => c.Id,
                opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(c => c.BrandId,
                opt => opt.MapFrom(src => src.BrandId.ToString()))
                .ForMember(c => c.ReferenceId,
                opt => opt.MapFrom(src => src.ReferenceId.ToString()));

            CreateMap<CarStockByFilterDTO, CarStockByFilterModel>();
            CreateMap<CarStockByFilterModel, CarStockByFilterDTO>();
        }
    }
}
