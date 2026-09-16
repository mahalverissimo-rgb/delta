using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    /// <summary>
    /// Base para controllers que precisam registrar auditoria (quem criou/alterou).
    /// Resolve o usuário autenticado real em vez de um ID fixo.
    /// </summary>
    public abstract class AppController : Controller
    {
        private readonly UserManager<Usuario> _userManager;

        protected AppController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Retorna o Id do usuário autenticado atual, ou null quando não for possível resolvê-lo.
        /// </summary>
        protected async Task<int?> GetCurrentUserIdAsync()
        {
            var usuario = await _userManager.GetUserAsync(User);
            return usuario?.Id;
        }
    }
}
