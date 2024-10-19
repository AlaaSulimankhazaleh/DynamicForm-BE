using DynamicFormBuilder.Application.IRepositories;
using DynamicFormBuilder.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Application.Servics
{
    public class ControService : IControService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;

        public ControService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }

    }
}
