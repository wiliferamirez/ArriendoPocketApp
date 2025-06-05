using System.Net.Http.Json;
using ArriendoPocketApp.Models;

namespace ArriendoPocketApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://TU_API_URL/api/auth/")
            };
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("register", model);
            return response.IsSuccessStatusCode
                ? "Registro exitoso"
                : $"Error: {await response.Content.ReadAsStringAsync()}";
        }

        public async Task<string> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("login", model);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsStringAsync()
                : "Login fallido";
        }
    }
}
