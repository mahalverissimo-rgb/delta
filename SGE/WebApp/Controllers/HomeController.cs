using SGE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SGE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace SGE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Contagens para estatísticas
            ViewBag.EventosCount = _context.Eventos.Count(e => e.Data >= DateTime.Now);
            ViewBag.LocaisCount = _context.Locais.Count();
            ViewBag.CardapiosCount = _context.Cardapios.Count();
            ViewBag.ServicosCount = _context.TiposServico.Count();

            // Próximos eventos
            ViewBag.ProximosEventos = _context.Eventos
                .Include(e => e.Local)
                .Where(e => e.Data >= DateTime.Now)
                .OrderBy(e => e.Data)
                .Take(5)
                .ToList();

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
