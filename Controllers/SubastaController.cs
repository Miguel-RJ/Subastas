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
        public async Task<ActionResult> Index(int usuario)
        {

            try
            {
                var Usuario = await _context.Usuarios.FindAsync(usuario);
                ViewBag.Message = Usuario.NombreUsuario;
                ViewBag.PyME = Usuario.ID;
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
                return RedirectToAction(nameof(Index), new { usuario = subasta.UsuarioID });
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
        public async Task<ActionResult> IndexPropuestas(int PyME)
        {
            try
            {
                // TODO: Add delete logic here
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.ID == PyME);
                ViewBag.PyME = usuario.ID;
                List<Propuesta> Propuestas = await _context.Propuesta.Where(x => x.Status == "S").ToListAsync();
                return View(Propuestas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<ActionResult> DetailsPropuestas(int consultoria, int PyME)
        {
            try
            {
                var Propuesta = await _context.Propuesta.Where(x => x.ID == consultoria).FirstOrDefaultAsync();
                var Usuario = await _context.Usuarios.Where(x => x.ID == Propuesta.UsuarioID).FirstOrDefaultAsync();
                var PropuestasTerminadas = await _context.Propuesta.Where(x => x.UsuarioID == Usuario.ID && x.Status == "T").ToListAsync();
                int SumaCalificaciones = 0;
                foreach (var item in PropuestasTerminadas)
                {
                    SumaCalificaciones += Convert.ToInt32(item.Grade);
                }

                if (PropuestasTerminadas.Count != 0)
                {
                    float Promedio = SumaCalificaciones / PropuestasTerminadas.Count();
                    ViewBag.Calificacion = Promedio;
                }
                else
                {
                    ViewBag.Calificacion = "Sin Proyectos Terminados calificados";
                }
                ViewBag.Consultoria = Usuario.NombreUsuario;
                var UsuarioPyME = await _context.Usuarios.Where(x => x.ID == PyME).FirstOrDefaultAsync();
                ViewBag.PyME = UsuarioPyME.ID;
                return View(Propuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> UpdateStatus(int consultoria, int PyME)
        {
            try
            {
                var Propuesta = await _context.Propuesta.Where(x => x.ID == consultoria).FirstOrDefaultAsync();
                var Usuario = await _context.Usuarios.Where(x => x.ID == Propuesta.UsuarioID).FirstOrDefaultAsync();
                ViewBag.Consultoria = Usuario.NombreUsuario;
                var UsuarioPyME = await _context.Usuarios.Where(x => x.ID == PyME).FirstOrDefaultAsync();
                ViewBag.PyME = UsuarioPyME.ID;
                return View("EditPropuestas", Propuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> UpdatePropuesta(Propuesta propuestaModificada)
        {
            try
            {
                Propuesta propuesta = await _context.Propuesta.FindAsync(propuestaModificada.ID);
                _context.Entry(propuesta).CurrentValues.SetValues(propuestaModificada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { usuario = propuestaModificada.UsuarioID });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<ActionResult> IndexPropuestasAcep(int PyME)
        {
            try
            {
                // TODO: Add delete logic here
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.ID == PyME);
                ViewBag.PyME = usuario.ID;
                List<Propuesta> Propuestas = await _context.Propuesta.Where(x => x.Status == "A").ToListAsync();
                return View(Propuestas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<ActionResult> IndexPropuestaTer(int PyME)
        {
            try
            {
                // TODO: Add delete logic here
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.ID == PyME);
                ViewBag.PyME = usuario.ID;
                List<Propuesta> Propuestas = await _context.Propuesta.Where(x => x.Status == "T").ToListAsync();
                return View(Propuestas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<ActionResult> UpdateGrade(int consultoria, int PyME)
        {
            try
            {
                var Propuesta = await _context.Propuesta.Where(x => x.ID == consultoria).FirstOrDefaultAsync();
                var Usuario = await _context.Usuarios.Where(x => x.ID == Propuesta.UsuarioID).FirstOrDefaultAsync();
                ViewBag.Consultoria = Usuario.NombreUsuario;
                var UsuarioPyME = await _context.Usuarios.Where(x => x.ID == PyME).FirstOrDefaultAsync();
                ViewBag.PyME = UsuarioPyME.ID;
                return View("EditPropuestasGrade", Propuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}