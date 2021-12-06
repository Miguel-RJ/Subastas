using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Subastas.Data;
using Subastas.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Subastas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly SubastaProyectosContext _context;

        public UsuarioController(SubastaProyectosContext context)
        {
            _context = context;
        }
        // GET: Usuario
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await _context.Usuarios.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Usuario/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                Usuario Usuario = await _context.Usuarios.FindAsync(id);
                return View(Usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            try
            {
                // TODO: Add insert logic here
                if (usuario.RolID == 2)
                {
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
                                return View("Create", usuario);

                            }
                        }
                    }
                }
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Usuario/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Usuario Usuario = await _context.Usuarios.FindAsync(id);
                return View(Usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Usuario usuarioModificado)
        {
            try
            {
                // TODO: Add update logic here
                Usuario Usuario = await _context.Usuarios.FindAsync(id);
                _context.Entry(Usuario).CurrentValues.SetValues(usuarioModificado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Usuario/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Usuario Usuario = await _context.Usuarios.FindAsync(id);
                return View(Usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Usuario usuarioEliminado)
        {
            try
            {
                // TODO: Add delete logic here
                Usuario Usuario = await _context.Usuarios.FindAsync(id);
                _context.Usuarios.Attach(Usuario);
                _context.Usuarios.Remove(Usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}