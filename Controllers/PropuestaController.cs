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
        public async Task<ActionResult> Index(Usuario usuario)
        {
            try
            {
                ViewBag.Message = usuario.NombreUsuario;
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
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // TODO: Add update logic here
                Propuesta Propuesta = await _context.Propuesta.FindAsync(id);
                return View(Propuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Propuesta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Propuesta propuestaModificada)
        {
            try
            {
                Propuesta propuesta = await _context.Propuesta.FindAsync(id);
                _context.Entry(propuesta).CurrentValues.SetValues(propuestaModificada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Propuesta/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Propuesta Propuesta = await _context.Propuesta.FindAsync(id);
                return View(Propuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Propuesta/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Propuesta propuestaEliminada)
        {
            try
            {
                // TODO: Add delete logic here
                Propuesta Propuesta = await _context.Propuesta.FindAsync(id);
                _context.Propuesta.Attach(Propuesta);
                _context.Propuesta.Remove(Propuesta);
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