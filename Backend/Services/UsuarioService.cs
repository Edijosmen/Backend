using Backend.Domain.IRepository;
using Backend.Domain.IServices;
using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class UsuarioService:IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task ChangePassword(Usuario usuario, string newPassword)
        {
            await _usuarioRepository.ChangePassword(usuario, newPassword);

        }

        public async Task SaveUser(Usuario usuario, string password)
        {
            await _usuarioRepository.SaveUser(usuario,password);
        }

        public async Task<bool> ValidateExistence(Usuario usuario)
        {
            
            return await _usuarioRepository.ValidateExistence(usuario);
        }

        public async Task<Usuario> ValidatePassword(int idUsuario, string beforePassword)
        {
            var user = await _usuarioRepository.ValidatePassword(idUsuario, beforePassword);

            return user;
        }
    }
}
