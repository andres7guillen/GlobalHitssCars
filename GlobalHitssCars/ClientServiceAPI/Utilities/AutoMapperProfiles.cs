using ClientServiceAPI.Models;
using ClientServiceDomain.Entities;

namespace ClientServiceAPI.Utilities
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ClientModel, Client>()
                .ForMember(a => a.Id,
                    opt => opt.MapFrom(src => Guid.Parse(src.Id))
            );

            CreateMap<Client, ClientModel>()
                .ForMember(c => c.Id,
                opt => opt.MapFrom(src => src.Id.ToString())
            );
        }
    }
}
