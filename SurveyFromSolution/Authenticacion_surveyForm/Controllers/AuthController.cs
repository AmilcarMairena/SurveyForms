using Authenticacion_surveyForm.Models;
using Authenticacion_surveyForm.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticacion_surveyForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService service;
        private readonly IUserRepository user;
        private readonly IConfiguration confi;

        public AuthController(ITokenService service, IUserRepository user, IConfiguration confi)
        {
            this.service = service;
            this.user = user;
            this.confi = confi;
        }
        
        [HttpPost]
        public IActionResult Authenticate([FromBody]Credentials credentials)
        {
            var validUser = user.GetUser(credentials);
            if(validUser != null)
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(30);
                return Ok(new
                {
                    access_token = this.service.BuildToken(confi["TokenSettings:key"],validUser, expiresAt),
                    expiresAt = expiresAt
                });
            }

            ModelState.AddModelError("Unauthorize", "Usuario ó contraseña incorrecta!");
            return BadRequest(ModelState);

        }
    }
}
