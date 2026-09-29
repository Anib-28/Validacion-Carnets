namespace CarnetEstudiantil.Api.DTOs
{
    public class LoginRequestDto
    {
        public string Correo { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;
    }
}