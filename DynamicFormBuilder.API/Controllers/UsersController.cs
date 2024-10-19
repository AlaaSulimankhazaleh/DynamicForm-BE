using DynamicFormBuilder.API.Common;
using DynamicFormBuilder.Application.Common;
using DynamicFormBuilder.Application.DTO;
using DynamicFormBuilder.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DynamicFormBuilder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("SignUp")]
        public async Task<IResponseResult<RegisterResponseModel>> SignUp(RegisterRequestModel registerRequest)
        {
            try
            { 
                
                return await _userService.SignUp(registerRequest);
            }   
            catch (Exception ex)
            {
                return new ResponseResult<RegisterResponseModel>
                {
                    Status = ResultStatus.Failed,
                    Errors = new List<string> { ex.Message ?? "An error occurred while SignUp" },
                    Data = null 
                };
            }
                
        }
        [HttpPost("SignIn")]
        public async Task<IResponseResult<SignInResponseModel>> SignIn(SignInRequestModel registerRequest)
        {
            try
            {
                var user = await _userService.SignIn(registerRequest);
                if (user.Status==ResultStatus.Success)
                {
                    user.Data.Token = JwtTokenGenerator.GenerateToken(user.Data);
                }

                return user;
            }
            catch (Exception ex)
            {
                return new ResponseResult<SignInResponseModel>
                {
                    Status = ResultStatus.Failed,
                    Errors = new List<string> { ex.Message ?? "An error occurred while SignIn" },
                    Data = null
                };
            }

        }
        

    }
}
