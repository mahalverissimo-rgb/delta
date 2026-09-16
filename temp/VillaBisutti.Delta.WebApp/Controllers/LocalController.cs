using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaBisutti.Delta.WebApp.Data;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class LocalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Local
        public async Task<IActionResult> Index()
        {
            return View(await _context.Locais.ToListAsync());
        }

        // GET: Local/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locais
                .FirstOrDefaultAsync(m => m.Id == id);
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
                local.UsuarioCreateId = GetCurrentUserId();
                local.UsuarioCreateData = DateTime.Now;
                
                _context.Add(local);
                await _context.SaveChangesAsync();
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

            var local = await _context.Locais.FindAsync(id);
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
                try
                {
                    local.UsuarioUpdateId = GetCurrentUserId();
                    local.UsuarioUpdateData = DateTime.Now;
                    
                    _context.Update(local);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalExists(local.Id))
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
            return View(local);
        }

        // GET: Local/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locais
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var local = await _context.Locais.FindAsync(id);
            if (local != null)
            {
                // Verifica se existem eventos associados a este local
                var temEventos = await _context.Eventos.AnyAsync(e => e.LocalId == id);
                if (temEventos)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível excluir este local pois existem eventos associados a ele.");
                    return View(local);
                }

                _context.Locais.Remove(local);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LocalExists(int id)
        {
            return _context.Locais.Any(e => e.Id == id);
        }

        private string GetCurrentUserId()
        {
            // Implementar lógica para obter o ID do usuário atual
            // Por enquanto retorna um ID fixo como exemplo
            return "1";
        }

        // API Methods
        [HttpGet]
        public async Task<JsonResult> GetLocaisAtivos()
        {
            var locais = await _context.Locais
                .Where(l => l.Ativo)
                .Select(l => new { l.Id, l.Nome, l.Capacidade })
                .ToListAsync();
            return Json(locais);
        }
    }
}
