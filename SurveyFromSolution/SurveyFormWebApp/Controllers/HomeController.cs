using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SurveyFormWebApp.Models;
using SurveyFormWebApp.Models.ViewModels;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unit;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unit)
        {
            _logger = logger;
            this.unit = unit;
        }

        public IActionResult Index()
        {
            return View();
        }
        [BindProperty]
        public SurveyResultViewModel surveyObj { get; set; }

      
        public IActionResult GetSurveyForm(string id)
        {
            surveyObj = new SurveyResultViewModel()
            {
                survey = this.unit.survey.GetFirst(x => x.Id == Guid.Parse(id),include:a=>a.Include(y=>y.FieldList))
               
            };
            var result = surveyObj.survey.FieldList.OrderBy(x => x.DataType);

            surveyObj.survey.FieldList = result;

            return View(surveyObj);
        }

        [HttpPost]
        public IActionResult CollectSurveyForm(string surveyId, string data)
        {
            string survId = JsonConvert.DeserializeObject<string>(surveyId);
            List<ValueResult> values = JsonConvert.DeserializeObject<List<ValueResult>>(data);

            //step 1 Creating new result base on the generated link

            Result mainResult = new Result()
            {
                ResultId = Guid.NewGuid(),
                SurveyId = Guid.Parse(survId),
                ResultDate = DateTime.Now
            };

            var surveyObj = this.unit.survey.GetFirst(x => x.Id == mainResult.SurveyId);

            SurveyResult survResult = new SurveyResult()
            {
                Id = Guid.NewGuid(),
                SurveyName = surveyObj.Name,
                Desc = surveyObj.SurveyDescription,
               ResultId = mainResult.ResultId
            };

            //finaly we add all our values to the db

            values.ForEach(x => x.SurveyResultId = survResult.Id);


            //saving all the changes
            try
            {
                this.unit.Result.Add(mainResult);
                this.unit.SurveyResult.Add(survResult);
                this.unit.ValueResult.Add(values);

                this.unit.Save();
            }
            catch 
            {

                return Json(new { success = false, message="Error del servidor!" });
            }



            return Json(new { success = true, url = Url.Action(nameof(LandingSurveyFormSubmition),"Home") });
        }
        public IActionResult LandingSurveyFormSubmition()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
