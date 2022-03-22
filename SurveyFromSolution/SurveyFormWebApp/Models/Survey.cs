using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Models
{
    public class Survey
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "El titulo: {0} debe tener al menos {2} y un maximo de {1}", MinimumLength = 6)]
        public string Name { get; set; }
        public string SurveyDescription { get; set; }
        public IEnumerable<Field> FieldList { get; set; }
    }
}
