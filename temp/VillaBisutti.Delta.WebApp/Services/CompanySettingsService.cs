using Microsoft.Extensions.Options;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Services
{
    public class CompanySettingsService
    {
        private readonly IConfiguration _configuration;

        public CompanySettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CompanySettings GetSettings()
        {
            var settings = _configuration.GetSection("CompanySettings").Get<CompanySettings>();
            return settings ?? new CompanySettings 
            { 
                Name = "Sistema de Eventos",
                ShortName = "Eventos",
                Logo = "/img/logo.png",
                Theme = "default"
            };
        }
    }
}
