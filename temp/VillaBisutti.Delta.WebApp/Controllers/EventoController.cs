using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VillaBisutti.Delta.WebApp.Data;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class EventoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evento
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos
                .Include(e => e.Local)
                .Include(e => e.Cardapio)
                .Include(e => e.TipoServico)
                .Include(e => e.Produtora)
                .Include(e => e.PosVendedora)
                .ToListAsync());
        }

        // GET: Evento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Local)
                .Include(e => e.Cardapio)
                .Include(e => e.TipoServico)
                .Include(e => e.Produtora)
                .Include(e => e.PosVendedora)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Evento/Create
        public IActionResult Create()
        {
            ViewBag.Locais = new SelectList(_context.Locais.Where(l => l.Ativo), "Id", "Nome");
            ViewBag.Cardapios = new SelectList(_context.Cardapios.Where(c => c.Ativo), "Id", "Nome");
            ViewBag.TiposServico = new SelectList(_context.TiposServico.Where(t => t.Ativo), "Id", "Nome");
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoEvento,LocalId,Data,HorarioInicio,HorarioTermino,Pax,PerfilFesta,CardapioId,TipoServicoId,ProdutoraId,PosVendedoraId,PossuiAssessoria,ContatoAssessoria,NomeResponsavel,EmailResponsavel,TelefoneResponsavel")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                evento.UsuarioCreateId = GetCurrentUserId();
                evento.UsuarioCreateData = DateTime.Now;
                
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoEvento,LocalId,Data,HorarioInicio,HorarioTermino,Pax,PerfilFesta,CardapioId,TipoServicoId,ProdutoraId,PosVendedoraId,PossuiAssessoria,ContatoAssessoria,NomeResponsavel,EmailResponsavel,TelefoneResponsavel")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    evento.UsuarioUpdateId = GetCurrentUserId();
                    evento.UsuarioUpdateData = DateTime.Now;
                    
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
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
            return View(evento);
        }

        // GET: Evento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Local)
                .Include(e => e.Cardapio)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }

        private string GetCurrentUserId()
        {
            // Implementar lógica para obter o ID do usuário atual
            // Por enquanto retorna um ID fixo como exemplo
            return "1";
        }
    }
}
