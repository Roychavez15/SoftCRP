using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;

namespace SoftCRP.Web.Controllers
{
    public class TipoNovedadesController : Controller
    {
        private readonly DataContext _context;

        public TipoNovedadesController(DataContext context)
        {
            _context = context;
        }

        // GET: TipoNovedades
        public async Task<IActionResult> Index()
        {
            return View(await _context.tipoNovedades.ToListAsync());
        }

        // GET: TipoNovedades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoNovedades = await _context.tipoNovedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoNovedades == null)
            {
                return NotFound();
            }

            return View(tipoNovedades);
        }

        // GET: TipoNovedades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoNovedades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo")] TipoNovedades tipoNovedades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoNovedades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoNovedades);
        }

        // GET: TipoNovedades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoNovedades = await _context.tipoNovedades.FindAsync(id);
            if (tipoNovedades == null)
            {
                return NotFound();
            }
            return View(tipoNovedades);
        }

        // POST: TipoNovedades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo")] TipoNovedades tipoNovedades)
        {
            if (id != tipoNovedades.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoNovedades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoNovedadesExists(tipoNovedades.Id))
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
            return View(tipoNovedades);
        }

        // GET: TipoNovedades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoNovedades = await _context.tipoNovedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoNovedades == null)
            {
                return NotFound();
            }

            return View(tipoNovedades);
        }

        // POST: TipoNovedades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoNovedades = await _context.tipoNovedades.FindAsync(id);
            _context.tipoNovedades.Remove(tipoNovedades);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoNovedadesExists(int id)
        {
            return _context.tipoNovedades.Any(e => e.Id == id);
        }
    }
}
