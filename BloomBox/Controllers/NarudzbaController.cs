using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloomBox.Data;
using BloomBox.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace BloomBox.Controllers
{
    [Authorize(Roles = "Administrator, Radnik, Korisnik")]
    public class NarudzbaController : Controller
    {
       
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NarudzbaController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<Korisnik> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
             _userManager = userManager;
        }
        //nesto nesto
        //nestooooooooo

        /* public NarudzbaController(ApplicationDbContext context)
         {
             _context = context;
         } */

        [Authorize(Roles = "Administrator, Radnik")]

        // GET: Narudzba
        public async Task<IActionResult> Index()
        {
            return View(await _context.Narudzba.ToListAsync());
        }


        // GET: Narudzba/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                            .Include(n => n.adresaDostave)
                            .Include(n => n.Kupac)
                            .FirstOrDefaultAsync(n => n.narudzbaId == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        //[Authorize(Roles = "Administrator, Radnik, Korisnik")]
        public async Task<IActionResult> Create(Narudzba narudzba)
        {
           
            _context.AdresaDostave.Add(narudzba.adresaDostave);
            await _context.SaveChangesAsync();


            narudzba.adresaDostaveId = narudzba.adresaDostave.adresaDostaveId;


            var korisnik = await _userManager.GetUserAsync(User);
            if (korisnik == null)
            {
                return Unauthorized(); 
            }

            narudzba.KupacId = korisnik.Id;
            narudzba.datum = DateTime.Now;

            narudzba.ukupnaCijena = await CalculateCartTotalFromDatabase(korisnik.Id);

            
            _context.Narudzba.Add(narudzba);
            //Console.WriteLine("Before Save");
            await _context.SaveChangesAsync();
            //Console.WriteLine("After Save");

            var cartItems = await _context.ProizvodKorpa
                .Include(pk => pk.Proizvod)
                .Where(pk => pk.KupacId == korisnik.Id && pk.narudzbaId == null)
                .ToListAsync();

            foreach (var cartItem in cartItems)
            {
              
                cartItem.narudzbaId = narudzba.narudzbaId;
                _context.ProizvodKorpa.Update(cartItem);

                var proizvod = await _context.Proizvod.FindAsync(cartItem.proizvodId);
                if (proizvod != null)
                {
                    
                    proizvod.kolicinaNaStanju -= cartItem.kolicina;

                    if (proizvod.kolicinaNaStanju < 0)
                    {
                        proizvod.kolicinaNaStanju = 0;
                    }

                    _context.Proizvod.Update(proizvod);
                }
            }


            await _context.SaveChangesAsync();

         
            return RedirectToAction("KreirajCheckout", "Placanje", new { narudzbaId = narudzba.narudzbaId });
        }

        // GET: Narudzba/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba.FindAsync(id);
            if (narudzba == null)
            {
                return NotFound();
            }
            return View(narudzba);
        }

        // POST: Narudzba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("narudzbaId,datum,adresaDostave,ukupnaCijena")] Narudzba narudzba)
        {
            if (id != narudzba.narudzbaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narudzba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudzbaExists(narudzba.narudzbaId))
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
            return View(narudzba);
        }

        // GET: Narudzba/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                .FirstOrDefaultAsync(m => m.narudzbaId == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // POST: Narudzba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var narudzba = await _context.Narudzba.FindAsync(id);
            if (narudzba != null)
            {
                _context.Narudzba.Remove(narudzba);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarudzbaExists(int id)
        {
            return _context.Narudzba.Any(e => e.narudzbaId == id);
        }

        private async Task<List<ProizvodKorpaViewModel>> GetCartItemsFromDatabase(string kupacId)
        {
            return await _context.ProizvodKorpa
                .Include(pk => pk.Proizvod)
                .Where(pk => pk.KupacId == kupacId && pk.narudzbaId == null) // Only items not yet ordered
                .Select(pk => new ProizvodKorpaViewModel
                {
                    ProizvodId = pk.proizvodId,
                    Naziv = pk.Proizvod.ime,
                    Cijena = pk.Proizvod.cijena,
                    Kolicina = pk.kolicina,
                    UkupnaCijena = pk.Proizvod.cijena * pk.kolicina
                })
                .ToListAsync();
        }

        private async Task<double> CalculateCartTotalFromDatabase(string kupacId)
        {
            return await _context.ProizvodKorpa
                .Include(pk => pk.Proizvod)
                .Where(pk => pk.KupacId == kupacId && pk.narudzbaId == null) // Only items not yet ordered
                .SumAsync(pk => pk.Proizvod.cijena * pk.kolicina);
        }

        // Update your Create GET method to pass cart data from database
        [Authorize]
        [HttpGet]  
        public async Task<IActionResult> Create()
        {
            var korisnik = await _userManager.GetUserAsync(User);
            if (korisnik == null)
            {
                return Unauthorized();
            }

            var cartItems = await GetCartItemsFromDatabase(korisnik.Id);

            if (!cartItems.Any())
            {
                TempData["Error"] = "Vaša korpa je prazna.";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CartItems = cartItems;
            ViewBag.UkupnaCijena = await CalculateCartTotalFromDatabase(korisnik.Id);

            return View();
        }

        // ViewModel for cart display
        public class ProizvodKorpaViewModel
        {
            public int ProizvodId { get; set; }
            public string Naziv { get; set; }
            public double Cijena { get; set; }
            public int Kolicina { get; set; }
            public double UkupnaCijena { get; set; }
        }
    }
}
