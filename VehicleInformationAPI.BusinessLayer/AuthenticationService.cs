using System.Net.Http.Headers;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer
{
    public class AuthenticationService(ILogger<IAuthenticationService> logger, HttpClient httpClient) : IAuthenticationService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILogger<IAuthenticationService> _logger = logger;

        public async Task<string> GetAuthentication(string clientSec)
        {
            string url = @"https://login.microsoftonline.com/c4779dd6-d94f-4813-a8f9-a570fa0d8706/oauth2/v2.0/token";

            var formValues = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", "caad0062-176f-4150-8160-50c2050a5332" },
                { "client_secret", clientSec },
                { "scope", "api://caad0062-176f-4150-8160-50c2050a5332/.default" }
            });

            _httpClient.BaseAddress = new Uri(url);

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var tmp = _httpClient.PostAsync(_httpClient.BaseAddress, formValues).Result;

                if (tmp.IsSuccessStatusCode)
                {
                    var result = await tmp.Content.ReadFromJsonAsync<AuthorizationResponse>();
                    string token = string.Empty;

                    if (result != null)
                    {
                        if (result.token_type == "Bearer")
                        {
                            token = "Bearer: " + result.access_token;
                        }
                    }

                    return token;
                }

                else
                {
                    _logger.LogError("Can't retrieve bearer token");
                    return string.Empty;
                }
            }
            catch (Exception err)
            {
                _logger.LogError($"\"Can't retrieve bearer token {err}");
                throw new Exception($"\"Can't retrieve bearer token {err}");
            }
        }
    }
}
