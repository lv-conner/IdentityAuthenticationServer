using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerHost.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }
        [HttpPost]
        public  IActionResult Login(LoginViewModel loginViewModel,string returnUrl)
        {
            return View();
        }
    }
}