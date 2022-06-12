using AutoMapper;
using Backend.Domain.IRepository;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaCuestionarioController : ControllerBase
    {
        private readonly ICuestionarioService _cuestionarioService;
        private readonly IRespuestaCuestionarioService _RCService;
        private readonly IMapper _mapper;
        public RespuestaCuestionarioController(IRespuestaCuestionarioService RCService, ICuestionarioService cuestionarioService, IMapper mapper)
        {
            _RCService = RCService;
            _cuestionarioService = cuestionarioService;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{idCuestionario}")]
        public async Task<ActionResult> GetListaCuestionarios(int idCuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfiguration.GetTokenIduser(identity);
                Console.WriteLine("iduser"+idUsuario);
                var listRespuestaCuestionario = await _RCService.GetRespuestaCuestionario(idCuestionario,idUsuario);
                return Ok(listRespuestaCuestionario);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("RespuestaDetalle/{idRCuestionario}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetListaCuestionarioDetalle(int idRCuestionario)
        {
            try
            {
                int idCuestionario = await _RCService.GetIdCuestionarioFromRespuestaCuest(idRCuestionario);
                if (idCuestionario == 0)
                {
                    return NotFound();
                }
                var cuestionario = await _cuestionarioService.GetCuestionario(idCuestionario);
                var listRespuestasdetalle = await _RCService.GetlistaRespuestaDetalle(idRCuestionario);
                var listaRespuestas = _mapper.Map<List<RespuestaCuestionarioDetalle>, List<ListaRespuestasDto>>(listRespuestasdetalle);
               
                return Ok(new { cuestionario = cuestionario, respuestas = listaRespuestas });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RespuestaCuestionario respuestaCuestionario)
        {
            try
            {
                await _RCService.CrearRespuestaCuestionario(respuestaCuestionario);
                return Ok();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id )
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfiguration.GetTokenIduser(identity);
                var RCuestionario =  await _RCService.BuscarRespuestaCuestionarioByID(id,idUsuario);
               
                if (RCuestionario == null) return BadRequest(new {messege=" No se encontro Resultados"});
                await _RCService.EliminarRespuestaCuestionario(RCuestionario);
                return Ok(new {message="Eliminado con exito!!"});
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
