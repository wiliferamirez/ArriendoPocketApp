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
            return await _httpClient.GetFromJsonAsync<List<Propiedad>>("Propiedades") ?? new List<Propiedad>();
        }

        public async Task<(bool Success, string Content)> AgregarPropiedadAsync(Propiedad propiedad)
        {
            var response = await _httpClient.PostAsJsonAsync("Propiedades", propiedad);
            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"STATUS: {response.StatusCode}");
            Debug.WriteLine($"RESPUESTA: {content}");
            return (response.IsSuccessStatusCode, content);
        }

        public async Task<bool> EliminarPropiedadAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Propiedades/{id}");
            return response.IsSuccessStatusCode;
        }

        // PropiedadService.cs
        public async Task<Propiedad> GetPropiedadAsync(int id)
        {
            // GET /Propiedades/{id}
            var resp = await _httpClient.GetAsync($"Propiedades/{id}");
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Propiedad>();
        }

        public async Task<(bool Success, string Content)> ActualizarPropiedadAsync(Propiedad propiedad)
        {
            // PUT /Propiedades/{id}
            var response = await _httpClient.PutAsJsonAsync(
                $"Propiedades/{propiedad.PropiedadID}", propiedad);
            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"STATUS: {response.StatusCode}");
            Debug.WriteLine($"RESPUESTA: {content}");
            return (response.IsSuccessStatusCode, content);
        }



    }
}
