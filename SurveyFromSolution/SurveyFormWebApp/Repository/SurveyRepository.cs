using SurveyFormWebApp.Data;
using SurveyFormWebApp.Models;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository
{
    public class SurveyRepository : Repository<Survey>, ISurveyRepository
    {
        private readonly ApplicationDbContext db;

        public SurveyRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public void Update(Survey obj)
        {
            db.Survey.Update(obj);
            db.SaveChanges();
        }
    }
}
