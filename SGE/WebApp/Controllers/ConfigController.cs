using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    [Authorize]
    public class ConfigController : Controller
    {

        // GET: Config
        public IActionResult Index()
        {
            return View();
        }

        // GET: Config/Sistema
        public IActionResult Sistema()
        {
            return View();
        }

        // GET: Config/Perfil
        public IActionResult Perfil()
        {
            return View();
        }

        // GET: Config/Preferencias
        public IActionResult Preferencias()
        {
            return View();
        }

        // GET: Config/Seguranca
        public IActionResult Seguranca()
        {
            return View();
        }
    }
}
