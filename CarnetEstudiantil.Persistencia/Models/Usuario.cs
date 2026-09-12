using System;
using System.Collections.Generic;
using System.Text;

namespace CarnetEstudiantil.Persistencia.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Correo { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;

        public Estudiante? Estudiante { get; set; }
    }
}
