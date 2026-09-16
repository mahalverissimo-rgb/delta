using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaBisutti.Delta.WebApp.Data;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class CardapioController : AppController
    {
        private readonly ApplicationDbContext _context;

        public CardapioController(ApplicationDbContext context, UserManager<Usuario> userManager)
            : base(userManager)
        {
            _context = context;
        }

        // GET: Cardapio
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cardapios
                .Include(c => c.Pratos)
                .ToListAsync());
        }

        // GET: Cardapio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardapio = await _context.Cardapios
                .Include(c => c.Pratos)
                .FirstOrDefaultAsync(m => m.Id == id);
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
                cardapio.UsuarioCreateId = await GetCurrentUserIdAsync();
                cardapio.UsuarioCreateData = DateTime.Now;
                
                _context.Add(cardapio);
                await _context.SaveChangesAsync();
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

            var cardapio = await _context.Cardapios
                .Include(c => c.Pratos)
                .FirstOrDefaultAsync(m => m.Id == id);
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
                try
                {
                    cardapio.UsuarioUpdateId = await GetCurrentUserIdAsync();
                    cardapio.UsuarioUpdateData = DateTime.Now;
                    
                    _context.Update(cardapio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardapioExists(cardapio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
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

            var cardapio = await _context.Cardapios
                .Include(c => c.Pratos)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var cardapio = await _context.Cardapios.FindAsync(id);
            if (cardapio != null)
            {
                // Verifica se existem eventos associados a este cardápio
                var temEventos = await _context.Eventos.AnyAsync(e => e.CardapioId == id);
                if (temEventos)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível excluir este cardápio pois existem eventos associados a ele.");
                    return View(cardapio);
                }

                _context.Cardapios.Remove(cardapio);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CardapioExists(int id)
        {
            return _context.Cardapios.Any(e => e.Id == id);
        }

        // API Methods
        [HttpGet]
        public async Task<JsonResult> GetCardapiosAtivos()
        {
            var cardapios = await _context.Cardapios
                .Where(c => c.Ativo)
                .Select(c => new { c.Id, c.Nome, c.PrecoPorPessoa })
                .ToListAsync();
            return Json(cardapios);
        }
    }
}
