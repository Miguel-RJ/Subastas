using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Subastas.Data;
using Subastas.Models;

namespace Subastas.Controllers
{
    public class PropuestaController : Controller
    {
        private readonly SubastaProyectosContext _context;
        public PropuestaController(SubastaProyectosContext context)
        {
            _context = context;
        }
        // GET: Propuesta
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await _context.Propuesta.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Propuesta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Propuesta/Create
        public async Task<ActionResult> Create()
        {
            Propuesta propuesta = new Propuesta()
            {
                UsuarioID = 8
            };
            //ViewBag.Message = usuario.NombreUsuario;
            ViewBag.Message = "Accenture";
            return View(propuesta);
        }

        // POST: Propuesta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Propuesta propuesta)
        {
            try
            {
                // TODO: Add insert logic here
                propuesta.Status = "S";
                await _context.Propuesta.AddAsync(propuesta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Propuesta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Propuesta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propuesta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Propuesta/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}