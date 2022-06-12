using Backend.Domain.IServices;
using Backend.Domain.Models.Dtos;
using Backend.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _config;
        public LoginController(ILoginService loginService, IConfiguration config)
        {
            _loginService = loginService;
            _config = config;
        }

        [HttpPost]

        public async Task<IActionResult> Post(UsuarioAuthDto usuarioAuth)
        {
            var usuario = await _loginService.ValidateUser(usuarioAuth.NombreUsuario,usuarioAuth.Password);
            if (usuario == null)
            {
               return BadRequest(new { message = "contraseñas no valida!" });
            }
            string tokenString = JwtConfiguration.GetToken(usuario, _config);
            return Ok(new {message="contraseñas validas!!" , tokenString });
        }
    }
}
