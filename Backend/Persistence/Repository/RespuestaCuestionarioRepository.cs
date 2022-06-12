using Backend.Domain.IRepository;
using Backend.Domain.Models;
using Backend.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Persistence.Repository
{
    public class RespuestaCuestionarioRepository:IRespuestaCuestionarioRepository
    {
        private readonly AplicationDbContext _DbContex;

        public RespuestaCuestionarioRepository(AplicationDbContext DbContex)
        {
            _DbContex = DbContex;
        }

        public async Task CrearRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            respuestaCuestionario.Activo = 1;
            respuestaCuestionario.Fecha = DateTime.Now;
             _DbContex.Add(respuestaCuestionario);
            await  _DbContex.SaveChangesAsync();
        }

   
        public async Task<List<RespuestaCuestionario>> GetRespuestaCuestionario(int idCuestionario, int idUsuario)
        {
           var listRepuestaCuestionario= await _DbContex.RespuestaCuestionarios.Where(x => x.CuestionarioId==idCuestionario
                                                                                        && x.Activo == 1 
                                                                                        && x.Cuestionario.UsuarioId== idUsuario)
                                                                                        .OrderByDescending(x=>x.Fecha).ToListAsync();
            return listRepuestaCuestionario;    
        }

        public async Task EliminarRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            respuestaCuestionario.Activo = 0;
            _DbContex.Entry(respuestaCuestionario).State = EntityState.Modified;
            await _DbContex.SaveChangesAsync();
        }

        public async  Task<RespuestaCuestionario> BuscarRespuestaCuestionarioByID(int idRCuestionario, int idUsuario)
        {
            var RCuestionario = await _DbContex.RespuestaCuestionarios.Where(x => x.Id == idRCuestionario
                                                                         && x.Cuestionario.UsuarioId == idUsuario).FirstOrDefaultAsync();
             return RCuestionario;
        }

        public async Task<int> GetIdCuestionarioFromRespuestaCuest(int idRCuestionario)
        {
            var cuestionario =  await _DbContex.RespuestaCuestionarios.Where(x=>x.Id==idRCuestionario
                                                                       && x.Activo==1).FirstOrDefaultAsync();
            if (cuestionario == null) return 0;
            return cuestionario.CuestionarioId;
        }

        public async Task<List<RespuestaCuestionarioDetalle>> GetlistaRespuestaDetalle(int idRCuestionario)
        {
            var listaRespuestas = await _DbContex.RespuestaCuestionarioDetalles.Where(x=>x.RespuestaCuestionarioId==idRCuestionario).ToListAsync();
            return listaRespuestas;
        }
    }
}
