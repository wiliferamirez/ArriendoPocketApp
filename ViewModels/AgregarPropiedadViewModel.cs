using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace ArriendoPocketApp.ViewModels
{
    public class AgregarPropiedadViewModel : ObservableObject
    {
        private readonly PropiedadService _service;
        private readonly LogService _logService;

        public AgregarPropiedadViewModel(PropiedadService service, LogService logService)
        {
            _service = service;
            _logService = logService;
        }

        // Propiedades enlazadas al XAML
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
            set => SetProperty(ref _mensaje, value);
        }

        private bool _hayError;
        public bool HayError
        {
            get => _hayError;
            set => SetProperty(ref _hayError, value);
        }

        public ICommand GuardarCommand => new Command(async () => await EjecutarGuardarAsync());

        private async Task EjecutarGuardarAsync()
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

            try
            {
                var (resultado, content) = await _service.AgregarPropiedadAsync(nueva);

                if (resultado)
                {
                    // Log de éxito
                    await _logService.AddAsync(new LogEntry
                    {
                        Timestamp = DateTime.UtcNow,
                        Level = "Info",
                        Message = $"Propiedad '{nueva.AliasPropiedad}' creada",
                        Endpoint = "POST /Propiedades",
                        Payload = JsonSerializer.Serialize(nueva)
                    });

                    HayError = false;
                    Mensaje = "";
                    await Shell.Current.DisplayAlert("Éxito", "Propiedad registrada correctamente", "OK");
                    await Shell.Current.GoToAsync("//propiedades");
                }
                else
                {
                    // Log de fallo con detalles del error
                    await _logService.AddAsync(new LogEntry
                    {
                        Timestamp = DateTime.UtcNow,
                        Level = "Error",
                        Message = "Validación falló al crear propiedad",
                        Endpoint = "POST /Propiedades",
                        Payload = content
                    });

                    HayError = true;
                    await Shell.Current.DisplayAlert("Error al registrar", content, "OK");
                }
            }
            catch (Exception ex)
            {
                // Log de excepción
                await _logService.AddAsync(new LogEntry
                {
                    Timestamp = DateTime.UtcNow,
                    Level = "Error",
                    Message = $"Excepción en registro de propiedad: {ex.Message}",
                    Endpoint = "POST /Propiedades",
                    Payload = JsonSerializer.Serialize(nueva)
                });

                HayError = true;
                Mensaje = "Error al registrar la propiedad: " + ex.Message;
            }
        }
    }
}
