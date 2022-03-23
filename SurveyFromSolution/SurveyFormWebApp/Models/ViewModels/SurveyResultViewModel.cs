using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Models.ViewModels
{
    public class SurveyResultViewModel
    {
        public Result result { get; set; }
        public Survey survey { get; set; }
        public SurveyResult surveyResult { get; set; }
        public ValueResult valueResult { get; set; }

    }
}
