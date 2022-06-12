using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Persistence.Context
{
    public class AplicationDbContext:DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options):base(options)
        {

        }
        public DbSet<Usuario> Usuario { set; get; }
        public DbSet<Cuestionario> Cuestionario { get; set; }
        public DbSet<Pregunta> Pregunta { get; set; }
        public DbSet<Respuesta> Respuesta { get; set; }
        public DbSet<RespuestaCuestionario> RespuestaCuestionarios { get; set; }
        public DbSet<RespuestaCuestionarioDetalle> RespuestaCuestionarioDetalles { get; set; }
    }
}
