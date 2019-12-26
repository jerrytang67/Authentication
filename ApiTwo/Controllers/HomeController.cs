using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace ApiTwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            //retrieve access token
            var serverClient = _httpClientFactory.CreateClient();
            var discovertyDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44343");

            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discovertyDocument.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "ApiOne"
            });

            //retrieve secret data
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("https://localhost:44356/secret");
            var content = await response.Content.ReadAsStringAsync();
            return Ok(new {access_token = tokenResponse.AccessToken, message = content});
        }
    }
}