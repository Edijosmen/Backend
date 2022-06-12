using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.Models.Dtos
{
    public class UsuarioAuthDto
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Password { get; set; }
    }
}
