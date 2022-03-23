using Microsoft.EntityFrameworkCore;
using SurveyFormWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<SurveyResult> SurveyResult { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<ValueResult> ValueResult { get; set; }
    }
}
