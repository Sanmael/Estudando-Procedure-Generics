using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository
    {
        public void Insert<T>(T entity) where T : class;
        public void Delete<T>(long id) where T : class;
        public void Edit<T>(T entity) where T : class;
    }
}
