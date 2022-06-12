using Backend.Domain.IRepository;
using Backend.Domain.IServices;
using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class CuestionarioService : ICuestionarioService
    {
        private readonly ICuestionarioRepository _cuestionarioRepository;
        public CuestionarioService(ICuestionarioRepository cuestionarioRepository)
        {
            _cuestionarioRepository = cuestionarioRepository;
        }

        public async Task<Cuestionario> BuscarCuestionario(int idCuestionario, int idUser)
        {
            return await _cuestionarioRepository.BuscarCuestionario(idCuestionario, idUser);
        }

        public  async Task CrearCuestionario(Cuestionario cuestionario)
        {
            await _cuestionarioRepository.CrearCuestionario(cuestionario);
        }

        public async Task EliminarCuestionario(Cuestionario cuestionario)
        {
            await _cuestionarioRepository.EliminarCuestionario(cuestionario);
        }

        public async Task<Cuestionario> GetCuestionario(int idCuestionario)
        {
            return await _cuestionarioRepository.GetCuestionario(idCuestionario);
        }

        public async Task<List<Cuestionario>> GetCuestionarioList()
        {
            return await _cuestionarioRepository.GetCuestionarioList();
        }

        public async Task<List<Cuestionario>> GetListCuestionarioByUser(int ideUser)
        {
            return await _cuestionarioRepository.GetListCuestionarioByUser(ideUser);
        }
    }
}
