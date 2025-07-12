using System.Collections.ObjectModel;
using System.Windows.Input;
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Services;
using CommunityToolkit.Mvvm.Input;

namespace ArriendoPocketApp.ViewModels
{
    public class PropiedadesViewModel : BaseViewModel
    {
        private readonly PropiedadService _service;
        private readonly LogService _logService;

        public ObservableCollection<Propiedad> ListaPropiedades { get;  }
        public PropiedadesViewModel(PropiedadService propiedadService, LogService logService)
        {
            _service = propiedadService;
            _logService = logService;
            ListaPropiedades = new ObservableCollection<Propiedad>();
        }

        public ICommand IrACrearPropiedadCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("//crearpropiedad");
        });

        public ICommand EditarCommand => new Command<Propiedad>(async (propiedad) =>
        {
            await Shell.Current.GoToAsync($"editarpropiedad?propiedadId={propiedad.PropiedadID}");
        });

        public ICommand EliminarCommand => new Command<Propiedad>(async (propiedad) =>
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmar", "¿Eliminar esta propiedad?", "Sí", "No");
            if (!confirm) return;

            bool success = await _service.EliminarPropiedadAsync(propiedad.PropiedadID);
            if (success)
            {
                ListaPropiedades.Remove(propiedad);
                await Shell.Current.DisplayAlert("Éxito", "Propiedad eliminada correctamente.", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo eliminar la propiedad.", "OK");
            }
        });

        public async Task CargarPropiedades()
        {
            ListaPropiedades.Clear();
            var propiedades = await _service.GetPropiedadesAsync();
            foreach (var p in propiedades)
                ListaPropiedades.Add(p);

            await _logService.AddAsync(new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                Level = "Info",
                Message = $"Se cargaron {ListaPropiedades.Count} propiedades",
                Endpoint = "GET /Propiedades",
            });
        }
    }
}
