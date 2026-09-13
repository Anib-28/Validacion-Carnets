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

            if (carnet == null)
            {
                return NotFound(new CarnetResponseDto
                {
                    Valido = false,
                    Mensaje = "Carnet no encontrado"
                });
            }

            if (carnet.Estudiante == null)
            {
                return NotFound(new CarnetResponseDto
                {
                    Valido = false,
                    Mensaje = "El carnet no tiene un estudiante asociado"
                });
            }

            var respuesta = new CarnetResponseDto
            {
                Valido = carnet.Estado == "ACTIVO",
                Mensaje = carnet.Estado == "ACTIVO"
                    ? "Carnet válido"
                    : "Carnet no válido",

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