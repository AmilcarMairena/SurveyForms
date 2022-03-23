using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Models
{
    public class Result
    {
        [Key]
        public Guid ResultId { get; set; }
        [Required]
        public Guid SurveyId { get; set; }
        [ForeignKey("SurveyId")]
        public Survey Survey { get; set; }
        public SurveyResult SurveyResults { get; set; }

        public DateTime ResultDate { get; set; }
    }
}
