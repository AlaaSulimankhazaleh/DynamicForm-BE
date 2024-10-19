using DynamicFormBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Application.DTO
{
    public record ControlDto
    {
            public long Id { get; set; }
            public string Key { get; set; }
            public string Label { get; set; }
            public bool Required { get; set; }
            public int Order { get; set; }
            public string Value { get; set; }
            public string Type { get; set; } 
        
    }
}
