using System;
using System.Collections.Generic;
using System.Text;
using CarnetEstudiantil.Persistencia.Models;

namespace CarnetEstudiantil.Persistencia.Repositories
{
    public interface ICarnetRepository
    {
        Task<Carnet?> ObtenerCodigoQR(string codigoQR);
    }
}
