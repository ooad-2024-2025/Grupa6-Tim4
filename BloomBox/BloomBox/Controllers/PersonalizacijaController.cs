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
    public class PersonalizacijaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonalizacijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personalizacija
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personalizacija.ToListAsync());
        }

        // GET: Personalizacija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizacija = await _context.Personalizacija
                .FirstOrDefaultAsync(m => m.personalizacijaId == id);
            if (personalizacija == null)
            {
                return NotFound();
            }

            return View(personalizacija);
        }

        // GET: Personalizacija/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personalizacija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("personalizacijaId,maximalnaCijena,parametar,proizvodId")] Personalizacija personalizacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalizacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personalizacija);
        }

        // GET: Personalizacija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizacija = await _context.Personalizacija.FindAsync(id);
            if (personalizacija == null)
            {
                return NotFound();
            }
            return View(personalizacija);
        }

        // POST: Personalizacija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("personalizacijaId,maximalnaCijena,parametar,proizvodId")] Personalizacija personalizacija)
        {
            if (id != personalizacija.personalizacijaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalizacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalizacijaExists(personalizacija.personalizacijaId))
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
            return View(personalizacija);
        }

        // GET: Personalizacija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizacija = await _context.Personalizacija
                .FirstOrDefaultAsync(m => m.personalizacijaId == id);
            if (personalizacija == null)
            {
                return NotFound();
            }

            return View(personalizacija);
        }

        // POST: Personalizacija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalizacija = await _context.Personalizacija.FindAsync(id);
            if (personalizacija != null)
            {
                _context.Personalizacija.Remove(personalizacija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalizacijaExists(int id)
        {
            return _context.Personalizacija.Any(e => e.personalizacijaId == id);
        }
    }
}
