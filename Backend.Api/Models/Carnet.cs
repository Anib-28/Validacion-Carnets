namespace Backend.Api.Models
{
    public class Carnet
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

    }
}
