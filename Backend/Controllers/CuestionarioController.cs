using Backend.Domain.IServices;
using Backend.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Utils;
using Backend.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using Backend.Domain.Models.Dtos;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuestionarioController : ControllerBase
    {
        private readonly ICuestionarioService _cuestionarioService;
        private readonly IMapper _mapper;
        public CuestionarioController(ICuestionarioService cuestionarioService, IMapper mapper)
        {
            _cuestionarioService = cuestionarioService;
            _mapper = mapper;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] Cuestionario cuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfiguration.GetTokenIduser(identity);
                cuestionario.UsuarioId = idUsuario;
                cuestionario.Activo = 1;
                cuestionario.CFechaCreacion = DateTime.Now;
               await _cuestionarioService.CrearCuestionario(cuestionario);
                return Ok(new { message = "se creo correctamente" });
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("GetcuestinarioBy")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCuestionarioByUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int idUsuario = JwtConfiguration.GetTokenIduser(identity);
            List<Cuestionario> cuestionarios= await  _cuestionarioService.GetListCuestionarioByUser(idUsuario);
            return Ok(cuestionarios);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCuestionarioId(int id)
        {
            Cuestionario cuestionario = await _cuestionarioService.GetCuestionario(id);

            return Ok(cuestionario);
        }

        [HttpDelete("{idCuestionario}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int idCuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfiguration.GetTokenIduser(identity);
                var cuestionario = await _cuestionarioService.BuscarCuestionario(idCuestionario, idUsuario);
                if (cuestionario == null)
                {
                    return BadRequest(new { message = "no se encontro el cuestionario" });
                }

                await _cuestionarioService.EliminarCuestionario(cuestionario);
                return Ok(new { message = "cuestionario eliminado con exito" });
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("GetListCuestionario")]
        
        public async Task<IActionResult> GetListCuestionario()
        {
           var cuestionarios = await _cuestionarioService.GetCuestionarioList();
            var listCuestionario = _mapper.Map<List<Cuestionario>, List<CuestionarioDto>>(cuestionarios);
            return Ok(listCuestionario);
        }
    }
}
