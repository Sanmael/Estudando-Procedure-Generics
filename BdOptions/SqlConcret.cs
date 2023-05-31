using BdOptions.AppDataBase;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BdOptions
{
    public class SqlConcret : IRepository, IGetRepository
    {
        private readonly DbContext _appDbContext;

        public SqlConcret(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Insert<T>(T entity) where T : class
        {
            _appDbContext.Set<T>().Add(entity);
            _appDbContext.SaveChanges();
        }

        public void Delete<T>(long id) where T : class
        {
            T entity = _appDbContext.Set<T>().Find(id);
            _appDbContext.Set<T>().Remove(entity);
            _appDbContext.SaveChanges();
        }

        public void Edit<T>(T entity) where T : class
        {
            _appDbContext.Set<T>().Update(entity);
            _appDbContext.SaveChanges();
        }

        public T GetEntityById<T>(long id) where T : class
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _appDbContext.Set<T>().AsNoTracking().Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> GetAll<T>(Dictionary<string, object> keyValuePairs = null, string procedureName = null) where T : class
        {
            return _appDbContext.Set<T>().AsNoTracking().Select(x => x).ToList();
        }
    }
}
