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
                ViewBag.Consultoria = Usuario.ID;
                ViewBag.TipoPropuesta = "";
                return View(await _context.Propuesta.Where(x => x.UsuarioID == usuario).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<ActionResult> IndexSelec(int consultoria, string status)
        {
            try
            {
                var Usuario = await _context.Usuarios.FindAsync(consultoria);
                ViewBag.Message = Usuario.NombreUsuario;
                ViewBag.Consultoria = Usuario.ID;
                var Propuestas = await _context.Propuesta.Where(x => x.Status == status && x.UsuarioID == consultoria).ToListAsync();
                if (status == "A")
                {
                    ViewBag.TipoPropuesta = "Aceptadas";
                }
                else if (status == "T")
                {
                    ViewBag.TipoPropuesta = "Terminadas";
                }
                else
                {
                    ViewBag.TipoPropuesta = "";
                }
                return View("Index", Propuestas);
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
        public async Task<ActionResult> Create(int subasta_, int consultoria)
        {

            //ViewBag.Message = usuario.NombreUsuario;
            Subasta subasta = await _context.Subasta.FindAsync(subasta_);
            Usuario usuario = await _context.Usuarios.FindAsync(subasta.UsuarioID);
            Usuario Consultoria = await _context.Usuarios.FindAsync(consultoria);
            ViewBag.Message = usuario.NombreUsuario;
            ViewBag.TituloSubasta = subasta.NombreProyecto;
            ViewBag.DescripcionSubasta = subasta.Descripcion;
            ViewData["IdUser"] = Consultoria.ID;
            ViewBag.Pyme = subasta.UsuarioID;
            ViewBag.Consultoria = consultoria;
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
                return RedirectToAction(nameof(Index), new { usuario = propuesta.UsuarioID });
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
                ViewBag.Consultoria = Propuesta.UsuarioID;
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
                return RedirectToAction(nameof(Index), new { usuario = propuesta.UsuarioID });
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
                ViewBag.Consultoria = Propuesta.UsuarioID;
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
        public async Task<ActionResult> Delete(Propuesta propuestaEliminada)
        {
            try
            {
                // TODO: Add delete logic here
                Propuesta Propuesta = await _context.Propuesta.FindAsync(propuestaEliminada.ID);
                _context.Propuesta.Attach(Propuesta);
                _context.Propuesta.Remove(Propuesta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { usuario = Propuesta.UsuarioID });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> IndexSubastas(int consultoria)
        {
            try
            {
                // TODO: Add delete logic here
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.ID == consultoria);
                ViewBag.IDPyme = usuario.ID;
                ViewBag.Consultoria = usuario.ID;
                List<Subasta> Subastas = await _context.Subasta.ToListAsync();
                return View(Subastas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<ActionResult> DetailsSubastas(int subastaid, int consultoria)
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
                var Consultoria = await _context.Usuarios.FindAsync(consultoria);
                ViewBag.Consultoria = Consultoria.ID;
                return View(Subasta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<ActionResult> IndexPropuestasSelec(int consultoria)
        {
            try
            {
                // TODO: Add delete logic here
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.ID == consultoria);
                ViewBag.IDPyme = usuario.ID;
                ViewBag.Consultoria = usuario.ID;
                List<Subasta> Subastas = await _context.Subasta.Where(x => x.Status == "S").ToListAsync();
                return View("Index", Subastas);
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


                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}