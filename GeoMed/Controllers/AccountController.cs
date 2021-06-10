using GeoMed.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeoMed.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties() { 
               RedirectUri = "/Home/Index",
            }, "oidc");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            return SignOut(new AuthenticationProperties()
            {
                RedirectUri = "/Home/Index",
            }, "Cookies", "oidc");
        }

        [HttpGet]
        public IActionResult Example()
        {
            return View();
        }


    }
}
