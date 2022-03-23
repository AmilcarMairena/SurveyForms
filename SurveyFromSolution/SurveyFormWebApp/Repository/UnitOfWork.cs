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
            Result = new ResultRepository(db);
            SurveyResult = new SurveyResultRepository(db);
            ValueResult = new ValueResultRepository(db);
        }
        public ISurveyRepository survey {get;private set;}

        public IFieldRepository Field { get; private set; }

        public IResultRepository Result { get; private set; }

        public ISurverResultRepository SurveyResult { get; private set; }

        public IValueResultRepository ValueResult { get; private set; }

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
