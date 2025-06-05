using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Services;
using System.Threading.Tasks;

namespace ArriendoPocketApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService = new AuthService();

        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public bool Recordarme { get; set; }

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

        public ICommand LoginCommand => new Command(async () => await RealizarLogin());
        public ICommand IrARegistroCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("//register");
        });


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        private async Task RealizarLogin()
        {
            var modelo = new LoginModel
            {
                Correo = this.Correo,
                Contrasena = this.Contrasena,
                RememberMe = this.Recordarme
            };

            var resultado = await _authService.LoginAsync(modelo);

            if (resultado.Contains("Login fallido") || resultado.Contains("incorrecto"))
            {
                Mensaje = resultado;
                HayError = true;
            }
            else
            {
                Mensaje = string.Empty;
                HayError = false;
                
                await Shell.Current.GoToAsync("//propiedades");
            }
        }
    }
}
