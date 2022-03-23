using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Models
{
    public class SurveyResult
    {
        [Key]
        public Guid Id { get; set; }
        public string SurveyName { get; set; }
        public string Desc { get; set; }
        [Required]
        public Guid ResultId { get; set; }
        [ForeignKey("ResultId")]
        public Result Result { get; set; }
        public IEnumerable<ValueResult> ListValues { get; set; }
    }
}
