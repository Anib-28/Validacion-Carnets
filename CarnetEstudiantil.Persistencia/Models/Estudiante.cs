using System;
using System.Collections.Generic;
using System.Text;

namespace CarnetEstudiantil.Persistencia.Models
{
    public class Estudiante
    {
        public int IdEstudiante { get; set; }

        public int IdUsuario { get; set; }

        public string Cedula { get; set; } = string.Empty;

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string Carrera { get; set; } = string.Empty;

        public int Semestre { get; set; }

        public string? FotoUrl { get; set; }

        public string Estado { get; set; } = string.Empty;

        // Relación con Usuario
        public Usuario? Usuario { get; set; }

        // Relación con Carnet
        public Carnet? Carnet { get; set; }
    }
}   
