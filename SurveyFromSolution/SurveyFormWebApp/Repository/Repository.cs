using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SurveyFormWebApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _db;
        internal DbSet<T> dbSet;
        public Repository(DbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T entity) => this.dbSet.Add(entity);

        public void Add(IEnumerable<T> entity) => this.dbSet.AddRange(entity);

        public bool Any() => this.dbSet.Any();

        public bool Exist(Expression<Func<T, bool>> filter) => this.dbSet.Any(filter);

        public T Get(string id) => dbSet.Find(id);

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isTracking = true)
        {
            IQueryable<T> query = this.dbSet;


            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(include != null)
            {
                query = include(query);
            }
            if(orderBy != null)
            {
                return orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public T GetFirst(Expression<Func<T, bool>> filter = null, bool isTracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = this.dbSet;


            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(include != null)
            {
                query = include(query);
            }
           
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return query.FirstOrDefault();
        }


        public void Remove(T entity) => this.dbSet.Remove(entity);

        public void Remove(IEnumerable<T> entity) => this.dbSet.RemoveRange(entity);

        
    }
}
