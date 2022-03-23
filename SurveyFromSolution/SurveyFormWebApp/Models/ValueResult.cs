using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Models
{
    public class ValueResult
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid SurveyResultId { get; set; }
        [ForeignKey("SurveyResultId")]
        public SurveyResult SurveyResult { get; set; }
       
        [Required]
        public Guid FieldId { get; set; }
        [ForeignKey("FieldId")]
        public Field Field { get; set; }
        public string Value { get; set; }
        public string Title { get; set; }

    }
}
