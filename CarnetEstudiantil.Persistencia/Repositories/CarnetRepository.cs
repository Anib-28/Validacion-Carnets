using System;
using System.Collections.Generic;
using System.Text;
using CarnetEstudiantil.Persistencia.Context;
using CarnetEstudiantil.Persistencia.Models;
using Microsoft.EntityFrameworkCore;

namespace CarnetEstudiantil.Persistencia.Repositories
{
    public class CarnetRepository : ICarnetRepository
    {
        private readonly AppDbContext _context;
        public CarnetRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Carnet?> ObtenerCodigoQR(string codigoQR)
        {
            return await _context.Carnets
                .Include(c => c.Estudiante)
                .FirstOrDefaultAsync(c => c.CodigoQR == codigoQR);
        }
    }
}
