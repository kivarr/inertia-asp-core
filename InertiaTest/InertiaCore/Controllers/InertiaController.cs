using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InertiaAdapter;
using InertiaCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InertiaCore.Controllers
{
    public class InertiaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly HttpContext _httpContext;
        public InertiaController(
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userManager = userManager;
            _httpContext = httpContextAccessor.HttpContext;
            
            GenerateShareModel();
        }

        internal void GenerateShareModel(IEnumerable<ErrorViewModel> errors = null,
            IEnumerable<ErrorViewModel> successes = null)
        {
            var user = _httpContext.User.Identity.IsAuthenticated
                ? _userManager.FindByNameAsync(_httpContext.User.Identity.Name)
                : null;

            user?.Wait();
            
            Inertia.Share = new InertiaShareModel()
            {
                User = user?.Result,
                IsLogged = user != null,
                Errors = errors?.ToArray(),
                Successes = successes?.ToArray(),
            };
        }
    }

    public class InertiaShareModel
    {
        [JsonProperty("user")]
        public IdentityUser User { get; set; }
        [JsonProperty("isLogged")]
        public bool IsLogged { get; set; }
        
        [JsonProperty("errors")]
        public ErrorViewModel[] Errors { get; set; }
        [JsonProperty("successes")]
        public ErrorViewModel[] Successes { get; set; }
    }
}