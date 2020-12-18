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
    public class TipoAnalisisController : BaseController
    {
        private readonly DataContext _context;
        private readonly ILogger<TipoAnalisisController> _logger;
        private readonly ILogRepository _logRepository;

        public TipoAnalisisController(
            DataContext context,
            ILogger<TipoAnalisisController> logger,
            ILogRepository logRepository)
        {
            _context = context;
            _logger = logger;
            _logRepository = logRepository;
        }

        // GET: TipoAnalisis

        public async Task<IActionResult> Index()
        {
            await _logRepository.SaveLogs("Get", "Obtiene lista de Tipo Análisis", "Tipo Análisis", User.Identity.Name);
            return View(await _context.TiposAnalisis.ToListAsync());
        }

        // GET: TipoAnalisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAnalisis = await _context.TiposAnalisis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoAnalisis == null)
            {
                return NotFound();
            }

            return View(tipoAnalisis);
        }

        // GET: TipoAnalisis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoAnalisis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoAnalisis tipoAnalisis)
        {
            if (ModelState.IsValid)
            {
                tipoAnalisis.isActive = true;
                _context.Add(tipoAnalisis);
                await _context.SaveChangesAsync();

                await _logRepository.SaveLogs("Crear", "Crear Tipo Análisis", "Tipo Análisis Id: "+tipoAnalisis.Id.ToString(), User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAnalisis);
        }

        // GET: TipoAnalisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAnalisis = await _context.TiposAnalisis.FindAsync(id);
            if (tipoAnalisis == null)
            {
                return NotFound();
            }
            return View(tipoAnalisis);
        }

        // POST: TipoAnalisis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipoAnalisis tipoAnalisis)
        {
            if (id != tipoAnalisis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoAnalisis);
                    await _context.SaveChangesAsync();
                    await _logRepository.SaveLogs("Editar", "Editar Tipo Análisis", "Tipo Análisis Id: "+tipoAnalisis.Id.ToString(), User.Identity.Name);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAnalisisExists(tipoAnalisis.Id))
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
            return View(tipoAnalisis);
        }

        // GET: TipoAnalisis/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tipoAnalisis = await _context.TiposAnalisis
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (tipoAnalisis == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tipoAnalisis);
        //}

        //[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoAnalisis = await _context.TiposAnalisis.FindAsync(id);

            await _logRepository.SaveLogs("Desactivar", "Desactivar Tipo Análisis: " + tipoAnalisis.Id.ToString(), "Tipo Análisis", User.Identity.Name);

            tipoAnalisis.isActive = false;

            //_context.TiposAnalisis.Remove(tipoAnalisis);
            _context.TiposAnalisis.Update(tipoAnalisis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Enable(int id)
        {
            var tipoAnalisis = await _context.TiposAnalisis.FindAsync(id);

            await _logRepository.SaveLogs("Activar", "Activar Tipo Análisis: " + tipoAnalisis.Id.ToString(), "Tipo Análisis", User.Identity.Name);

            tipoAnalisis.isActive = true;
            
            //_context.TiposAnalisis.Remove(tipoAnalisis);
            _context.TiposAnalisis.Update(tipoAnalisis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // POST: TipoAnalisis/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var tipoAnalisis = await _context.TiposAnalisis.FindAsync(id);
        //    _context.TiposAnalisis.Remove(tipoAnalisis);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool TipoAnalisisExists(int id)
        {
            return _context.TiposAnalisis.Any(e => e.Id == id);
        }
    }
}
