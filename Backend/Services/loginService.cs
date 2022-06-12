using Backend.Domain.IRepository;
using Backend.Domain.IServices;
using Backend.Domain.Models;
using Backend.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class loginService:ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public loginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<Usuario> ValidateUser(string usuario, string password)
        {
           return await _loginRepository.ValidateUser(usuario,password);
        }
    }
}
