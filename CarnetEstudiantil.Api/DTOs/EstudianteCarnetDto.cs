namespace CarnetEstudiantil.Api.DTOs
{
    public class EstudianteCarnetDto
    {
        public int IdEstudiante { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public int Semestre { get; set; }
        public string? FotoUrl { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}