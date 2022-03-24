using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    [Authorize]
    public class SurveyController : Controller
    {
        private readonly IUnitOfWork unit;
        //private readonly ITokenService token;
        private readonly IConfiguration config;

        public SurveyController(IUnitOfWork unit,IConfiguration config)
        {
            this.unit = unit;
            this.config = config;
        }
        public IActionResult Index()
        {
            //string token = HttpContext.Session.GetString("Token");

            //if (token == null)
            //{
            //    return (RedirectToAction("Index","Home"));
            //}

            //if (!this.token.ValidateToken(config["TokenSettings:Key"].ToString(), token))
            //{
            //    return (RedirectToAction("Index","Home"));
            //}

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

            if (id == null)
            {
                return View(surveyForm);
            }

            surveyForm.SurveyObj = this.unit.survey.GetFirst(x => x.Id == Guid.Parse(id), include:a=>a.Include(f=>f.FieldList));
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.IsUpdate)))
            {
                var getList = surveyForm.SurveyObj.FieldList.OrderBy(x=>x.DataType);
                getList.ToList().ForEach(x => x.Survey = null);
                HttpContext.Session.SetString(SD.IsUpdate, "True");
                HttpContext.Session.SetObject(SD.FieldList, getList);
            }
            return View(surveyForm);
        }
        [HttpGet]
        public IActionResult LinkToSurvey(string returnUrl)
        {
            HttpContext.Session.Remove(SD.FieldList);
            HttpContext.Session.Remove(SD.IsUpdate);
            HttpContext.Session.Clear();
            ViewBag.url = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerateLink()
        {

          

            if (!ModelState.IsValid) return View(surveyForm);
            if(surveyForm.SurveyObj.Id == Guid.Empty)
            {
                // creating the form

                this.unit.survey.Add(surveyForm.SurveyObj);
                this.unit.Save();

                //retrieving the list of field from the session context

                surveyForm.FieldList = new List<Field>();

                surveyForm.FieldList = HttpContext.Session.GetObject<List<Field>>(SD.FieldList);

                surveyForm.FieldList.ForEach(x => x.SurveyId = surveyForm.SurveyObj.Id);

                this.unit.Field.Add(surveyForm.FieldList);


            }
            else
            {
                //updating the form
                this.unit.survey.Update(surveyForm.SurveyObj);

                var oldFields = this.unit.Field.GetAll(x => x.SurveyId == surveyForm.SurveyObj.Id);

                this.unit.Field.Remove(oldFields);

                this.unit.Save();
                var newFields = HttpContext.Session.GetObject<List<Field>>(SD.FieldList);
                newFields.ForEach(x => x.SurveyId = surveyForm.SurveyObj.Id);
                this.unit.Field.Add(newFields);
            }

            this.unit.Save();
            var callbackUrl = Url.Action("GetSurveyForm", "Home", new { id = surveyForm.SurveyObj.Id });
            return RedirectToAction(nameof(LinkToSurvey), new { returnUrl = callbackUrl });
        }

        #region  API CALLS
        [HttpGet]
        public IActionResult GetAllSurveyForms()
        {
            var obj = (from survey in this.unit.survey.GetAll().ToList()
                       select new { survey, url = Url.Action("GetSurveyForm", "Home", new { id = survey.Id }) }
                       );
            return Json(new { data =  obj });
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
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SD.IsUpdate)))
            {
                var getData = HttpContext.Session.GetObject<List<Field>>(SD.FieldList);
                return Json(new { data =getData});
            }

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
