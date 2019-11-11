using DahlexApp.Logic.Interfaces;

namespace DahlexApp.Logic.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        public string ApiBaseUrl { get; set; } = @"https://10.11.11.114:5000/api/v1/";

    }
}