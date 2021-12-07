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
                return View(await _context.Subasta.Where(x => x.UsuarioID == usuario).ToListAsync());
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
        public async Task<ActionResult> Create(int PyME)
        {
            try
            {
                // if (usuario.RolID != 3)
                // {
                //     ViewBag.Message = "Solo las PyME pueden crear subastas";
                //     return View("Home","Index", usuario);
                // }
                //ViewBag.Message = usuario.NombreUsuario;
                Usuario usuario = await _context.Usuarios.FindAsync(PyME);
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
                return RedirectToAction(nameof(Index), new { usuario = subastaModificada.UsuarioID });
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
                return RedirectToAction(nameof(Index), new { usuario = Subasta.UsuarioID });
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
                //var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.ID == PyME);
                //List<Propuesta> PropuestasAceptadas = await _context.Propuesta.Where(x => x.UsuarioID == PyME && x.Status == "A").ToListAsync;

                List<Subasta> Subastas = await _context.Subasta.Where(x => x.Status == "E").ToListAsync();
                List<Propuesta> PropuestasPyme = new List<Propuesta>();
                foreach (var item in Subastas)
                {
                    var PropuestasDeSubasta = await _context.Propuesta.Where(x => x.SubastaID == item.UsuarioID & x.Status == "S").ToListAsync();
                    foreach (var prop in PropuestasDeSubasta)
                    {
                        PropuestasPyme.Add(prop);
                    }
                }
                ViewBag.PyME = PyME;
                return View(PropuestasPyme);
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

                Subasta subasta = await _context.Subasta.Where(x => x.UsuarioID == propuestaModificada.SubastaID).FirstOrDefaultAsync(); ;
                Subasta newSubasta = subasta;
                if (propuestaModificada.Status == "A")
                {
                    newSubasta.Status = "S";
                }
                else if (propuestaModificada.Status == "T")
                {
                    newSubasta.Status = "T";
                }
                _context.Entry(subasta).CurrentValues.SetValues(newSubasta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { usuario = propuestaModificada.SubastaID });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<ActionResult> IndexPropuestasSR(int PyME)
        {
            try
            {
                // TODO: Add delete logic here
                ViewBag.PyME = PyME;
                List<Propuesta> Propuestas = await _context.Propuesta.Where(x => x.Status == "S" && x.UsuarioID == PyME).ToListAsync();
                return View(Propuestas);
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
                //var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.ID == PyME);
                List<Subasta> Subastas = await _context.Subasta.Where(x => x.UsuarioID == PyME && x.Status == "S").ToListAsync();
                List<Propuesta> PropuestasPyme = new List<Propuesta>();
                foreach (var item in Subastas)
                {
                    var PropuestasDeSubasta = await _context.Propuesta.Where(x => x.SubastaID == item.UsuarioID & x.Status == "A").ToListAsync();
                    foreach (var prop in PropuestasDeSubasta)
                    {
                        PropuestasPyme.Add(prop);
                    }
                }
                ViewBag.PyME = PyME;
                return View(PropuestasPyme);
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
                //var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.ID == PyME);
                List<Subasta> Subastas = await _context.Subasta.Where(x => x.UsuarioID == PyME && x.Status == "T").ToListAsync();
                List<Propuesta> PropuestasPyme = new List<Propuesta>();
                foreach (var item in Subastas)
                {
                    var PropuestasDeSubasta = await _context.Propuesta.Where(x => x.SubastaID == item.UsuarioID & x.Status == "T").ToListAsync();
                    foreach (var prop in PropuestasDeSubasta)
                    {
                        PropuestasPyme.Add(prop);
                    }
                }
                ViewBag.PyME = PyME;
                return View(PropuestasPyme);
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