using Backend.Domain.IServices;
using Backend.Domain.Models;
using Backend.Domain.Models.Dtos;
using Backend.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServe;
        public UsuarioController(IUsuarioService usuarioServe)
        {
            _usuarioServe = usuarioServe;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioAuthDto usuarioAutDto)
        {
            try
            {
                Usuario usuario = new()
                {
                    NombreUsuario = usuarioAutDto.NombreUsuario
                };
                var validateExistence = await _usuarioServe.ValidateExistence(usuario);
                if (validateExistence)
                {
                    return BadRequest(new { menssage = "el usuario " + usuario.NombreUsuario.ToUpper() + " ya esxiste" });
                }
                await _usuarioServe.SaveUser(usuario, usuarioAutDto.Password);
                var response = new
                {
                    statusCode = 200,
                    message = "Registro Exitoso"
                };
                return Ok(new { response });
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [Route("changePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public  async Task<IActionResult> ChangePassword(ChangePasswordAuthDto changePassword)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                 int ID = JwtConfiguration.GetTokenIduser(identity);
                var usuario = await _usuarioServe.ValidatePassword(ID,changePassword.BeforePassword);
               
                if (usuario == null)
                {
                    return BadRequest(new { message = "password incorrecta!!" });
                }
                await _usuarioServe.ChangePassword(usuario,changePassword.NewPassword);
                return Ok(new { message="Password fue cambiada con exito!" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
