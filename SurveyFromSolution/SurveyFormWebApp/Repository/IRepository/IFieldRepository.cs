using SurveyFormWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository.IRepository
{
    public interface IResultRepository : IRepository<Result>
    {
        void Update(Result obj);
    }
}
