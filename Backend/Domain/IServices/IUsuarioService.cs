using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.IServices
{
    public interface IUsuarioService
    {
        Task SaveUser(Usuario usuario, string password);
        Task<bool> ValidateExistence(Usuario usuario);
        Task ChangePassword(Usuario usuario, string newPassword);
        Task<Usuario> ValidatePassword(int idUsuario, string beforePassword);
    }
}
