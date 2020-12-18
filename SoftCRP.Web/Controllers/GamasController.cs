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
    public class GamasController : BaseController
    {
        private readonly DataContext _context;

        public GamasController(DataContext context)
        {
            _context = context;
        }

        // GET: Gamas
        public async Task<IActionResult> Index()
        {
            return View(await _context.gamas.ToListAsync());
        }

        // GET: Gamas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gama = await _context.gamas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gama == null)
            {
                return NotFound();
            }

            return View(gama);
        }

        // GET: Gamas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gamas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gama gama)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gama);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gama);
        }

        // GET: Gamas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gama = await _context.gamas.FindAsync(id);
            if (gama == null)
            {
                return NotFound();
            }
            return View(gama);
        }

        // POST: Gamas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gama gama)
        {
            if (id != gama.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gama);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamaExists(gama.Id))
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
            return View(gama);
        }

        // GET: Gamas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gama = await _context.gamas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gama == null)
            {
                return NotFound();
            }

            return View(gama);
        }

        // POST: Gamas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gama = await _context.gamas.FindAsync(id);
            _context.gamas.Remove(gama);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamaExists(int id)
        {
            return _context.gamas.Any(e => e.Id == id);
        }
    }
}
