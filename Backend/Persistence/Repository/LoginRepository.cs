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
    public class LoginRepository:ILoginRepository
    {
        private readonly AplicationDbContext _dbContext;
        public LoginRepository(AplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> ValidateUser(string usuario, string password)
        {
            var isUser = await _dbContext.Usuario.Where(x => x.NombreUsuario == usuario).FirstOrDefaultAsync();
            if (isUser != null)
            {
                var isPassword = UsuarioRepository.CheckPassword(password, isUser.PasswordHash, isUser.PasswordSalt);
                if (isPassword == true)
                {
                    return isUser;
                }
            }
            return null;
           
        }
    }
}
