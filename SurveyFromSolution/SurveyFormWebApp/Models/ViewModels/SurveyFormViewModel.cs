using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Models.ViewModels
{
    public class SurveyFormViewModel
    {
        public Survey SurveyObj { get; set; }
        public List<Field> FieldList { get; set; }
    }
}
