using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicFormBuilder.Domain.IRepositories; 
namespace DynamicFormBuilder.Application.IRepositories
{
    public interface IRepositoryUnitOfWork : IDisposable
    {
        IUserRepository  Users { get; set; }
        IControlRepository Controls { get; set; }

        IFormRepository Forms { get; set; }
    }
}
