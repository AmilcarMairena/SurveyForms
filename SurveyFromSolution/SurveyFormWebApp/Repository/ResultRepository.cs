using SurveyFormWebApp.Data;
using SurveyFormWebApp.Models;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository
{
    public class ResultRepository : Repository<Result>, IResultRepository
    {
        private readonly ApplicationDbContext db;

        public ResultRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public void Update(Result obj)
        {
            db.Result.Update(obj);
            db.SaveChanges();
        }
    }
}
