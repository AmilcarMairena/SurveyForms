using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Controllers
{
    public class SurveyResultController : Controller
    {
        private readonly IUnitOfWork unit;

        public SurveyResultController(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public IActionResult Index()
        {
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
