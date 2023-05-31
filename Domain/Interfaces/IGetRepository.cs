using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGetRepository<T>
    {
        public T GetEntity(long id);
        public T Get(Expression<Func<T, bool>> Predicate);
        public IEnumerable<T> GetAll();
    }
}
