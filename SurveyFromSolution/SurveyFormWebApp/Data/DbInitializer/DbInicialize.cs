using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Data.DbInitializer
{
    public class DbInicialize : IDbInitializer
    {
        private readonly ApplicationDbContext context;

        public DbInicialize(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task Initialize()
        {
            try
            {
                if (context.Database.GetPendingMigrations().Count() > 0)
                {
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Migrations has been denied by a mistake", ex);
            }
        }
    }
}
