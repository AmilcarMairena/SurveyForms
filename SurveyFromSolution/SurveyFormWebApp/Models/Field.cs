using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SurveyFormWebApp.Models
{
    public class Field
    {
        [Key]
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsRequired { get; set; }
        public int DataType { get; set; }
        [Required]
        public Guid SurveyId { get; set; }
        [ForeignKey("SurveyId")]
        public Survey Survey { get; set; }
    }
}
