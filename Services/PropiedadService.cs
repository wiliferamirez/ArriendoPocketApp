using ArriendoPocketApp.Models;
using System.Diagnostics;
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
                BaseAddress = new Uri("http://192.168.0.143:5037/api/")
            };
        }

        public async Task<List<Propiedad>> GetPropiedadesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Propiedad>>("") ?? new List<Propiedad>();
        }
        public async Task<bool> AgregarPropiedadAsync(Propiedad propiedad)
        {
            var response = await _httpClient.PostAsJsonAsync("Propiedades", propiedad);

            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"STATUS: {response.StatusCode}");
            Debug.WriteLine($"RESPUESTA: {content}");

            return response.IsSuccessStatusCode;
        }


        public async Task<bool> EliminarPropiedadAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
