using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Services;
using Microsoft.Maui.Storage;

namespace ArriendoPocketApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService = new();

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
        }

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

        private async Task RealizarLogin()
        {
            var modelo = new LoginModel
            {
                Correo = this.Correo,
                Contrasena = this.Contrasena,
                RememberMe = this.Recordarme
            };

            try
            {
                var (success, json) = await _authService.LoginAsync(modelo);

                if (success)
                {
                    var datos = JsonSerializer.Deserialize<LoginResponse>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    Preferences.Set("ArrendatarioID", datos.ArrendatarioID);
                    Preferences.Set("NombreArrendatario", datos.NombreArrendatario);

                    HayError = false;
                    Mensaje = "";

                    await Shell.Current.GoToAsync("//propiedades");
                }
                else
                {
                    HayError = true;
                    Mensaje = "Login fallido: " + json;
                }
            }
            catch (Exception ex)
            {
                HayError = true;
                Mensaje = "Error de red: " + ex.Message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
