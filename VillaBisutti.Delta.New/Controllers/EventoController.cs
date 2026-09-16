using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Services;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class EventoController : AppController
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService, UserManager<Usuario> userManager)
            : base(userManager)
        {
            _eventoService = eventoService;
        }

        // GET: Evento
        public async Task<IActionResult> Index()
        {
            return View(await _eventoService.GetAllAsync());
        }

        // GET: Evento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _eventoService.GetDetailsAsync(id.Value);
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
                await _eventoService.CreateAsync(evento, await GetCurrentUserIdAsync());
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

            var evento = await _eventoService.GetByIdAsync(id.Value);
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
                var atualizado = await _eventoService.UpdateAsync(evento, await GetCurrentUserIdAsync());
                if (!atualizado)
                {
                    return NotFound();
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

            var evento = await _eventoService.GetWithLocalCardapioAsync(id.Value);
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
            await _eventoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
