using Backend.Domain.Models;
using Backend.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.IRepository
{
    public interface ICuestionarioRepository
    {
        Task CrearCuestionario(Cuestionario cuestionario);
        Task<Cuestionario> GetCuestionario(int idCuestionario);
        Task<List<Cuestionario>> GetListCuestionarioByUser( int ideUser);
        Task<List<Cuestionario>> GetCuestionarioList();

        Task<Cuestionario> BuscarCuestionario(int idCuestionario,int idUser);
        Task EliminarCuestionario(Cuestionario cuestionario);
       

    }
}
