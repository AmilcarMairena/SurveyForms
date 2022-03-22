using SurveyFormWebApp.Data;
using SurveyFormWebApp.Models;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository
{
    public class FieldRepository : Repository<Field>, IFieldRepository
    {
        private readonly ApplicationDbContext db;

        public FieldRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public void Update(Field obj)
        {
            db.Field.Update(obj);
            db.SaveChanges();
        }
    }
}
