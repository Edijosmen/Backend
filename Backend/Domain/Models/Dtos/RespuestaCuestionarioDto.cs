using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Models.Dtos
{
    public class RespuestaCuestionarioDto
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string NombreParticipante { get; set; }
       
        public int CuestionarioId { get; set; }
      
        public List<RespuestaCuestionarioDetalle> ListaCuestionarioDetalle { get; set; }
    }
}
