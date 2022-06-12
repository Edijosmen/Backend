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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AplicationDbContext _DbContex;
        public UsuarioRepository(AplicationDbContext DbContex)
        {
            _DbContex = DbContex;
        }
        public async Task SaveUser(Usuario usuario ,string password)
        {
            byte[] passwordHash, passwordSalt;
            CrearPasswordEncript(out passwordHash, out passwordSalt, password);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            _DbContex.Add(usuario);
            await _DbContex.SaveChangesAsync();
        }

        public async Task<bool> ValidateExistence(Usuario usuario)
        {
            var validateExistence = await _DbContex.Usuario.AnyAsync(x => x.NombreUsuario == usuario.NombreUsuario);
            return validateExistence;
        }

        public async Task ChangePassword(Usuario usuario, string newPassword)
        {
            byte[] passwordHash, passwordSalt;
            CrearPasswordEncript(out passwordHash, out passwordSalt, newPassword);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            _DbContex.Usuario.Update(usuario);
            await _DbContex.SaveChangesAsync();
        }

        public async Task<Usuario> ValidatePassword(int idUsuario, string beforePassword)
        {
            var usuario = await _DbContex.Usuario.Where(x => x.Id == idUsuario).FirstOrDefaultAsync();
            if (!CheckPassword(beforePassword, usuario.PasswordHash, usuario.PasswordSalt))
            {
                return null;
            }
            return usuario;
        }



        private void CrearPasswordEncript(out byte[] passwordHash, out byte[] passwordSalt, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool  CheckPassword(string password, byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //var DATO = hashComputado.SequenceEqual(passwordHash);
                //return DATO;
                Console.WriteLine(hashComputado);
                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        
    }
}
