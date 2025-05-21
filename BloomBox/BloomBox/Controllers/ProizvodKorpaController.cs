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
    public class ProizvodKorpaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProizvodKorpaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProizvodKorpa
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProizvodKorpa.ToListAsync());
        }

        // GET: ProizvodKorpa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvodKorpa = await _context.ProizvodKorpa
                .FirstOrDefaultAsync(m => m.proizvodKorpaId == id);
            if (proizvodKorpa == null)
            {
                return NotFound();
            }

            return View(proizvodKorpa);
        }

        // GET: ProizvodKorpa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProizvodKorpa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("proizvodKorpaId,proizvodId,narudzbaId,kolicina")] ProizvodKorpa proizvodKorpa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proizvodKorpa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proizvodKorpa);
        }

        // GET: ProizvodKorpa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvodKorpa = await _context.ProizvodKorpa.FindAsync(id);
            if (proizvodKorpa == null)
            {
                return NotFound();
            }
            return View(proizvodKorpa);
        }

        // POST: ProizvodKorpa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("proizvodKorpaId,proizvodId,narudzbaId,kolicina")] ProizvodKorpa proizvodKorpa)
        {
            if (id != proizvodKorpa.proizvodKorpaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proizvodKorpa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodKorpaExists(proizvodKorpa.proizvodKorpaId))
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
            return View(proizvodKorpa);
        }

        // GET: ProizvodKorpa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvodKorpa = await _context.ProizvodKorpa
                .FirstOrDefaultAsync(m => m.proizvodKorpaId == id);
            if (proizvodKorpa == null)
            {
                return NotFound();
            }

            return View(proizvodKorpa);
        }

        // POST: ProizvodKorpa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proizvodKorpa = await _context.ProizvodKorpa.FindAsync(id);
            if (proizvodKorpa != null)
            {
                _context.ProizvodKorpa.Remove(proizvodKorpa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodKorpaExists(int id)
        {
            return _context.ProizvodKorpa.Any(e => e.proizvodKorpaId == id);
        }
    }
}
