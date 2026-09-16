using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Services;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class CardapioController : AppController
    {
        private readonly ICardapioService _cardapioService;

        public CardapioController(ICardapioService cardapioService, UserManager<Usuario> userManager)
            : base(userManager)
        {
            _cardapioService = cardapioService;
        }

        // GET: Cardapio
        public async Task<IActionResult> Index()
        {
            return View(await _cardapioService.GetAllAsync());
        }

        // GET: Cardapio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardapio = await _cardapioService.GetDetailsAsync(id.Value);
            if (cardapio == null)
            {
                return NotFound();
            }

            return View(cardapio);
        }

        // GET: Cardapio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cardapio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,PrecoPorPessoa,Ativo")] Cardapio cardapio)
        {
            if (ModelState.IsValid)
            {
                await _cardapioService.CreateAsync(cardapio, await GetCurrentUserIdAsync());
                return RedirectToAction(nameof(Index));
            }
            return View(cardapio);
        }

        // GET: Cardapio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardapio = await _cardapioService.GetDetailsAsync(id.Value);
            if (cardapio == null)
            {
                return NotFound();
            }
            return View(cardapio);
        }

        // POST: Cardapio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,PrecoPorPessoa,Ativo")] Cardapio cardapio)
        {
            if (id != cardapio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var atualizado = await _cardapioService.UpdateAsync(cardapio, await GetCurrentUserIdAsync());
                if (!atualizado)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cardapio);
        }

        // GET: Cardapio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardapio = await _cardapioService.GetDetailsAsync(id.Value);
            if (cardapio == null)
            {
                return NotFound();
            }

            return View(cardapio);
        }

        // POST: Cardapio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _cardapioService.DeleteAsync(id);
            if (!resultado.Success)
            {
                ModelState.AddModelError(string.Empty, resultado.ErrorMessage!);
                var cardapio = await _cardapioService.GetDetailsAsync(id);
                if (cardapio == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(cardapio);
            }
            return RedirectToAction(nameof(Index));
        }

        // API Methods
        [HttpGet]
        public async Task<JsonResult> GetCardapiosAtivos()
        {
            return Json(await _cardapioService.GetAtivosAsync());
        }
    }
}
