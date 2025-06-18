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
    //[Authorize(Roles = "Administrator, Radnik")]
    public class AdresaDostaveController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdresaDostaveController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdresaDostave
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdresaDostave.ToListAsync());
        }

        // GET: AdresaDostave/Details/5
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adresaDostave = await _context.AdresaDostave
                .FirstOrDefaultAsync(m => m.adresaDostaveId == id);
            if (adresaDostave == null)
            {
                return NotFound();
            }

            return View(adresaDostave);
        }

        // GET: AdresaDostave/Create
        //[Authorize(Roles = "Administrator, Radnik, Korisnik")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdresaDostave/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("adresaDostaveId,grad,adresa,postanskiBroj")] AdresaDostave adresaDostave)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adresaDostave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adresaDostave);
        }




        // GET: AdresaDostave/Edit/5
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adresaDostave = await _context.AdresaDostave.FindAsync(id);
            if (adresaDostave == null)
            {
                return NotFound();
            }
            return View(adresaDostave);
        }

        // POST: AdresaDostave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> Edit(int id, [Bind("adresaDostaveId,grad,adresa,postanskiBroj")] AdresaDostave adresaDostave)
        {
            if (id != adresaDostave.adresaDostaveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adresaDostave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdresaDostaveExists(adresaDostave.adresaDostaveId))
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
            return View(adresaDostave);
        }

        // GET: AdresaDostave/Delete/5
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adresaDostave = await _context.AdresaDostave
                .FirstOrDefaultAsync(m => m.adresaDostaveId == id);
            if (adresaDostave == null)
            {
                return NotFound();
            }

            return View(adresaDostave);
        }

        // POST: AdresaDostave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adresaDostave = await _context.AdresaDostave.FindAsync(id);
            if (adresaDostave != null)
            {
                _context.AdresaDostave.Remove(adresaDostave);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdresaDostaveExists(int id)
        {
            return _context.AdresaDostave.Any(e => e.adresaDostaveId == id);
        }
    }
}
