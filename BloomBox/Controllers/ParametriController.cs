using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloomBox.Data;
using BloomBox.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        [Authorize(Roles = "Administrator, Radnik")]
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
        [Authorize(Roles = "Administrator, Radnik")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parametri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Radnik")]
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
        [Authorize(Roles = "Administrator, Radnik")]
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
        [Authorize(Roles = "Administrator, Radnik")]
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
        [Authorize(Roles = "Administrator, Radnik")]
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
        [Authorize(Roles = "Administrator, Radnik")]
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

        public async Task<IActionResult> PronadjiProizvod(List<int> izabraniParametri)
        {
            if (izabraniParametri == null || !izabraniParametri.Any())
            {
                
                ViewBag.Greska = "Niste izabrali nijedan parametar.";
                var sviParametri = _context.Parametri.ToList();
                return View("Index", sviParametri);
            }
            var rezultat = new Dictionary<int, int>();

            foreach (var paramId in izabraniParametri)
            {
                var matches = _context.ProizvodParametri
                    .Where(pp => pp.parametarId == paramId)
                    .ToList();

                foreach (var match in matches)
                {
                    if (!rezultat.ContainsKey(match.proizvodId))
                        rezultat[match.proizvodId] = 0;

                    rezultat[match.proizvodId]++; 
                    
                }
            }

            // Najvise poklapanja
            var najProizvodId = rezultat
                .OrderByDescending(kv => kv.Value)
                .FirstOrDefault().Key;

            if (najProizvodId == 0)
            {
                ViewBag.Greska = "Ne postoji proizvod :P";
                var sviParametri = _context.Parametri.ToList();
                return View("Index", sviParametri);
            }

            var proizvod = _context.Proizvod.FirstOrDefault(p => p.proizvodId == najProizvodId);
            ViewBag.preporuceno = proizvod;
            var sviParametri2 = _context.Parametri.ToList();
            return View("Index", sviParametri2);
            //return RedirectToAction("Details", "Proizvod", new { id = najProizvodId });

        }
        private bool ParametriExists(int id)
        {
            return _context.Parametri.Any(e => e.parametarId == id);
        }
    }
}
