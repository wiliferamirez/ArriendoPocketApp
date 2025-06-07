using System.Windows.Input;
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Services;
using System;
using System.Diagnostics;
using Microsoft.Maui.Storage;

namespace ArriendoPocketApp.ViewModels
{
    public class AgregarPropiedadViewModel : BaseViewModel
    {
        private readonly PropiedadService _service = new();

        public string AliasPropiedad { get; set; }
        public string DireccionPropiedad { get; set; }
        public string CiudadUbicacion { get; set; }
        public string NombreInquilino { get; set; }

        public int NumeroHabitaciones { get; set; } = 1;
        public decimal CanonArrendatario { get; set; } = 0;
        public int MesesGarantia { get; set; } = 1;
        public int NumeroPisos { get; set; } = 1;
        public decimal AreaConstruccion { get; set; } = 0;

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
                HayError = false;
                Mensaje = "";
                await Shell.Current.DisplayAlert("Éxito", "Propiedad registrada correctamente", "OK");
                await Shell.Current.GoToAsync("//propiedades");
            }
            else
            {
                HayError = true;
                Mensaje = "No se pudo registrar la propiedad.";
            }
        });
    }
}
