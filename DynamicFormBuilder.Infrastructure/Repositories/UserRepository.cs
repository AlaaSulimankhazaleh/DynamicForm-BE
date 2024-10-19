using DynamicFormBuilder.Domain.Entities;
using DynamicFormBuilder.Domain.IRepositories;
using DynamicFormBuilder.Infrastructure.Common;
using DynamicFormBuilder.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Infrastructure.IRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        private DynamicFormBuilderDbContext _context;

        public UserRepository(DynamicFormBuilderDbContext context) : base(context)
        {
            _context = context;

        }

    }
}