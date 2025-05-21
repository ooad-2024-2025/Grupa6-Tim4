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
    public class ProizvodParametriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProizvodParametriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProizvodParametri
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProizvodParametri.ToListAsync());
        }

        // GET: ProizvodParametri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvodParametri = await _context.ProizvodParametri
                .FirstOrDefaultAsync(m => m.proParametriId == id);
            if (proizvodParametri == null)
            {
                return NotFound();
            }

            return View(proizvodParametri);
        }

        // GET: ProizvodParametri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProizvodParametri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("proParametriId,parametarId,proizvodId")] ProizvodParametri proizvodParametri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proizvodParametri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proizvodParametri);
        }

        // GET: ProizvodParametri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvodParametri = await _context.ProizvodParametri.FindAsync(id);
            if (proizvodParametri == null)
            {
                return NotFound();
            }
            return View(proizvodParametri);
        }

        // POST: ProizvodParametri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("proParametriId,parametarId,proizvodId")] ProizvodParametri proizvodParametri)
        {
            if (id != proizvodParametri.proParametriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proizvodParametri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodParametriExists(proizvodParametri.proParametriId))
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
            return View(proizvodParametri);
        }

        // GET: ProizvodParametri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvodParametri = await _context.ProizvodParametri
                .FirstOrDefaultAsync(m => m.proParametriId == id);
            if (proizvodParametri == null)
            {
                return NotFound();
            }

            return View(proizvodParametri);
        }

        // POST: ProizvodParametri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proizvodParametri = await _context.ProizvodParametri.FindAsync(id);
            if (proizvodParametri != null)
            {
                _context.ProizvodParametri.Remove(proizvodParametri);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodParametriExists(int id)
        {
            return _context.ProizvodParametri.Any(e => e.proParametriId == id);
        }
    }
}
