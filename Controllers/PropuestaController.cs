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
        public async Task<ActionResult> Index(int usuario)
        {
            try
            {
                var Usuario = await _context.Usuarios.FindAsync(usuario);
                ViewBag.Message = Usuario.NombreUsuario;
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
        public async Task<ActionResult> Create(int subasta_)
        {

            //ViewBag.Message = usuario.NombreUsuario;
            Subasta subasta = await _context.Subasta.FindAsync(subasta_);
            Usuario usuario = await _context.Usuarios.FindAsync(subasta.UsuarioID);
            ViewBag.Message = usuario.NombreUsuario;
            ViewBag.TituloSubasta = subasta.NombreProyecto;
            ViewBag.DescripcionSubasta = subasta.Descripcion;
            ViewData["IdUser"] = usuario.ID;
            return View();
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

        public async Task<ActionResult> IndexSubastas()
        {
            try
            {
                // TODO: Add delete logic here
                List<Subasta> Subastas = await _context.Subasta.Where(x => x.Status == "E").ToListAsync();
                return View(Subastas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<ActionResult> DetailsSubastas(int subastaid)
        {
            try
            {
                var Subasta = await _context.Subasta.Where(x => x.ID == subastaid).FirstOrDefaultAsync();
                var Usuario = await _context.Usuarios.Where(x => x.ID == Subasta.UsuarioID).FirstOrDefaultAsync();
                var SubastasTerminadas = await _context.Subasta.Where(x => x.UsuarioID == Usuario.ID && x.Status == "T").ToListAsync();
                int SumaCalificaciones = 0;
                foreach (var item in SubastasTerminadas)
                {
                    SumaCalificaciones += Convert.ToInt32(item.Grade);
                }

                if (SubastasTerminadas.Count != 0)
                {
                    float Promedio = SumaCalificaciones / SubastasTerminadas.Count();
                    ViewBag.Calificacion = Promedio;
                }
                else
                {
                    ViewBag.Calificacion = "Sin Proyectos Terminados calificados";
                }
                ViewBag.PyME = Usuario.NombreUsuario;
                return View(Subasta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}