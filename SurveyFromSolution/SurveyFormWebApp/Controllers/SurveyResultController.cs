using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Controllers
{
    [Authorize]
    public class SurveyResultController : Controller
    {
      

        private readonly IUnitOfWork unit;
        private readonly IConfiguration config;

        public SurveyResultController(IUnitOfWork unit, IConfiguration config)
        {
            this.unit = unit;
            //this.token = token;
            this.config = config;
        }
        public IActionResult Index()
        {
            //string token = HttpContext.Session.GetString("Token");

            //if (token == null)
            //{
            //    return (RedirectToAction("Index", "Home"));
            //}

            //if (!this.token.ValidateToken(config["TokenSettings:Key"].ToString(), token))
            //{
            //    return (RedirectToAction("Index", "Home"));
            //}

            var result = this.unit.survey.GetAll(include: x => x.Include(a => a.Results)).ToList();
            return View(result);
        }
        public IActionResult SurveyFormRepo(string id)
        {
            var result = this.unit.survey.GetFirst(x => x.Id == Guid.Parse(id), include: a => a.Include(r => r.Results).ThenInclude(sr => sr.SurveyResults).ThenInclude(lv => lv.ListValues).ThenInclude(f => f.Field));
            return View(result);
        }
    }
}
