using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveyFormWebApp.Extensions;
using SurveyFormWebApp.Models;
using SurveyFormWebApp.Models.ViewModels;
using SurveyFormWebApp.Repository.IRepository;
using SurveyFormWebApp.Static;
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
        [HttpPost]
        public IActionResult CollectField(string field)
        {
            Field fieldOut = JsonConvert.DeserializeObject<Field>(field);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.FieldList)))
            {
                List<Field> NewList = new List<Field>() { fieldOut };
                HttpContext.Session.SetObject(SD.FieldList, NewList);

                return Json(new { success = true });
            }
            var getList = HttpContext.Session.GetObject<List<Field>>(SD.FieldList);
            getList.Add(fieldOut);
            HttpContext.Session.SetObject(SD.FieldList, getList);

            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult GetAllFieldInputs()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.FieldList)))
            {
                return Json(new { data = new List<Field>() });
            }
            var FieldList = HttpContext.Session.GetObject<List<Field>>(SD.FieldList);
            return Json(new { data = FieldList });
          
        }
        #endregion
    }
}
