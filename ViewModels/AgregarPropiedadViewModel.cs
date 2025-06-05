using System.Windows.Input;
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Services;

namespace ArriendoPocketApp.ViewModels
{
    public class AgregarPropiedadViewModel : BaseViewModel
    {
        private readonly PropiedadService _service = new();

        public string AliasPropiedad { get; set; }
        public string DireccionPropiedad { get; set; }
        public string CiudadUbicacion { get; set; }
        public string NombreInquilino { get; set; }

        public int NumeroHabitaciones { get; set; }
        public decimal CanonArrendatario { get; set; }
        public int MesesGarantia { get; set; }
        public int NumeroPisos { get; set; }
        public decimal AreaConstruccion { get; set; }

        public DateTime FechaConstruccion { get; set; } = DateTime.Now;
        public bool Disponible { get; set; } = true;

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

        public ICommand GuardarCommand => new Command(async () =>
        {
            var nueva = new Propiedad
            {
                AliasPropiedad = AliasPropiedad,
                DireccionPropiedad = DireccionPropiedad,
                CiudadUbicacion = CiudadUbicacion,
                NombreInquilino = NombreInquilino,
                NumeroHabitaciones = NumeroHabitaciones,
                CanonArrendatario = CanonArrendatario,
                MesesGarantia = MesesGarantia,
                NumeroPisos = NumeroPisos,
                AreaConstruccion = AreaConstruccion,
                FechaConstruccion = FechaConstruccion,
                Disponible = Disponible,
                FechaCreacion = DateTime.Now
            };

            bool resultado = await _service.AgregarPropiedadAsync(nueva);

            if (resultado)
            {
                await Shell.Current.DisplayAlert("Éxito", "Propiedad registrada correctamente", "OK");
                await Shell.Current.GoToAsync("//propiedades");
            }
            else
            {
                Mensaje = "No se pudo registrar la propiedad.";
                HayError = true;
            }
        });
    }
}
