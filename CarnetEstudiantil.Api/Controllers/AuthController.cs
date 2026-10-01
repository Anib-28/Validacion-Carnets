using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarnetEstudiantil.Api.DTOs;
using CarnetEstudiantil.Api.Services;
using CarnetEstudiantil.Persistencia.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarnetEstudiantil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public AuthController(
            AppDbContext context,
            PasswordService passwordService,
            IConfiguration configuration)
        {
            _context = context;
            _passwordService = passwordService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Correo) ||
                string.IsNullOrWhiteSpace(request.Contrasena))
            {
                return BadRequest(new LoginResponseDto
                {
                    Exitoso = false,
                    Mensaje = "Correo y contraseña son obligatorios."
                });
            }

            var correo = request.Correo.Trim();

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo);

            if (usuario == null)
            {
                return Unauthorized(new LoginResponseDto
                {
                    Exitoso = false,
                    Mensaje = "Correo o contraseña incorrectos."
                });
            }

            if (!string.Equals(
                    usuario.Estado,
                    "Activo",
                    StringComparison.OrdinalIgnoreCase))
            {
                return Unauthorized(new LoginResponseDto
                {
                    Exitoso = false,
                    Mensaje = "El usuario está inactivo."
                });
            }

            var contraseñaCorrecta =
                _passwordService.VerificarContraseña(
                    usuario,
                    request.Contrasena,
                    usuario.Contrasena);
            
            if (!contraseñaCorrecta)
            {
                return Unauthorized(new LoginResponseDto
                {
                    Exitoso = false,
                    Mensaje = "Correo o contraseña incorrectos."
                });
            }
            //Temporal para pruebas, se debe eliminar en producción
            Console.WriteLine(
                $"LOGIN DEBUG - Rol BD: [{usuario.Rol}]");

            var token = GenerarToken(usuario);

            return Ok(new LoginResponseDto
            {
                Exitoso = true,
                Mensaje = "Inicio de sesión correcto.",
                Token = token,
                IdUsuario = usuario.IdUsuario,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            });
        }

        //Prueba
        [HttpPost("restablecer-prueba")]
        public async Task<IActionResult> RestablecerPrueba(
    [FromQuery] string correo)
        {
            const string nuevaContrasena = "Prueba123!";

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo.Trim());

            if (usuario == null)
            {
                return NotFound(new
                {
                    mensaje = "Usuario no encontrado."
                });
            }

            usuario.Contrasena = _passwordService.HashearContraseña(
                usuario,
                nuevaContrasena);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = $"Contraseña de prueba actualizada para {usuario.Correo}."
            });
        }
        private string GenerarToken(
            CarnetEstudiantil.Persistencia.Models.Usuario usuario)
        {
            var jwtKey = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException(
                    "No se configuró Jwt:Key.");

            var jwtIssuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException(
                    "No se configuró Jwt:Issuer.");

            var jwtAudience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException(
                    "No se configuró Jwt:Audience.");

            var expiresMinutes = _configuration
                .GetValue<int>("Jwt:ExpiresMinutes");

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.IdUsuario.ToString()),

                new Claim(
                    ClaimTypes.Email,
                    usuario.Correo),

                new Claim(
                    ClaimTypes.Role,
                    usuario.Rol.Trim())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}