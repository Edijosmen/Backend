using AutoMapper;
using Backend.Domain.Models;
using Backend.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Persistence.Profiles
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Cuestionario, CuestionarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<RespuestaCuestionarioDetalle, ListaRespuestasDto>().ReverseMap();

        }
    }
}
