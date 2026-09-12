using CarnetEstudiantil.Persistencia.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarnetEstudiantil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PruebaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PruebaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("conexion")]
        public async Task<IActionResult> ProbarConexion()
        {
            try
            {
                bool conexion = await _context.Database.CanConnectAsync();

                if (conexion)
                {
                    return Ok(new
                    {
                        mensaje = "Conexión exitosa con SQL Server",
                        baseDatos = "CarnetEstudiantil",
                        conectado = true
                    });
                }

                return BadRequest(new
                {
                    mensaje = "No se pudo conectar con SQL Server",
                    conectado = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al conectar con la base de datos",
                    detalle = ex.Message
                });
            }
        }
    }
}