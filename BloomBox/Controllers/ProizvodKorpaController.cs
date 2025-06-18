using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BloomBox.Data;
using BloomBox.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BloomBox.Controllers
{
    [Authorize(Roles = "Administrator, Radnik")]
    public class ProizvodKorpaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProizvodKorpaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProizvodKorpa
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Uzimanje ID-a trenutnog korisnika
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Administrator") || User.IsInRole("Radnik"))
            {
                // Svi proizvodi u svim korpama
                var allCartItems = await _context.ProizvodKorpa
                    .Include(pk => pk.Proizvod)
                    .Include(pk => pk.Kupac)
                    .ToListAsync();

                double ukupnaCijena = allCartItems.Sum(item => item.Proizvod.cijena * item.kolicina);
                ViewBag.UkupnaCijena = ukupnaCijena;

                return View(allCartItems);
            }
            else
            {
                // Proizvodi samo za trenutnog korisnika
                var cartItems = await _context.ProizvodKorpa
                    .Include(pk => pk.Proizvod)
                    .Where(pk => pk.KupacId == userId && pk.narudzbaId == null)
                    .ToListAsync();

                double ukupnaCijena = cartItems.Sum(item => item.Proizvod.cijena * item.kolicina);
                ViewBag.UkupnaCijena = ukupnaCijena;

                return View(cartItems);
            }
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
            /* var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var korisnikKorpa = await _context.ProizvodKorpa
                .Include(pk => pk.proizvodId)
                .Where(pk => pk.KupacId == userId)
                .ToListAsync();

            
            var proizvodKorpa = await _context.ProizvodKorpa
                .FirstOrDefaultAsync(m => m.proizvodKorpaId == id);
            if (proizvodKorpa == null)
            {
                return NotFound();
            }
            

            return View(korisnikKorpa); */
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
        //[Authorize(Roles = "Administrator")]
        [AllowAnonymous]
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
        //[Authorize(Roles = "Administrator")]
        [AllowAnonymous]
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
