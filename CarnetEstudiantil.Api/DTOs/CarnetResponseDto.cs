namespace CarnetEstudiantil.Api.DTOs
{
    public class CarnetResponseDto
    {
        public bool Valido { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public int IdCarnet { get; set; }
        public string CodigoCarnet { get; set; } = string.Empty;
        public string CodigoQR { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public EstudianteCarnetDto? Estudiante { get; set; }
    }
}