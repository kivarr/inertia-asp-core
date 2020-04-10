using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Humanizer;
using InertiaAdapter;
using InertiaCore.Extensions;
using InertiaCore.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InertiaCore.Models;
using InertiaCore.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace InertiaCore.Controllers
{
    public class HomeController : InertiaController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IComponentResolver<HomeController> _componentResolver;
        private bool _testBool = true;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, IComponentResolver<HomeController> componentResolver, 
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor, SignInManager<IdentityUser> signInManager) 
            : base(userManager, httpContextAccessor)
        {
            _logger = logger;
            _componentResolver = componentResolver;
            _signInManager = signInManager;
        }
        
        [Authorize]
        [InertiaGetStateFilter]
        public IActionResult Index()
        {
             //return whatever you want.
             var data = new { Id = 1 };
             //return Inertia Result.
             return Inertia.Render(_componentResolver.ResolveComponent(nameof(Index)), data);
        }

        [InertiaGetStateFilter]
        public IActionResult Privacy()
        {
            //return whatever you want.
            var data = new { Id = 1 };
            //return Inertia Result.
            var result = Inertia.Render(_componentResolver.ResolveComponent(nameof(Privacy)), data);
            return result;
        }

        [AllowAnonymous]
        [InertiaGetStateFilter]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return Inertia.Render(_componentResolver.ResolveComponent(nameof(Login)), new
            {
                returnUrl
            });
        }

        [HttpPost]
        [InertiaSaveStateFilter]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                lockoutOnFailure: true);
            
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            
            TempData.Add("InertiaError", "Login falhou");
            
            return RedirectToAction(nameof(Login), new { returnUrl });
        }

        [Authorize]
        public IActionResult Logged()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Test(string a)
        {
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class LoginViewModel : BaseViewModel
    {
        [Required]
        [EmailAddress]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        [JsonProperty("remember")]
        public bool RememberMe { get; set; }
    }

    public class BaseViewModel
    {
    }
}
