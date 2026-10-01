using CarnetEstudiantil.Api.DTOs;

using CarnetEstudiantil.Persistencia.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarnetEstudiantil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CarnetsController : ControllerBase
    {
        private readonly ICarnetRepository _carnetRepository;

        public CarnetsController(ICarnetRepository carnetRepository)
        {
            _carnetRepository = carnetRepository;
        }

        // ============================================================
        // CARNET DEL ESTUDIANTE AUTENTICADO
        // ============================================================

        [HttpGet("mi-carnet")]
        [Authorize(Roles = "ESTUDIANTE")]
        public async Task<IActionResult> ObtenerMiCarnet()
        {
            var idUsuarioClaim =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(idUsuarioClaim, out var idUsuario))
            {
                return Unauthorized(new
                {
                    mensaje = "No se pudo identificar al usuario."
                });
            }

            var carnet =
                await _carnetRepository.ObtenerPorIdUsuario(idUsuario);

            if (carnet == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró un carnet para este estudiante."
                });
            }

            if (carnet.Estudiante == null)
            {
                return NotFound(new
                {
                    mensaje = "El carnet no tiene información del estudiante."
                });
            }

            return Ok(new CarnetResponseDto
            {
                Valido = true,
                Mensaje = "Carnet encontrado correctamente.",
                IdCarnet = carnet.IdCarnet,
                CodigoCarnet = carnet.CodigoCarnet,
                CodigoQR = carnet.CodigoQR,
                FechaEmision = carnet.FechaEmision,
                FechaExpiracion = carnet.FechaExpiracion,
                Estado = carnet.Estado,

                Estudiante = new EstudianteCarnetDto
                {
                    IdEstudiante = carnet.Estudiante.IdEstudiante,
                    Cedula = carnet.Estudiante.Cedula,
                    Nombres = carnet.Estudiante.Nombres,
                    Apellidos = carnet.Estudiante.Apellidos,
                    Carrera = carnet.Estudiante.Carrera,
                    Semestre = carnet.Estudiante.Semestre,
                    FotoUrl = carnet.Estudiante.FotoUrl,
                    Estado = carnet.Estudiante.Estado
                }
            });
        }

        // ============================================================
        // VERIFICAR CARNET MEDIANTE CÓDIGO QR
        // ============================================================

        [HttpGet("{codigoQR}")]
        [Authorize(Roles = "ESTUDIANTE,VALIDADOR,ADMIN")]
        public async Task<IActionResult> ObtenerCarnet(string codigoQR)
        {
            var carnet =
                await _carnetRepository.ObtenerCodigoQR(codigoQR);

            if (carnet == null)
            {
                return NotFound(new
                {
                    mensaje = "Carnet no encontrado."
                });
            }

            if (carnet.Estudiante == null)
            {
                return NotFound(new
                {
                    mensaje = "El carnet no tiene información del estudiante."
                });
            }

            return Ok(new CarnetResponseDto
            {
                Valido = true,
                Mensaje = "Carnet encontrado correctamente.",
                IdCarnet = carnet.IdCarnet,
                CodigoCarnet = carnet.CodigoCarnet,
                CodigoQR = carnet.CodigoQR,
                FechaEmision = carnet.FechaEmision,
                FechaExpiracion = carnet.FechaExpiracion,
                Estado = carnet.Estado,

                Estudiante = new EstudianteCarnetDto
                {
                    IdEstudiante = carnet.Estudiante.IdEstudiante,
                    Cedula = carnet.Estudiante.Cedula,
                    Nombres = carnet.Estudiante.Nombres,
                    Apellidos = carnet.Estudiante.Apellidos,
                    Carrera = carnet.Estudiante.Carrera,
                    Semestre = carnet.Estudiante.Semestre,
                    FotoUrl = carnet.Estudiante.FotoUrl,
                    Estado = carnet.Estudiante.Estado
                }
            });
        }
    }
}