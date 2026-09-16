using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaBisutti.Delta.WebApp.Data;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class EventoController : AppController
    {
        private readonly ApplicationDbContext _context;

        public EventoController(ApplicationDbContext context, UserManager<Usuario> userManager)
            : base(userManager)
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
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoEvento,LocalId,Data,HorarioInicio,HorarioTermino,Pax,PerfilFesta,CardapioId,TipoServicoId,ProdutoraId,PosVendedoraId,PossuiAssessoria,ContatoAssessoria,NomeResponsavel,EmailResponsavel,TelefoneResponsavel")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                evento.UsuarioCreateId = await GetCurrentUserIdAsync();
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
                    evento.UsuarioUpdateId = await GetCurrentUserIdAsync();
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
    }
}
