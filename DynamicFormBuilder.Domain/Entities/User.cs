using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Domain.Entities
{
    public  class User
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string CreatedBy { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public ICollection<Form> Forms { get; set; } = new List<Form>();

    }
}
