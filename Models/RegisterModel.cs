namespace ArriendoPocketApp.Models
{
    public class RegisterModel
    {
        public string CedulaArrendatario { get; set; }
        public string NombreArrendatario { get; set; }
        public string ApellidoArrendatario { get; set; }
        public string CorreoArrendatario { get; set; }
        public string TelefonoArrendatario { get; set; }
        public DateTime FechaNacimientoArrendatario { get; set; }
        public string Contrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
    }
}
