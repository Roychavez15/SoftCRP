using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers
{
    [Authorize(Roles = "Admin,Renting")]
    public class TipoCapacitacionesController : Controller
    {
        private readonly ILogRepository _logRepository;
        private readonly ILogger<TipoCapacitacionesController> _logger;
        private readonly DataContext _context;

        public TipoCapacitacionesController(
            ILogRepository logRepository,
            ILogger<TipoCapacitacionesController> logger,
            DataContext context)
        {
            _logRepository = logRepository;
            _logger = logger;
            _context = context;
        }

        // GET: TipoCapacitaciones
        public async Task<IActionResult> Index()
        {
            await _logRepository.SaveLogs("Get", "Obtiene lista de Tipo Capacitaciones", "Tipo Capacitaciones", User.Identity.Name);
            return View(await _context.tipoCapacitaciones.ToListAsync());
        }

        // GET: TipoCapacitaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCapacitacion = await _context.tipoCapacitaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCapacitacion == null)
            {
                return NotFound();
            }

            return View(tipoCapacitacion);
        }

        // GET: TipoCapacitaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCapacitaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoCapacitacion tipoCapacitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCapacitacion);
                await _context.SaveChangesAsync();
                await _logRepository.SaveLogs("Crear", "Crear Tipo Capacitaciones Id: "+tipoCapacitacion.Id.ToString(), "Tipo Capacitaciones", User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCapacitacion);
        }

        // GET: TipoCapacitaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCapacitacion = await _context.tipoCapacitaciones.FindAsync(id);
            if (tipoCapacitacion == null)
            {
                return NotFound();
            }
            return View(tipoCapacitacion);
        }

        // POST: TipoCapacitaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipoCapacitacion tipoCapacitacion)
        {
            if (id != tipoCapacitacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCapacitacion);
                    await _context.SaveChangesAsync();
                    await _logRepository.SaveLogs("Editar", "Editar Tipo Capacitaciones Id: " + tipoCapacitacion.Id.ToString(), "Tipo Capacitaciones", User.Identity.Name);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCapacitacionExists(tipoCapacitacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCapacitacion);
        }

        // GET: TipoCapacitaciones/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tipoCapacitacion = await _context.tipoCapacitaciones
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (tipoCapacitacion == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tipoCapacitacion);
        //}

        // POST: TipoCapacitaciones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var tipoCapacitacion = await _context.tipoCapacitaciones.FindAsync(id);
        //    _context.tipoCapacitaciones.Remove(tipoCapacitacion);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> Delete(int id)
        {
            var tipoCapacitacion = await _context.tipoCapacitaciones.FindAsync(id);
            await _logRepository.SaveLogs("Desactivar", "Desactivar Tipo Capacitaciones Id: " + tipoCapacitacion.Id.ToString(), "Tipo Capacitaciones", User.Identity.Name);

            tipoCapacitacion.isActive = false;
            _context.tipoCapacitaciones.Update(tipoCapacitacion);
            //_context.tipoCapacitaciones.Remove(tipoCapacitacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Enable(int id)
        {
            var tipoCapacitacion = await _context.tipoCapacitaciones.FindAsync(id);
            await _logRepository.SaveLogs("Activar", "Activar Tipo Capacitaciones Id: " + tipoCapacitacion.Id.ToString(), "Tipo Capacitaciones", User.Identity.Name);
            tipoCapacitacion.isActive = true;
            _context.tipoCapacitaciones.Update(tipoCapacitacion);
            //_context.tipoCapacitaciones.Remove(tipoCapacitacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool TipoCapacitacionExists(int id)
        {
            return _context.tipoCapacitaciones.Any(e => e.Id == id);
        }
    }
}
