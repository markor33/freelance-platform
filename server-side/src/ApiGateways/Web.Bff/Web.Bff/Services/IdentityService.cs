using FluentResults;
using System.Text.Json;

namespace Web.Bff.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;

        public IdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<string>> LoginAsync(string username, string password)
        {
            var requestBody = new Dictionary<string, string>
            {
                    { "username", username },
                    { "password", password },
                    { "grant_type", "password" },
                    { "client_id", "angular-app" },
                    { "client_secret", "secret" }
            };
            var content = new FormUrlEncodedContent(requestBody);

            var response = await _httpClient.PostAsync("http://host.docker.internal:50000/connect/token", content);
            if (!response.IsSuccessStatusCode)
                return Result.Fail("Login failed");

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);
            var accessToken = responseObject["access_token"].ToString();

            return Result.Ok(accessToken);
        }

    }
}
