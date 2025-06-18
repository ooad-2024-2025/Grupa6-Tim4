// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BloomBox.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using BloomBox.Data; 

namespace BloomBox.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public LoginModel(SignInManager<Korisnik> signInManager, ILogger<LoginModel> logger, ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }


        //Purpose: Binds form data from Login.cshtml to this object.
        //[BindProperty]: Automatically links HTTP POST data to this property.
        [BindProperty]
        public InputModel Input { get; set; }


        //Purpose: Stores available external login providers (Google, Facebook, etc.).
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        

        //BITNO: Purpose: Redirects users after successful login (e.g., to the page they tried to access before authentication)
        //vrlo vjerovatno da cemo koristiti ovaj atribut kad se bude koristila korpa tjst pokretanje same narudzbe
        public string ReturnUrl { get; set; }


        //[TempData]: Persists error messages between requests (e.g., after redirects)
        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

           
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //Purpose: Fetches a list of configured external login providers (e.g., Google, Facebook)
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        
  

     

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var korisnik = await _userManager.FindByEmailAsync(Input.Email);
                    await PrebaciKorpuSaGostaNaRegistrovanog(korisnik.Id);

                    return LocalRedirect(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    return RedirectToPage("./Lockout");
                }

                ModelState.AddModelError(string.Empty, "Neuspješan pokušaj prijave.");
            }

            // Ako model nije validan ili login nije uspio
            return Page();
        }

        private async Task PrebaciKorpuSaGostaNaRegistrovanog(string korisnikId)
        {
            var guestSessionId = HttpContext.Session.GetString("GuestSessionId");

            if (string.IsNullOrEmpty(guestSessionId))
                return;

            var stavke = await _context.ProizvodKorpa
                .Where(pk => pk.SessionId == guestSessionId && pk.KupacId == null && pk.narudzbaId == null)
                .ToListAsync();

            foreach (var stavka in stavke)
            {
                var postojeca = await _context.ProizvodKorpa
                    .FirstOrDefaultAsync(p => p.KupacId == korisnikId && p.proizvodId == stavka.proizvodId && p.narudzbaId == null);

                if (postojeca != null)
                {
                    postojeca.kolicina += stavka.kolicina;
                    _context.ProizvodKorpa.Remove(stavka);
                }
                else
                {
                    stavka.KupacId = korisnikId;
                    stavka.SessionId = null;
                }
            }

            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("GuestSessionId");
        }
    
}
}
