using AutoMapper;
using Domain;
using WebApiAmigo.ResourceControllers.AmigoResource;

namespace WebApiAmigo.automapperProfiles
{
    public class AmigoDoAmigoProfile : Profile
    {
        public AmigoDoAmigoProfile()
        {
            CreateMap<AmigosDoAmigo, AmigosDoAmigoResponse>();
            CreateMap<AmigosDoAmigoRequest, AmigosDoAmigo>();
        }
    }
}
