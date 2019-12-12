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
    public class TipoAnalisisController : Controller
    {
        private readonly DataContext _context;

        public TipoAnalisisController(DataContext context)
        {
            _context = context;
        }

        // GET: TipoAnalisis

        public async Task<IActionResult> Index()
        {
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
                _context.Add(tipoAnalisis);
                await _context.SaveChangesAsync();
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
            _context.TiposAnalisis.Remove(tipoAnalisis);
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
