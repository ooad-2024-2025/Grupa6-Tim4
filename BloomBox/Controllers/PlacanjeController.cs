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
using Stripe.Checkout;

namespace BloomBox.Controllers

{
    //[Authorize(Roles = "Administrator, Radnik")]
    public class PlacanjeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public PlacanjeController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration=configuration;
        }

        // GET: Placanje
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Placanje.ToListAsync());
        }

        // GET: Placanje/Details/5
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placanje = await _context.Placanje
                .FirstOrDefaultAsync(m => m.placanjeId == id);
            if (placanje == null)
            {
                return NotFound();
            }

            return View(placanje);
        }

        // GET: Placanje/Create
        [Authorize(Roles = "Administrator, Radnik")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Placanje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> Create([Bind("placanjeId,narudzbaId,datum,status,transakcijskiId")] Placanje placanje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placanje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(placanje);
        }

        // GET: Placanje/Edit/5
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placanje = await _context.Placanje.FindAsync(id);
            if (placanje == null)
            {
                return NotFound();
            }
            return View(placanje);
        }

        // POST: Placanje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> Edit(int id, [Bind("placanjeId,narudzbaId,datum,status,transakcijskiId")] Placanje placanje)
        {
            if (id != placanje.placanjeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placanje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacanjeExists(placanje.placanjeId))
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
            return View(placanje);
        }

        // GET: Placanje/Delete/5
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placanje = await _context.Placanje
                .FirstOrDefaultAsync(m => m.placanjeId == id);
            if (placanje == null)
            {
                return NotFound();
            }

            return View(placanje);
        }

        // POST: Placanje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Radnik")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placanje = await _context.Placanje.FindAsync(id);
            if (placanje != null)
            {
                _context.Placanje.Remove(placanje);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacanjeExists(int id)
        {
            return _context.Placanje.Any(e => e.placanjeId == id);
        }

        [HttpGet]
        public async Task<IActionResult> KreirajCheckout(int narudzbaId)
        {
            var narudzba = await GetNarudzba(narudzbaId);
            var domain = "http://localhost:5209"; // lokalni fajl

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "bam",
                            UnitAmount = (long)(narudzba.ukupnaCijena * 100),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Narudžba #" + narudzba.narudzbaId
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = domain + "/Placanje/Uspjeh?narudzbaId=" + narudzba.narudzbaId,
                CancelUrl = domain + "/Placanje/Otkazano"
            };
            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }
        [Authorize(Roles = "Administrator, Radnik, Korisnik")]
        public async Task<IActionResult> Uspjeh(int narudzbaId)
        {
            var placanje = new Placanje
            {
                narudzbaId = narudzbaId,
                datum = DateTime.Now,
                status = Status.prihvaceno,
                transakcijskiId = "stripe-checkout" 
            };

            await SavePlacanje(placanje);
            return View();
        }
        [Authorize(Roles = "Administrator, Radnik, Korisnik")]
        public IActionResult Otkazano()
        {
            ViewBag.Message = "Plaćanje je otkazano. Možete pokušati ponovo.";
            return View();
        }

        private async Task<Narudzba> GetNarudzba(int id)
        {
            return _context.Narudzba
               // .Include(n => n.AdresaDostave) 
                .FirstOrDefault(n => n.narudzbaId == id);
        }

        private async Task SavePlacanje(Placanje p)
        {
            _context.Add(p);
            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }
    }
}
