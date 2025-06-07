using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Services;

namespace ArriendoPocketApp.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService = new();

        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;

        private string _mensaje;
        public string Mensaje
        {
            get => _mensaje;
            set { _mensaje = value; OnPropertyChanged(); }
        }

        private bool _hayError;
        public bool HayError
        {
            get => _hayError;
            set { _hayError = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand => new Command(async () => await RealizarRegistro());
        public ICommand IrALoginCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("//login");
        });


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        private async Task RealizarRegistro()
        {
            var model = new RegisterModel
            {
                CedulaArrendatario = Cedula,
                NombreArrendatario = Nombre,
                ApellidoArrendatario = Apellido,
                CorreoArrendatario = Correo,
                TelefonoArrendatario = Telefono,
                FechaNacimientoArrendatario = FechaNacimiento,
                Contrasena = Contrasena,
                ConfirmarContrasena = ConfirmarContrasena
            };

            var resultado = await _authService.RegisterAsync(model);

            if (resultado.Contains("exitoso"))
            {
                HayError = false;
                Mensaje = string.Empty;
                await Shell.Current.DisplayAlert("Listo", "Registrado correctamente", "OK");
                await Shell.Current.GoToAsync("//login");
            }
            else
            {
                HayError = true;
                Mensaje = resultado;
            }
        }
    }
}
