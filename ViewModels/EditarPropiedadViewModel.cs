// ViewModels/EditarPropiedadViewModel.cs
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArriendoPocketApp.ViewModels
{
    public class EditarPropiedadViewModel : ObservableObject, IQueryAttributable
    {
        readonly PropiedadService _service;
        readonly LogService _logService;

        public EditarPropiedadViewModel(PropiedadService service, LogService logService)
        {
            _service = service;
            _logService = logService;
            GuardarCambiosCommand = new AsyncRelayCommand(EjecutarGuardarAsync);
        }

        // Campos enlazados al XAML
        public int PropiedadID { get; set; }
        public string AliasPropiedad { get; set; }
        public string DireccionPropiedad { get; set; }
        public string CiudadUbicacion { get; set; }
        public string NombreInquilino { get; set; }
        public int NumeroHabitaciones { get; set; }
        public decimal CanonArrendatario { get; set; }
        public int MesesGarantia { get; set; }
        public int NumeroPisos { get; set; }
        public decimal AreaConstruccion { get; set; }
        public DateTime FechaConstruccion { get; set; }
        public bool Disponible { get; set; }

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

        public IAsyncRelayCommand GuardarCambiosCommand { get; }

        // Recibe el parámetro de la ruta: propiedadId
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("propiedadId", out var obj)
                && int.TryParse(obj?.ToString(), out var id))
            {
                PropiedadID = id;
                await CargarPropiedadAsync(id);
            }
        }

        private async Task CargarPropiedadAsync(int id)
        {
            var p = await _service.GetPropiedadAsync(id);
            AliasPropiedad = p.AliasPropiedad;
            DireccionPropiedad = p.DireccionPropiedad;
            CiudadUbicacion = p.CiudadUbicacion;
            NombreInquilino = p.NombreInquilino;
            NumeroHabitaciones = p.NumeroHabitaciones;
            CanonArrendatario = p.CanonArrendatario;
            MesesGarantia = p.MesesGarantia;
            NumeroPisos = p.NumeroPisos;
            AreaConstruccion = p.AreaConstruccion;
            FechaConstruccion = p.FechaConstruccion;
            Disponible = p.Disponible;
            // Notificar cambios
            OnPropertyChanged(string.Empty);
        }

        private async Task EjecutarGuardarAsync()
        {
            var edit = new Propiedad
            {
                PropiedadID = PropiedadID,
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
                Disponible = Disponible
            };

            try
            {
                var (success, content) = await _service.ActualizarPropiedadAsync(edit);

                if (success)
                {
                    await _logService.AddAsync(new LogEntry
                    {
                        Timestamp = DateTime.UtcNow,
                        Level = "Info",
                        Message = $"Propiedad {PropiedadID} actualizada",
                        Endpoint = $"PUT /Propiedades/{PropiedadID}",
                        Payload = JsonSerializer.Serialize(edit),
                        UserId = null
                    });
                    await Shell.Current.DisplayAlert("Éxito", "Propiedad actualizada", "OK");
                    await Shell.Current.GoToAsync("//propiedades");
                }
                else
                {
                    await _logService.AddAsync(new LogEntry
                    {
                        Timestamp = DateTime.UtcNow,
                        Level = "Error",
                        Message = $"Error al actualizar propiedad {PropiedadID}",
                        Endpoint = $"PUT /Propiedades/{PropiedadID}",
                        Payload = content,
                        UserId = null
                    });
                    HayError = true;
                    Mensaje = content;
                }
            }
            catch (Exception ex)
            {
                await _logService.AddAsync(new LogEntry
                {
                    Timestamp = DateTime.UtcNow,
                    Level = "Error",
                    Message = $"Excepción al editar propiedad: {ex.Message}",
                    Endpoint = $"PUT /Propiedades/{PropiedadID}",
                    Payload = JsonSerializer.Serialize(edit)
                });
                HayError = true;
                Mensaje = ex.Message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
