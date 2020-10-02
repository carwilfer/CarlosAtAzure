using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApiAmigo.ResourceControllers.AmigoResource;

namespace WebApiAmigo.automapperProfiles
{
    public class AmigoProfile : Profile
    {
        public AmigoProfile()
        {
            CreateMap<AmigoRequest, Amigo>()
                .ForMember(
                    dest => dest.UrlFoto,
                    opt => opt.MapFrom(src => src.UrlFoto)
                );
            CreateMap<AmigoRequest, Amigo>();
            CreateMap<Amigo, AmigoResponse>();
            CreateMap<Amigo, AmigosComAmigos>();

        }
    }
}
