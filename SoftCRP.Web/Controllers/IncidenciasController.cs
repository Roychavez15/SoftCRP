using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers
{
    [Authorize(Roles = "Admin,Renting")]
    public class IncidenciasController : BaseController
    {
        private readonly DataContext _context;
        private readonly IIncidenciasRepository _incidenciasRepository;

        public IncidenciasController(DataContext context,
            IIncidenciasRepository incidenciasRepository)
        {
            _context = context;
            _incidenciasRepository = incidenciasRepository;
        }

        // GET: Incidencias
        public async Task<IActionResult> Index()
        {
            return View(await _incidenciasRepository.GetListIncidenciasAsync());
        }

        // GET: Incidencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var incidencia = await _context.incidencias
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var incidencia = await _incidenciasRepository.GetIncidenciaByIdAsync(id.Value);
            if (incidencia == null)
            {
                return NotFound();
            }

            return View(incidencia);
        }

        // GET: Incidencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Incidencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Incidencia incidencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incidencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incidencia);
        }

        // GET: Incidencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var incidencia = await _context.incidencias.FindAsync(id);
            var incidencia = await _incidenciasRepository.GetIncidenciaByIdAsync(id.Value);
            if (incidencia == null)
            {
                return NotFound();
            }
            return View(incidencia);
        }

        // POST: Incidencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Incidencia incidencia)
        {
            if (id != incidencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incidencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidenciaExists(incidencia.Id))
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
            return View(incidencia);
        }

        // GET: Incidencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidencia = await _context.incidencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incidencia == null)
            {
                return NotFound();
            }

            return View(incidencia);
        }

        // POST: Incidencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incidencia = await _context.incidencias.FindAsync(id);
            _context.incidencias.Remove(incidencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidenciaExists(int id)
        {
            return _context.incidencias.Any(e => e.Id == id);
        }
    }
}
