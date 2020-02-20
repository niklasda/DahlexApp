
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DahlexApp.Logic.Configuration;
using DahlexApp.Logic.Interfaces;

namespace DahlexApp.Logic.Services
{
    public class GameService : IGameService
    {
        public GameService(IConfigurationService apiConfigurationService)
        {
            _apiBaseUrl = new Uri(apiConfigurationService.ApiBaseUrl);

            _http.DefaultRequestHeaders.Add("Caller", AuthSettings.AppName);

            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentJson));
        }

        private readonly Uri _apiBaseUrl;
        private readonly HttpClient _http = new HttpClient();
        private const string ContentJson = "application/json";

        public void SetToken(string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
        }

        public async Task<string> GetTest()
        {

            try
            {
                string relative = $"Test";
                
                var r = await _http.GetStringAsync(new Uri(_apiBaseUrl, relative)).ConfigureAwait(false);
                return r;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
