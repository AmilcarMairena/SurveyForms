using Microsoft.AspNetCore.Mvc;
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
            return View();
        }
    }
}
