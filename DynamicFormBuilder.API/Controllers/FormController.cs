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
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpPost("Add")]
        public async Task<IResponseResult<FormModel>> Add(FormModel model)
        {
            try
            { 
                
                return await _formService.Add(model);
            }   
            catch (Exception ex)
            {
                return new ResponseResult<FormModel>
                {
                    Status = ResultStatus.Failed,
                    Errors = new List<string> { ex.Message ?? "An error occurred while SignUp" },
                    Data = null 
                };
            }
                
        }
        [HttpPost("Update")]
        public async Task<IResponseResult<FormModel>> Update(FormModel model)
        {
            try
            {

                return await _formService.Update(model);
            }
            catch (Exception ex)
            {
                return new ResponseResult<FormModel>
                {
                    Status = ResultStatus.Failed,
                    Errors = new List<string> { ex.Message ?? "An error occurred while SignUp" },
                    Data = null
                };
            }

        }


    }
}
