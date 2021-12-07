using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subastas.Data;
using Subastas.Models;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;

namespace Subastas.Controllers
{
    public class SubastaController : Controller
    {
        private readonly SubastaProyectosContext _context;
        public SubastaController(SubastaProyectosContext context)
        {
            _context = context;
        }
        // GET: Subasta
        public async Task<ActionResult> Index(Usuario usuario)
        {

            try
            {
                ViewBag.Message = usuario.NombreUsuario;
                return View(await _context.Subasta.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Subasta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subasta/Create
        public async Task<ActionResult> Create(int subasta_)
        {
            try
            {
                // if (usuario.RolID != 3)
                // {
                //     ViewBag.Message = "Solo las PyME pueden crear subastas";
                //     return View("Home","Index", usuario);
                // }
                //ViewBag.Message = usuario.NombreUsuario;
                Subasta subasta = await _context.Subasta.FindAsync(subasta_);
                Usuario usuario = await _context.Usuarios.FindAsync(subasta.UsuarioID);
                ViewBag.Message = usuario.NombreUsuario;
                ViewData["IdUser"] = usuario.ID;
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Subasta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Subasta subasta)
        {
            try
            {
                // TODO: Add insert logic here
                subasta.Status = "E";
                await _context.Subasta.AddAsync(subasta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Subasta/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // TODO: Add update logic here
                Subasta Subasta = await _context.Subasta.FindAsync(id);
                return View(Subasta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Subasta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Subasta subastaModificada)
        {
            try
            {
                Subasta subasta = await _context.Subasta.FindAsync(id);
                _context.Entry(subasta).CurrentValues.SetValues(subastaModificada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Subasta/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Subasta Subasta = await _context.Subasta.FindAsync(id);
                return View(Subasta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Subasta/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Subasta subastaModificada)
        {
            try
            {
                // TODO: Add delete logic here
                Subasta Subasta = await _context.Subasta.FindAsync(id);
                _context.Subasta.Attach(Subasta);
                _context.Subasta.Remove(Subasta);
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