using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGetRepository
    {
        public T GetEntityById<T>(long id) where T : class;
        public T Get<T>(Expression<Func<T, bool>> Predicate) where T : class;
        public IEnumerable<T> GetAll<T>(Dictionary<string, object> keyValuePairs = null, string procedureName = null) where T : class;
    }
}
