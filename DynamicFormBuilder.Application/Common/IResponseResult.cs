using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Application.Common
{
    public interface IResponseResult<T>
    {
        List<string> Errors { get; set; }
        ResultStatus Status { get; set; }
        T Data { get; set; }
        long TotalRecords { get; set; }
    }
}
