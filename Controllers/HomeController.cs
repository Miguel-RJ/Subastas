using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Subastas.Models;
using Subastas.Data;
using Newtonsoft.Json;
using System.Net.Http;

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
                    return RedirectToAction("Index", "Usuario", new { usuario = match.ID });
                }
                else if (match.RolID == 2)
                {
                    return RedirectToAction("Index", "Subasta", new { usuario = match.ID });
                }
                else
                {
                    return RedirectToAction("Index", "Propuesta", new { usuario = match.ID });
                }
            }
        }

        public async Task<ActionResult> Create(Usuario usuario)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    ResponseSAT SAT = new ResponseSAT();

                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.satws.com/rfc/validate/" + usuario.RFC))
                    {
                        requestMessage.Headers.Add("x-api-key", "53b49f2431c1cb104911f1e197b68926");
                        HttpResponseMessage response = await client.SendAsync(requestMessage);
                        if (response.IsSuccessStatusCode)
                        {
                            SAT = JsonConvert.DeserializeObject<ResponseSAT>(response.Content.ReadAsStringAsync().Result);
                        }
                        if (!(SAT.valid == true && SAT.active == true))
                        {
                            ViewBag.Message = "RFC no valido o no activo";
                            return View(usuario);

                        }
                    }
                }
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

        public async Task<ActionResult> SignUp()
        {
            return View();
        }
    }
}
