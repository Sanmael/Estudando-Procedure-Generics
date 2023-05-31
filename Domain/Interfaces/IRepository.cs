using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        public void Insert(T entity);
        public void Delete(T entity);
        public void Edit(T entity);
    }
}
