using Backend.Domain.IRepository;
using Backend.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class RespuestaCuestionarioService:IRespuestaCuestionarioService
    {
        private readonly IRespuestaCuestionarioRepository _cuestionarioRepository;
        public RespuestaCuestionarioService(IRespuestaCuestionarioRepository cuestionarioRepository)
        {
            _cuestionarioRepository = cuestionarioRepository;
        }

        public async Task CrearRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            await _cuestionarioRepository.CrearRespuestaCuestionario(respuestaCuestionario);
        }

        public async Task<List<RespuestaCuestionario>> GetRespuestaCuestionario(int idCuestionario, int idUsuario)
        {
            return await _cuestionarioRepository.GetRespuestaCuestionario(idCuestionario, idUsuario);
        }
        public async Task EliminarRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
           await _cuestionarioRepository.EliminarRespuestaCuestionario(respuestaCuestionario);
        }

        public async Task<RespuestaCuestionario> BuscarRespuestaCuestionarioByID(int idRCuestionario, int idUsuario)
        {
            return await _cuestionarioRepository.BuscarRespuestaCuestionarioByID(idRCuestionario,idUsuario);
        }

        public async Task<int> GetIdCuestionarioFromRespuestaCuest(int idRCuestionario)
        {
            return await _cuestionarioRepository.GetIdCuestionarioFromRespuestaCuest(idRCuestionario);
        }

        public async Task<List<RespuestaCuestionarioDetalle>> GetlistaRespuestaDetalle(int idRCuestionario)
        {
            return await _cuestionarioRepository.GetlistaRespuestaDetalle(idRCuestionario);
        }
    }
}
