using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Application.DTO
{
 

    public record SignInResponseModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public string Token { get; set; }
    }
}
