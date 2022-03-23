using SurveyFormWebApp.Data;
using SurveyFormWebApp.Models;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository
{
    public class SurveyResultRepository : Repository<SurveyResult>, ISurverResultRepository
    {
        private readonly ApplicationDbContext db;

        public SurveyResultRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public void Update(SurveyResult obj)
        {
            db.SurveyResult.Update(obj);
            db.SaveChanges();
        }
    }
}
