using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;
using MvvmCross;
using MvvmCross.Plugin.Network.Rest;

namespace DahlexApp.Services
{
    public class WebApiService : IWebApiService
    {
        //private readonly IConfigurationService _configurationService;
        private readonly Uri _apiBaseUrl;

        public WebApiService(IConfigurationService configurationService)
        {
            _apiBaseUrl = new Uri(configurationService.ApiBaseUrl);
        }

        public async Task<TestThing> MakeTestCall()
        {
            //            var r = await _http.GetStringAsync(new Uri(_apiBaseUrl, relative)).ConfigureAwait

            string relative = "test";
            var uri = new Uri(_apiBaseUrl, relative);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            var request = new MvxRestRequest(uri, "GET");
            var client = Mvx.IoCProvider.Resolve<IMvxRestClient>();
            var resp = await client.MakeStreamRequestAsync(request, cts.Token);
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                return null;
            }
            else
            {
                return null;
            }

            
        }
    }
}