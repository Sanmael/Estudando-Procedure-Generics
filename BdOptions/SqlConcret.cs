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
    public class SqlConcret<T> : IRepository<T>, IGetRepository<T> where T : class
    {
        private readonly DbContext _appDbContext;

        public SqlConcret(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            _appDbContext.SaveChanges();
        }
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().AsNoTracking().Where(predicate).FirstOrDefault();
        }
        public void Insert(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            _appDbContext.SaveChanges();
        }
        public IEnumerable<T> GetAll()
        {
            return _appDbContext.Set<T>().AsNoTracking().Select(x => x).ToList();
        }

        public void Edit(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
            _appDbContext.SaveChanges();
        }

        public T GetEntity(long id)
        {
            return _appDbContext.Set<T>().Find(id);
        }
    }
}
