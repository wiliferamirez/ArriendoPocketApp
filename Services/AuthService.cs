using System.Net.Http.Json;
using ArriendoPocketApp.Models;
using System.Net;

namespace ArriendoPocketApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://192.168.0.143:5037/api/Auth/")
            };
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("register", model);
            if (response.IsSuccessStatusCode)
            {
                return "Registro exitoso";
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return $"Error: {errorContent}";
        }

        public async Task<(bool success, string responseBody)> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("login", model);
            var content = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, content);
        }
    }
}
