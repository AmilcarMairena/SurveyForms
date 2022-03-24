using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Data.DbInitializer
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
}
