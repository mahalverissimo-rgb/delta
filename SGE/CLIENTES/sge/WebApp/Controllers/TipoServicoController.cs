using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGE.Models;
using SGE.Data;

namespace SGE.Controllers
{
    [Authorize]
    public class TipoServicoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoServicoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoServico
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposServico.ToListAsync());
        }

        // GET: TipoServico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServico = await _context.TiposServico
                .FirstOrDefaultAsync(m => m.Id == id);
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
                tipoServico.UsuarioCreateId = GetCurrentUserId();
                tipoServico.UsuarioCreateData = DateTime.Now;
                
                _context.Add(tipoServico);
                await _context.SaveChangesAsync();
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

            var tipoServico = await _context.TiposServico.FindAsync(id);
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
                try
                {
                    tipoServico.UsuarioUpdateId = GetCurrentUserId();
                    tipoServico.UsuarioUpdateData = DateTime.Now;
                    
                    _context.Update(tipoServico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoServicoExists(tipoServico.Id))
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
            return View(tipoServico);
        }

        // GET: TipoServico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServico = await _context.TiposServico
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var tipoServico = await _context.TiposServico.FindAsync(id);
            if (tipoServico != null)
            {
                // Verifica se existem eventos associados a este tipo de serviço
                var temEventos = await _context.Eventos.AnyAsync(e => e.TipoServicoId == id);
                if (temEventos)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível excluir este tipo de serviço pois existem eventos associados a ele.");
                    return View(tipoServico);
                }

                _context.TiposServico.Remove(tipoServico);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TipoServicoExists(int id)
        {
            return _context.TiposServico.Any(e => e.Id == id);
        }

        private string GetCurrentUserId()
        {
            // Implementar lógica para obter o ID do usuário atual
            // Por enquanto retorna um ID fixo como exemplo
            return "1";
        }

        // API Methods
        [HttpGet]
        public async Task<JsonResult> GetTiposServicoAtivos()
        {
            var tipos = await _context.TiposServico
                .Where(t => t.Ativo)
                .Select(t => new { t.Id, t.Nome })
                .ToListAsync();
            return Json(tipos);
        }
    }
}
