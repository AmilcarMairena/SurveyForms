using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ISurveyRepository survey { get; }
        IFieldRepository Field { get; }
        void Save();
    }
}
