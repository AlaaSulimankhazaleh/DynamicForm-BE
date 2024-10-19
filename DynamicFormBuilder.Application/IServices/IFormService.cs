using DynamicFormBuilder.Application.Common;
using DynamicFormBuilder.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Application.IServices
{
   
    public interface IFormService
    {
         Task<IResponseResult<FormModel>> Add(FormModel model);

         Task<IResponseResult<FormModel>> Update(FormModel model);
    }
}
