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

namespace SoftCRP.Web.Controllers
{
    [Authorize(Roles = "Admin,Renting")]
    public class CiudadesController : BaseController
    {
        private readonly DataContext _context;

        public CiudadesController(DataContext context)
        {
            _context = context;
        }

        // GET: Ciudades
        public async Task<IActionResult> Index()
        {
            return View(await _context.ciudades.ToListAsync());
        }

        // GET: Ciudades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await _context.ciudades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        // GET: Ciudades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ciudades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciudad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciudad);
        }

        // GET: Ciudades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await _context.ciudades.FindAsync(id);
            if (ciudad == null)
            {
                return NotFound();
            }
            return View(ciudad);
        }

        // POST: Ciudades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ciudad ciudad)
        {
            if (id != ciudad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciudad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadExists(ciudad.Id))
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
            return View(ciudad);
        }

        // GET: Ciudades/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ciudad = await _context.ciudades
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ciudad == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ciudad);
        //}

        // POST: Ciudades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var ciudad = await _context.ciudades.FindAsync(id);
        //    _context.ciudades.Remove(ciudad);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> Delete(int id)
        {
            var ciudad = await _context.ciudades.FindAsync(id);
            //await _logRepository.SaveLogs("Desactivar", "Descativar Tipo Trámites Id: " + tipoTramite.Id.ToString(), "Tipo Trámites", User.Identity.Name);
            ciudad.isActive = false;

            _context.ciudades.Update(ciudad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Enable(int id)
        {
            var ciudad = await _context.ciudades.FindAsync(id);
            //await _logRepository.SaveLogs("Activar", "Activar Tipo Trámites Id: " + tipoTramite.Id.ToString(), "Tipo Trámites", User.Identity.Name);
            ciudad.isActive = true;

            _context.ciudades.Update(ciudad);

            //_context.tipoTramites.Remove(tipoTramite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CiudadExists(int id)
        {
            return _context.ciudades.Any(e => e.Id == id);
        }
    }
}
