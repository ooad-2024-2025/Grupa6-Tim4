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
    public class ValidacijaProizvodaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValidacijaProizvodaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ValidacijaProizvoda
        public async Task<IActionResult> Index()
        {
            return View(await _context.ValidacijaProizvoda.ToListAsync());
        }

        // GET: ValidacijaProizvoda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var validacijaProizvoda = await _context.ValidacijaProizvoda
                .FirstOrDefaultAsync(m => m.validacijaProizvodaId == id);
            if (validacijaProizvoda == null)
            {
                return NotFound();
            }

            return View(validacijaProizvoda);
        }

        // GET: ValidacijaProizvoda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ValidacijaProizvoda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("validacijaProizvodaId,status,stanjeProizvoda")] ValidacijaProizvoda validacijaProizvoda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(validacijaProizvoda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(validacijaProizvoda);
        }

        // GET: ValidacijaProizvoda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var validacijaProizvoda = await _context.ValidacijaProizvoda.FindAsync(id);
            if (validacijaProizvoda == null)
            {
                return NotFound();
            }
            return View(validacijaProizvoda);
        }

        // POST: ValidacijaProizvoda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("validacijaProizvodaId,status,stanjeProizvoda")] ValidacijaProizvoda validacijaProizvoda)
        {
            if (id != validacijaProizvoda.validacijaProizvodaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(validacijaProizvoda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValidacijaProizvodaExists(validacijaProizvoda.validacijaProizvodaId))
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
            return View(validacijaProizvoda);
        }

        // GET: ValidacijaProizvoda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var validacijaProizvoda = await _context.ValidacijaProizvoda
                .FirstOrDefaultAsync(m => m.validacijaProizvodaId == id);
            if (validacijaProizvoda == null)
            {
                return NotFound();
            }

            return View(validacijaProizvoda);
        }

        // POST: ValidacijaProizvoda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var validacijaProizvoda = await _context.ValidacijaProizvoda.FindAsync(id);
            if (validacijaProizvoda != null)
            {
                _context.ValidacijaProizvoda.Remove(validacijaProizvoda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValidacijaProizvodaExists(int id)
        {
            return _context.ValidacijaProizvoda.Any(e => e.validacijaProizvodaId == id);
        }
    }
}
