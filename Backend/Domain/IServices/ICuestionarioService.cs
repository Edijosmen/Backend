using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.IServices
{
    public interface ICuestionarioService
    {
        Task CrearCuestionario(Cuestionario cuestinario);
        Task<Cuestionario> GetCuestionario(int idCuestionario);
        Task<List<Cuestionario>> GetCuestionarioList();
        Task<List<Cuestionario>> GetListCuestionarioByUser(int ideUser);
        Task<Cuestionario> BuscarCuestionario(int idCuestionario, int idUser);
        Task EliminarCuestionario(Cuestionario cuestionario);

    }
}
