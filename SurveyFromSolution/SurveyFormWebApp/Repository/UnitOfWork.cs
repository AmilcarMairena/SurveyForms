using SurveyFormWebApp.Data;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;

        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            survey = new SurveyRepository(db);
            Field = new FieldRepository(db);
        }
        public ISurveyRepository survey {get;private set;}

        public IFieldRepository Field { get; private set; }

        public void Dispose()
        {
            db.Dispose();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
