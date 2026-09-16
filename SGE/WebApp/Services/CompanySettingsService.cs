using System.Text.Json;
using System.Text.Json.Nodes;

namespace SGE.Services
{
    public class CompanySettingsService
    {
        private readonly IConfiguration _configuration;
        private readonly string _configPath;

        public CompanySettingsService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _configPath = Path.Combine(environment.ContentRootPath, "appsettings.json");
        }

        public CompanySettings GetSettings()
        {
            return _configuration.GetSection("CompanySettings").Get<CompanySettings>();
        }

        public void SaveSettings(CompanySettings settings)
        {
            var jsonString = File.ReadAllText(_configPath);
            var jsonObj = JsonNode.Parse(jsonString);

            if (jsonObj is JsonObject root)
            {
                root["CompanySettings"] = JsonNode.Parse(JsonSerializer.Serialize(settings));
                File.WriteAllText(_configPath, root.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }

    public class CompanySettings
    {
        public string Name { get; set; } = "SGE - Sistema de Gerenciamento de Eventos";
        public string ShortName { get; set; } = "SGE";
        public string Logo { get; set; } = "/img/logo.png";
        public string Theme { get; set; } = "default";
    }
}
