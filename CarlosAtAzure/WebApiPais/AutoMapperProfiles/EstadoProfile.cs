using AutoMapper;
using Domain.Estado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPais.Models;

namespace WebApiPais.AutoMapperProfiles
{
    public class EstadoProfile : Profile
    {
        public EstadoProfile()
        {
            CreateMap<EstadoRequest, Estado>();
            CreateMap<Estado, EstadoResponse>();
        }
    }
}
