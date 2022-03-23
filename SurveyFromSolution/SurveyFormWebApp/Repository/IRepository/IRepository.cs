using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        T Get(string id);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool isTracking = true
            );

        T GetFirst(Expression<Func<T, bool>> filter = null, bool isTracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        void Add(T entity);
        void Add(IEnumerable<T> entity);
        void Remove(T entity);
        void Remove(IEnumerable<T> entity);
        bool Exist(Expression<Func<T, bool>> filter);
        bool Any();



    }
}
