namespace CarnetEstudiantil.Api.DTOs
{
    public class LoginResponseDto
    {
        public bool Exitoso { get; set; }

        public string Mensaje { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public int IdUsuario { get; set; }

        public string Correo { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;
    }
}