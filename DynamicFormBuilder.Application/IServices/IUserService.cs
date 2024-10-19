using DynamicFormBuilder.Application.Common;
using DynamicFormBuilder.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Application.IServices
{
   
    public interface IUserService
    {
       Task<IResponseResult<RegisterResponseModel>> SignUp(RegisterRequestModel registerRequest);

        Task<IResponseResult<SignInResponseModel>> SignIn(SignInRequestModel registerRequest);
    }
}
