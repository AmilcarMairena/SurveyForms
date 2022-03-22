using SurveyFormWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository.IRepository
{
    public interface IFieldRepository:IRepository<Field>
    {
        void Update(Field obj);
    }
}
