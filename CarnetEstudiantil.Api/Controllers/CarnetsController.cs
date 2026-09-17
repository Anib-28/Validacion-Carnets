using CarnetEstudiantil.Api.DTOs;
using CarnetEstudiantil.Persistencia.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarnetEstudiantil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarnetsController : ControllerBase
    {
        private readonly ICarnetRepository _carnetRepository;
        public CarnetsController(ICarnetRepository carnetRepository)
        {
            _carnetRepository = carnetRepository;
        }
        [HttpGet("{codigoQR}")]
        public async Task<IActionResult> ObtenerCarnet(string codigoQR)
        {
            var carnet = await _carnetRepository.ObtenerCodigoQR(codigoQR);
            // Si el código QR no existe en la base de datos
            if (carnet == null)
            {
                return NotFound(new CarnetResponseDto
                {
                    Valido = false,
                    Mensaje = "Carnet no encontrado"
                });
            }
            // Si existe, el carnet está registrado en el sistema
            if (carnet.Estudiante == null)
            {
                return NotFound(new CarnetResponseDto
                {
                    Valido = false,
                    Mensaje = "Carnet no válido"
                });
            }
            var respuesta = new CarnetResponseDto
            {
                Valido = true,
                Mensaje = "Carnet auténtico",

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
            };

            return Ok(respuesta);
        }
    }
}       