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
    public class TipoTramitesController : Controller
    {
        private readonly DataContext _context;

        public TipoTramitesController(DataContext context)
        {
            _context = context;
        }

        // GET: TipoTramites
        public async Task<IActionResult> Index()
        {
            return View(await _context.tipoTramites.ToListAsync());
        }

        // GET: TipoTramites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTramite = await _context.tipoTramites
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTramite == null)
            {
                return NotFound();
            }

            return View(tipoTramite);
        }

        // GET: TipoTramites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoTramites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoTramite tipoTramite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoTramite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoTramite);
        }

        // GET: TipoTramites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTramite = await _context.tipoTramites.FindAsync(id);
            if (tipoTramite == null)
            {
                return NotFound();
            }
            return View(tipoTramite);
        }

        // POST: TipoTramites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipoTramite tipoTramite)
        {
            if (id != tipoTramite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoTramite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoTramiteExists(tipoTramite.Id))
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
            return View(tipoTramite);
        }

        // GET: TipoTramites/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tipoTramite = await _context.tipoTramites
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (tipoTramite == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tipoTramite);
        //}

        //// POST: TipoTramites/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var tipoTramite = await _context.tipoTramites.FindAsync(id);
        //    _context.tipoTramites.Remove(tipoTramite);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> Delete(int id)
        {
            var tipoTramite = await _context.tipoTramites.FindAsync(id);
            _context.tipoTramites.Remove(tipoTramite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool TipoTramiteExists(int id)
        {
            return _context.tipoTramites.Any(e => e.Id == id);
        }
    }
}
