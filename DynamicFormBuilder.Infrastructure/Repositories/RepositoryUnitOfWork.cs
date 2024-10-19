using DynamicFormBuilder.Application.IRepositories;
using DynamicFormBuilder.Domain.IRepositories;
using DynamicFormBuilder.Infrastructure.Common;
using DynamicFormBuilder.Infrastructure.Context;
using DynamicFormBuilder.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace DynamicFormBuilder.Infrastructure.Repositories
{

    public class RepositoryUnitOfWork : IRepositoryUnitOfWork
    {
        public IUserRepository Users { get ; set ; }

        public IControlRepository  Controls { get; set; }
        public IFormRepository Forms { get; set; }

        private DynamicFormBuilderDbContext _context;
        public RepositoryUnitOfWork(DynamicFormBuilderDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Controls = new ControlRepository(_context);
            Forms = new FormRepository(_context);
        }

        

        public void Dispose()
        {
            _context.Dispose();
        }

        void IDisposable.Dispose()
        { 
        }
    }
}
