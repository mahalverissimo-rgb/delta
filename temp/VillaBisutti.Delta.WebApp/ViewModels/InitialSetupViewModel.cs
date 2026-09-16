using Microsoft.AspNetCore.Http;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.ViewModels
{
    public class InitialSetupViewModel
    {
        public CompanySettings CompanySettings { get; set; }
        public RegisterViewModel AdminUser { get; set; }
        public IFormFile LogoFile { get; set; }
    }
}
