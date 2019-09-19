using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly TokenClient _tokenClient;
        public AccountController(TokenClient _tokenClient)
        {

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginAsync(string userName, string password)
        {
            var tokenResponse = await _tokenClient.RequestPasswordTokenAsync(userName, password);
            if(tokenResponse.IsError)
            {
                return Content(tokenResponse.ErrorDescription);
            }
            else
            {
                var use = tokenResponse.AccessToken;
            }
            return View();
        }
    }
}