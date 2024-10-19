using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Domain.Entities
{
    public class Form
    {
        public long Id { get; set; }
        public string Name { get; set; }  
        public string Key { get; set; }  
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ? ModifiedBy { get; set; }
        public long UserId { get; set; }  
        public User User { get; set; }  
        public ICollection<Control> Controls { get; set; } = new List<Control>();
    }

}
