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

        public ObservableCollection<Propiedad> ListaPropiedades { get;  }
        public PropiedadesViewModel(PropiedadService propiedadService)
        {
            _service = propiedadService;
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
            var propiedades = await _service.GetPropiedadesAsync();
            ListaPropiedades.Clear();
            foreach (var p in propiedades)
                ListaPropiedades.Add(p);
        }
    }
}
