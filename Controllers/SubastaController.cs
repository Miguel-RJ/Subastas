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
        public async Task<ActionResult> Index()
        {
            try
            {
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
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                // if (usuario.RolID != 3)
                // {
                //     ViewBag.Message = "Solo las PyME pueden crear subastas";
                //     return View("Home","Index", usuario);
                // }
                Subasta subasta = new Subasta()
                {
                    UsuarioID = 8
                };
                //ViewBag.Message = usuario.NombreUsuario;
                ViewBag.Message = "Banco de México";
                return View(subasta);
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subasta/Edit/5
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

        // GET: Subasta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Subasta/Delete/5
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