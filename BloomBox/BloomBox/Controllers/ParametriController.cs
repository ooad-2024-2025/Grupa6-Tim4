using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloomBox.Data;
using BloomBox.Models;

namespace BloomBox.Controllers
{
    public class ParametriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParametriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parametri
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parametri.ToListAsync());
        }

        // GET: Parametri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametri = await _context.Parametri
                .FirstOrDefaultAsync(m => m.parametarId == id);
            if (parametri == null)
            {
                return NotFound();
            }

            return View(parametri);
        }

        // GET: Parametri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parametri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("parametarId,ime")] Parametri parametri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parametri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parametri);
        }

        // GET: Parametri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametri = await _context.Parametri.FindAsync(id);
            if (parametri == null)
            {
                return NotFound();
            }
            return View(parametri);
        }

        // POST: Parametri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("parametarId,ime")] Parametri parametri)
        {
            if (id != parametri.parametarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parametri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParametriExists(parametri.parametarId))
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
            return View(parametri);
        }

        // GET: Parametri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametri = await _context.Parametri
                .FirstOrDefaultAsync(m => m.parametarId == id);
            if (parametri == null)
            {
                return NotFound();
            }

            return View(parametri);
        }

        // POST: Parametri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parametri = await _context.Parametri.FindAsync(id);
            if (parametri != null)
            {
                _context.Parametri.Remove(parametri);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParametriExists(int id)
        {
            return _context.Parametri.Any(e => e.parametarId == id);
        }
    }
}
