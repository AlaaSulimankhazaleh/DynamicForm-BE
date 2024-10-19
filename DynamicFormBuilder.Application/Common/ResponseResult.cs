using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Application.Common
{
    public class ResponseResult<T> : IResponseResult<T>
    {
        public List<string> Errors { get; set; }
        public ResultStatus Status { get; set; }
        public T Data { get; set; }
        public long TotalRecords { get; set; }
    }
}
