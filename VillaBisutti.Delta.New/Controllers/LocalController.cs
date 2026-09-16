using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Services;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class LocalController : AppController
    {
        private readonly ILocalService _localService;

        public LocalController(ILocalService localService, UserManager<Usuario> userManager)
            : base(userManager)
        {
            _localService = localService;
        }

        // GET: Local
        public async Task<IActionResult> Index()
        {
            return View(await _localService.GetAllAsync());
        }

        // GET: Local/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _localService.GetByIdAsync(id.Value);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // GET: Local/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Local/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Endereco,Capacidade,Ativo,Observacoes")] Local local)
        {
            if (ModelState.IsValid)
            {
                await _localService.CreateAsync(local, await GetCurrentUserIdAsync());
                return RedirectToAction(nameof(Index));
            }
            return View(local);
        }

        // GET: Local/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _localService.GetByIdAsync(id.Value);
            if (local == null)
            {
                return NotFound();
            }
            return View(local);
        }

        // POST: Local/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Capacidade,Ativo,Observacoes")] Local local)
        {
            if (id != local.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var atualizado = await _localService.UpdateAsync(local, await GetCurrentUserIdAsync());
                if (!atualizado)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(local);
        }

        // GET: Local/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _localService.GetByIdAsync(id.Value);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // POST: Local/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _localService.DeleteAsync(id);
            if (!resultado.Success)
            {
                ModelState.AddModelError(string.Empty, resultado.ErrorMessage!);
                var local = await _localService.GetByIdAsync(id);
                if (local == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(local);
            }
            return RedirectToAction(nameof(Index));
        }

        // API Methods
        [HttpGet]
        public async Task<JsonResult> GetLocaisAtivos()
        {
            return Json(await _localService.GetAtivosAsync());
        }
    }
}
