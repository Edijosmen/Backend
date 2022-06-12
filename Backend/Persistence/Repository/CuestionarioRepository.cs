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
    public class CuestionarioRepository:ICuestionarioRepository
    {
        private readonly AplicationDbContext _context;
        public CuestionarioRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cuestionario> BuscarCuestionario(int idCuestionario, int idUser)
        {
            var cuestionario = await _context.Cuestionario.Where(x => x.Id == idCuestionario && x.Activo == 1 && x.UsuarioId==idUser).FirstOrDefaultAsync();
            return cuestionario;
        }

        public async Task CrearCuestionario(Cuestionario cuestionario)
        {
            _context.Cuestionario.Add(cuestionario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarCuestionario(Cuestionario cuestionario)
        {
            cuestionario.Activo = 0;
            _context.Entry(cuestionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Cuestionario> GetCuestionario(int idCuestionario)
        {
            return await _context.Cuestionario.Where(x => x.Activo == 1 && x.Id == idCuestionario)
                                               .Include(x => x.listPregunta)
                                               .ThenInclude(x=>x.listRespueta).FirstOrDefaultAsync();
        }

        public async Task<List<Cuestionario>> GetCuestionarioList()
        {
            var listCuestionario = await _context.Cuestionario.Where(x => x.Activo == 1).Include(x => x.Usuario).ToListAsync();
            return listCuestionario;
        }

        public async Task<List<Cuestionario>> GetListCuestionarioByUser(int ideUser)
        {
           return await _context.Cuestionario.Where(x => x.Activo == 1 && x.UsuarioId == ideUser).ToListAsync();
        }
    }
}
