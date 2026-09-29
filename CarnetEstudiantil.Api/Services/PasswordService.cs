using CarnetEstudiantil.Persistencia.Models;
using Microsoft.AspNetCore.Identity;

namespace CarnetEstudiantil.Api.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public string HashearContraseña(Usuario usuario, string contraseña)
        {
            return _passwordHasher.HashPassword(usuario, contraseña);
        }

        public bool VerificarContraseña(
            Usuario usuario,
            string contraseña,
            string hash)
        {
            var resultado = _passwordHasher.VerifyHashedPassword(
                usuario,
                hash,
                contraseña
            );

            return resultado == PasswordVerificationResult.Success ||
                   resultado == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}