using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SurveyFormWebApp.Models;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration confi;
        private readonly IHttpClientFactory httpClientFactory;
        private string generatedToken = null;
        public AccountController(IConfiguration confi, IHttpClientFactory httpClientFactory)
        {
            this.confi = confi;
            this.httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public IActionResult AccessDenied() => View();
        
        [AllowAnonymous]
        public IActionResult Login(string returnUrl =null)
        {
            ViewBag.url = returnUrl;
            Credentials credentials = new Credentials();
            return View(credentials);
        }
       
        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Login(Credentials userCredentials, string returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                var httpClient = httpClientFactory.CreateClient("JWTAuthentication");
                var res = await httpClient.PostAsJsonAsync("/api/auth", userCredentials);
                try
                {
                    res.EnsureSuccessStatusCode();
                }
                catch 
                {
                    return RedirectToAction(nameof(AccessDenied));

                }
               
                string strJwt = await res.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<TokenDTO>(strJwt);
                if(token != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                    HttpContext.Session.SetString("Token", token.AccessToken);

                    return LocalRedirect(returnUrl);

                }
                else
                {
                    
                        return RedirectToAction(nameof(AccessDenied));
                    
                }

              
                
            }
         
            ModelState.AddModelError("Error", "Usuario incorrecto");
            return View(userCredentials);
        }
    }
}
