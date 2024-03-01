using PurchaseServiceAPI.Models;
using PurchaseServiceDomain.Entities;

namespace PurchaseServiceAPI.Utilities
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PurchaseModel, Purchase>()
                .ForMember(a => a.Id,
                    opt => opt.MapFrom(src => Guid.Parse(src.Id))
                ).ForMember(p => p.CarId,
                    opt => opt.MapFrom(src => Guid.Parse(src.CarId)))
                .ForMember(p => p.ClientId,
                    opt => opt.MapFrom(src => Guid.Parse(src.ClientId)));

            CreateMap<Purchase, PurchaseModel>()
                .ForMember(c => c.Id,
                opt => opt.MapFrom(src => src.Id.ToString())
            )
                .ForMember(p => p.Id,
                opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(p => p.CarId,
                opt => opt.MapFrom(src => src.CarId.ToString()))
                .ForMember(p => p.ClientId,
                opt => opt.MapFrom(src => src.ClientId.ToString()));
        }
    }
}
