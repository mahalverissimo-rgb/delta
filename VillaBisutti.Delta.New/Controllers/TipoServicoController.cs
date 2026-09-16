using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Services;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class TipoServicoController : AppController
    {
        private readonly ITipoServicoService _tipoServicoService;

        public TipoServicoController(ITipoServicoService tipoServicoService, UserManager<Usuario> userManager)
            : base(userManager)
        {
            _tipoServicoService = tipoServicoService;
        }

        // GET: TipoServico
        public async Task<IActionResult> Index()
        {
            return View(await _tipoServicoService.GetAllAsync());
        }

        // GET: TipoServico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServico = await _tipoServicoService.GetByIdAsync(id.Value);
            if (tipoServico == null)
            {
                return NotFound();
            }

            return View(tipoServico);
        }

        // GET: TipoServico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoServico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Ativo")] TipoServico tipoServico)
        {
            if (ModelState.IsValid)
            {
                await _tipoServicoService.CreateAsync(tipoServico, await GetCurrentUserIdAsync());
                return RedirectToAction(nameof(Index));
            }
            return View(tipoServico);
        }

        // GET: TipoServico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServico = await _tipoServicoService.GetByIdAsync(id.Value);
            if (tipoServico == null)
            {
                return NotFound();
            }
            return View(tipoServico);
        }

        // POST: TipoServico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Ativo")] TipoServico tipoServico)
        {
            if (id != tipoServico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var atualizado = await _tipoServicoService.UpdateAsync(tipoServico, await GetCurrentUserIdAsync());
                if (!atualizado)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoServico);
        }

        // GET: TipoServico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServico = await _tipoServicoService.GetByIdAsync(id.Value);
            if (tipoServico == null)
            {
                return NotFound();
            }

            return View(tipoServico);
        }

        // POST: TipoServico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _tipoServicoService.DeleteAsync(id);
            if (!resultado.Success)
            {
                ModelState.AddModelError(string.Empty, resultado.ErrorMessage!);
                var tipoServico = await _tipoServicoService.GetByIdAsync(id);
                if (tipoServico == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(tipoServico);
            }
            return RedirectToAction(nameof(Index));
        }

        // API Methods
        [HttpGet]
        public async Task<JsonResult> GetTiposServicoAtivos()
        {
            return Json(await _tipoServicoService.GetAtivosAsync());
        }
    }
}
