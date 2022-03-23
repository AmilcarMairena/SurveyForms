using SurveyFormWebApp.Data;
using SurveyFormWebApp.Models;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository
{
    public class ValueResultRepository : Repository<ValueResult>, IValueResultRepository
    {
        private readonly ApplicationDbContext db;

        public ValueResultRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public void Update(ValueResult obj)
        {
            db.ValueResult.Update(obj);
            db.SaveChanges();
        }
    }
}
