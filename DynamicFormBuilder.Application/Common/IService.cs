using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Application.Common
{
    public  interface IService<T>
    {
        IResponseResult<T> Add(T entity);
        IResponseResult<T> Get(long Id);
        IResponseResult<IEnumerable<T>> AddRange(IEnumerable<T> models);
        IResponseResult<IEnumerable<T>> GetAll();
        IResponseResult<T> Remove(T entity);
        IResponseResult<T> Update(T entity);
        IResponseResult<IEnumerable<T>> RemoveRange(IEnumerable<T> models);
        IResponseResult<IEnumerable<T>> RemoveRangeByIDs(IEnumerable<long> IDs);
        IResponseResult<IEnumerable<T>> UpdateRange(IEnumerable<T> models);
    }
}
