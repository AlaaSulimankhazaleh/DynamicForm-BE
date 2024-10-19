using DynamicFormBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Application.DTO
{
    public record FormModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }  
        public long UserId { get; set; }
        public ICollection<ControlDto> Controls { get; set; } = new List<ControlDto>();
    }
}
