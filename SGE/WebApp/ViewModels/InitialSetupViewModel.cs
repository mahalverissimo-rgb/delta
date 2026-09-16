using Microsoft.AspNetCore.Http;
using SGE.Models;
using SGE.Services;

namespace SGE.ViewModels
{
    public class InitialSetupViewModel
    {
        public CompanySettings CompanySettings { get; set; }
        public RegisterViewModel AdminUser { get; set; }
        public IFormFile LogoFile { get; set; }
    }
}
