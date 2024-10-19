using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Domain.IRepositories
{
    public interface  IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        IQueryable<T> Filter(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IQueryable<T> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        IQueryable<T> GetAll();
        IQueryable<T>  SingleOrDefault(  IQueryable<T> query, Expression<Func<T, bool>> predicate);
        Task UpdateAsync(T entity);
    }
}
