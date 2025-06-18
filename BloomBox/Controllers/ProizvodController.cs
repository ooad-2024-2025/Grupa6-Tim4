using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloomBox.Data;
using BloomBox.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BloomBox.Controllers
{
    [Authorize(Roles = "Administrator, Radnik")]
    public class ProizvodController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProizvodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proizvod
        [AllowAnonymous] //kako bi mogli pregledati i neregistrvani korisnici sajta 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proizvod.ToListAsync());
        }

        // GET: Proizvod/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvod
                .FirstOrDefaultAsync(m => m.proizvodId == id);
            if (proizvod == null)
            {
                return NotFound();
            }
            var parametri = await (from pp in _context.ProizvodParametri
                                   join p in _context.Parametri on pp.parametarId equals p.parametarId
                                   where pp.proizvodId == id
                                   select p).Distinct().ToListAsync();

            ViewBag.Parametri = parametri;

            return View(proizvod);
        }

        // GET: Proizvod/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proizvod/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("proizvodId,ime,cijena,uvodznikId,kategorija,slikaURL,opis,kolicinaNaStanju")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proizvod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proizvod);
        }

        // GET: Proizvod/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod == null)
            {
                return NotFound();
            }
            ViewBag.Kategorije = new SelectList(Enum.GetValues(typeof(Kategorija)));
            return View(proizvod);
        }

        // POST: Proizvod/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("proizvodId,ime,cijena,uvodznikId,kategorija,slikaURL,opis,kolicinaNaStanju")] Proizvod proizvod)
        {
            if (id != proizvod.proizvodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proizvod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodExists(proizvod.proizvodId))
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
            return View(proizvod);
        }

        // GET: Proizvod/Delete/5
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvod
                .FirstOrDefaultAsync(m => m.proizvodId == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // POST: Proizvod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod != null)
            {
                _context.Proizvod.Remove(proizvod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodExists(int id)
        {
            return _context.Proizvod.Any(e => e.proizvodId == id);
        }

        [AllowAnonymous]
        /*public async Task<IActionResult> Dodaj(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Proizvod proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod == null) return NotFound();

            ProizvodKorpa existingItem = null;
            var guestId = HttpContext.Session.GetString("GuestSessionId");
            if (guestId == null)
            {
                guestId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("GuestSessionId", guestId);
            }


            if (userId != null)
            {
                existingItem = await _context.ProizvodKorpa
                    .FirstOrDefaultAsync(pk => pk.proizvodId == id && pk.KupacId == userId);
            }
            else
            {
               // var guestId = HttpContext.Session.GetString("GuestSessionId");
                if (guestId != null)
                {
                    existingItem = await _context.ProizvodKorpa
                        .FirstOrDefaultAsync(pk => pk.proizvodId == id && pk.SessionId == guestId);
                }
            } 
            //koliko go ovo glupo izgledalo
            //ne diraj
            if (existingItem!=null && existingItem.narudzbaId != null)
                {
                    _context.ProizvodKorpa.Remove(existingItem);
                }

            if (existingItem != null && existingItem.narudzbaId==null)
            {
                existingItem.kolicina++;
            }
            else
            {
              
                var noviItem = new ProizvodKorpa
                    {
                        proizvodId = id,
                        kolicina = 1
                    };

                    if (userId != null)
                    {
                        noviItem.KupacId = userId;
                    }
                    else
                    {
                        var guestId = HttpContext.Session.GetString("GuestSessionId");
                        if (guestId == null)
                        {
                            guestId = Guid.NewGuid().ToString();
                            HttpContext.Session.SetString("GuestSessionId", guestId);
                        }
                        noviItem.SessionId = guestId;
                    } 

                    _context.ProizvodKorpa.Add(noviItem);
                
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }*/
        public async Task<IActionResult> Dodaj(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Proizvod proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod == null) return NotFound();

            ProizvodKorpa existingItem = null;

            // Dobij guestId iz sesije ili ga kreiraj ako ne postoji
            var guestId = HttpContext.Session.GetString("GuestSessionId");
            if (guestId == null)
            {
                guestId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("GuestSessionId", guestId);
            }

            if (userId != null)
            {
                // Ako je korisnik prijavljen, traži stavku po korisničkom ID-u
                existingItem = await _context.ProizvodKorpa
                    .FirstOrDefaultAsync(pk => pk.proizvodId == id && pk.KupacId == userId && pk.narudzbaId == null);
            }
            else
            {
                // Ako nije prijavljen, traži stavku po guest sessionId
                existingItem = await _context.ProizvodKorpa
                    .FirstOrDefaultAsync(pk => pk.proizvodId == id && pk.SessionId == guestId && pk.narudzbaId == null);
            }

            // Ako stavka postoji, i nije vezana za narudžbu, povećaj količinu
            if (existingItem != null)
            {
                existingItem.kolicina++;
            }
            else
            {
                // Kreiraj novu stavku i postavi ili KupacId ili SessionId
                var noviItem = new ProizvodKorpa
                {
                    proizvodId = id,
                    kolicina = 1
                };

                if (userId != null)
                {
                    noviItem.KupacId = userId;
                }
                else
                {
                    noviItem.SessionId = guestId;
                }

                _context.ProizvodKorpa.Add(noviItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Administrator, Radnik")]
        public IActionResult DodajParametre(int id)
        {
            var proizvod = _context.Proizvod.Find(id);
            if (proizvod == null)
                return NotFound();

            ViewBag.ProizvodId = id;
            ViewBag.Parametri = _context.Parametri.ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Radnik")]
        public IActionResult DodajParametre(int proizvodId, List<int> izabraniParametri)
        {
            foreach (var paramId in izabraniParametri)
            {
                var postoji = _context.ProizvodParametri.Any(pp => pp.proizvodId == proizvodId && pp.parametarId == paramId);
                if (!postoji)
                {
                    _context.ProizvodParametri.Add(new ProizvodParametri
                    {
                        proizvodId = proizvodId,
                        parametarId = paramId
                    });
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Details", new { id = proizvodId });
        }



    }

}
