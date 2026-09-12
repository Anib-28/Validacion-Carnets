using System;
using System.Collections.Generic;
using System.Text;

namespace CarnetEstudiantil.Persistencia.Models
{
    public class Carnet
    {
        public int IdCarnet { get; set; }

        public int IdEstudiante { get; set; }

        public string CodigoCarnet { get; set; } = string.Empty;

        public string CodigoQR { get; set; } = string.Empty;

        public DateTime FechaEmision { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public string Estado { get; set; } = string.Empty;

        // Relación con Estudiante
        public Estudiante? Estudiante { get; set; }
    }
}   
