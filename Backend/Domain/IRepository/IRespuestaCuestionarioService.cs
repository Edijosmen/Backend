using Backend.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Domain.IRepository
{
    public interface IRespuestaCuestionarioService
    {
        Task CrearRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario);
        Task<List<RespuestaCuestionario>> GetRespuestaCuestionario(int idCuestionario, int idUsuario);
        Task EliminarRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario);
        Task<RespuestaCuestionario> BuscarRespuestaCuestionarioByID(int idRCuestionario, int idUsuario);
        Task<int> GetIdCuestionarioFromRespuestaCuest(int idRCuestionario);
        Task<List<RespuestaCuestionarioDetalle>> GetlistaRespuestaDetalle(int idRCuestionario);
    }
}
