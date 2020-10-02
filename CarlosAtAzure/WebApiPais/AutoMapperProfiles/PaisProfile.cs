using AutoMapper;
using Domain.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPais.Models;

namespace WebApiPais.AutoMapperProfiles
{
    public class PaisProfile : Profile
    {
        public PaisProfile()
        {
            CreateMap<PaisRequest, Pais>();
            CreateMap<Pais, PaisResponse>();
            CreateMap<Pais, PaisResponseComEstados>();
        }
    }
}
