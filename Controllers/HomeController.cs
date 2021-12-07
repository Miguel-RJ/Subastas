using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Subastas.Models;
using Subastas.Data;


namespace Subastas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SubastaProyectosContext _context;
        public HomeController(SubastaProyectosContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(Usuario usuario)
        {
            return View();
        }

        public async Task<ActionResult> LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(Usuario usuario)
        {
            Usuario match = _context.Usuarios.SingleOrDefault(x => x.TagUsuario == usuario.TagUsuario);
            if (match is null)
            {
                return View();
            }
            else if (match.Password != usuario.Password)
            {
                return View();
            }
            else
            {
                if (match.RolID == 1)
                {
                    return RedirectToAction("Index", "Usuario", match);
                }
                else if (match.RolID == 2)
                {
                    return RedirectToAction("Index", "Subasta", match);
                }
                else
                {
                    return RedirectToAction("Index", "Propuesta", match);
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
