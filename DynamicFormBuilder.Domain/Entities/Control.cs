using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Domain.Entities
{
    public class Control
    {
        public long  Id { get; set; }
        public string Key { get; set; }
        public string Label { get; set; }
        public bool Required { get; set; }
        public int Order { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string? OptionsJson { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } 
        public long  FormId { get; set; }  
        public Form Form { get; set; }  
    }

}
