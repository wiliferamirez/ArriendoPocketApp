using ArriendoPocketApp.Models;
using System.Net.Http.Json;

namespace ArriendoPocketApp.Services
{
    public class PropiedadService
    {
        private readonly HttpClient _httpClient;

        public PropiedadService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://192.168.0.143:5037/api/Propiedades/")
            };
        }

        public async Task<List<Propiedad>> GetPropiedadesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Propiedad>>("") ?? new List<Propiedad>();
        }
        public async Task<bool> AgregarPropiedadAsync(Propiedad propiedad)
        {
            var response = await _httpClient.PostAsJsonAsync("", propiedad);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarPropiedadAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
