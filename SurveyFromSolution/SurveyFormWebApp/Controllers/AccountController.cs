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
                res.EnsureSuccessStatusCode();
                string strJwt = await res.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<TokenDTO>(strJwt);
                if(token != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                    HttpContext.Session.SetString("Token", token.AccessToken);

                    return RedirectToAction("Index", "Survey");

                }
                else
                {
                    
                        return RedirectToAction(nameof(AccessDenied));
                    
                }

              
                
            }
            //if(string.IsNullOrEmpty(userCredentials.email) || string.IsNullOrEmpty(userCredentials.Password))
            //{
            //    return BadRequest();
            //}

            //IActionResult respose = Unauthorized();

            //var validUser = user.GetUser(userCredentials);
            //if (validUser != null)
            //{
            //    generatedToken = token.BuildToken(confi["TokenSettings:key"].ToString(),validUser);  
            //    if(generatedToken != null)
            //    {
            //        HttpContext.Session.SetString("Token", generatedToken);

            //        return RedirectToAction("Index", "Survey");
            //    }
            //    else
            //    {
            //        return BadRequest();
            //    }
            //}
            ModelState.AddModelError("Error", "Usuario incorrecto");
            return View(userCredentials);
        }
    }
}
