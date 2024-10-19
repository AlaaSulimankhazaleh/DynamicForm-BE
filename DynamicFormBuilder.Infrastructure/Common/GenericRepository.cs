
using DynamicFormBuilder.Domain.IRepositories;
using DynamicFormBuilder.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Infrastructure.Common
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DynamicFormBuilderDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DynamicFormBuilderDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync(); // Ensure this is awaited
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                throw new Exception("Error adding entity", ex);
            }
        }

      

        public IQueryable<T> Filter(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            // Apply the include logic for related entities if provided
            if (include != null)
            {
                query = include(query).AsSplitQuery();
            }

            // Apply the where clause (lambda expression)
            if (where != null)
            {
                query = query.Where(where);
            }

            return query.AsNoTracking();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _dbSet;

            // Apply eager loading for the related entities
            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty).AsSplitQuery();
            }

            return query.Where(where).AsNoTracking();
        }

        public   IQueryable<T> GetAll()
        { 
            return _context.Set<T>().AsNoTracking();
        }

        public async  Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> SingleOrDefault(IQueryable<T> query, Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            T result = query.SingleOrDefault(predicate);

            // Return as IQueryable<T>, either the result or an empty list if null
            return result != null ? new List<T> { result }.AsQueryable() : Enumerable.Empty<T>().AsQueryable();
        }
            public async Task UpdateAsync(T entity)
        {
            try
            {
                // Attach the entity and mark it as modified
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;

                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;

            }
        }
    }
}
