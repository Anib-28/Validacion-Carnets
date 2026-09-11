using Microsoft.AspNetCore.Mvc;
using Backend.Api.Models;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarnetsController : ControllerBase
    {
        [HttpGet("{codigo}")]
        public IActionResult ObtenerCarnet(string codigo)
        {
            var carnet = new Carnet
            {
                Id = 1,
                Codigo = "0001",
                Nombre = "Widinson Guaraca",
                Carrera = "Ingeniería en TI",
                Estado = "Activo"
            };
            if(codigo != carnet.Codigo)
            {
                return NotFound(new
                {
                    mensaje = "Carnet no encontrado",
                    valido = false
                });
            }
            return Ok(new
            {
                mensaje = "Carnet encontrado",
                valido = true,
                carnet
            });
        }
    }
}
