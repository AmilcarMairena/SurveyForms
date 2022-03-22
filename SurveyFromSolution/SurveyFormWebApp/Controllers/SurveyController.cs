using Microsoft.AspNetCore.Mvc;
using SurveyFormWebApp.Models;
using SurveyFormWebApp.Models.ViewModels;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly IUnitOfWork unit;

        public SurveyController(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public IActionResult Index()
        {
            return View();
        }

        [BindProperty]
        public SurveyFormViewModel surveyForm { get; set; }

        public IActionResult Upsert(string? id)
        {
            surveyForm = new SurveyFormViewModel()
            {
                SurveyObj = new Models.Survey()
            };

            if(id == null)
            {
                return View(surveyForm);
            }

            return View(surveyForm);
        }

        #region  API CALLS
        [HttpGet]
        public IActionResult GetAllSurveyForms()
        {
            return Json(new { data = this.unit.survey.GetAll().ToList() });
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var getObj = this.unit.survey.GetFirst(x => x.Id == Guid.Parse(id));

            if(getObj == null)
            {
                return Json(new { success = false, message = "This record does'n exist there must an error consult your tecnical support!" });
            }
            this.unit.survey.Remove(getObj);
            this.unit.Save();
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult GetAllFieldInputs()
        {
            return Json(new { data = new Field() });
        }
        #endregion
    }
}
