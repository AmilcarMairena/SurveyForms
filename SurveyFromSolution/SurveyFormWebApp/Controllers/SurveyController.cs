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
        [HttpGet]
        public IActionResult GetField(string id)
        {
            var getObj = HttpContext.Session.GetObject<List<Field>>(SD.FieldList).Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();

            if(getObj == null)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true, data = getObj });
        }
        [HttpPost]
        public IActionResult CollectField(string field)
        {
            Field fieldOut = JsonConvert.DeserializeObject<Field>(field);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.FieldList)))
            {
                fieldOut.Id = Guid.NewGuid();
                List<Field> NewList = new List<Field>() { fieldOut };
                HttpContext.Session.SetObject(SD.FieldList, NewList);

                return Json(new { success = true });
            }
            var getList = HttpContext.Session.GetObject<List<Field>>(SD.FieldList);
            if (fieldOut.Id == null)
            {
                fieldOut.Id = Guid.NewGuid();
               
                getList.Add(fieldOut);
                HttpContext.Session.SetObject(SD.FieldList, getList);
            }
            else
            {
                getList = getList.Where(x => x.Id != fieldOut.Id).ToList();

                getList.Add(fieldOut);
                HttpContext.Session.SetObject(SD.FieldList, getList);
            }
            

            return Json(new { success = true });
        }
        [HttpDelete]
        public IActionResult DeleteField(string id)
        {
            var objList = HttpContext.Session.GetObject<List<Field>>(SD.FieldList);
            objList = objList.Where(x => x.Id != Guid.Parse(id)).ToList();
            HttpContext.Session.SetObject(SD.FieldList, objList);
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
