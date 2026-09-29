namespace CarnetEstudiantil.Persistencia.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        // Usuario utilizado para iniciar sesión
        public string Correo { get; set; } = string.Empty;

        // IMPORTANTE:
        // Este campo almacenará el HASH de la contraseña,
        // nunca la contraseña en texto plano.
        public string Contrasena { get; set; } = string.Empty;

        // Ejemplo: Estudiante, Docente, Guardia, Administrador
        public string Rol { get; set; } = string.Empty;

        // Ejemplo: Activo, Inactivo
        public string Estado { get; set; } = string.Empty;

        public Estudiante? Estudiante { get; set; }
    }
}