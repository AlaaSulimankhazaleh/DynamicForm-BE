﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFormBuilder.Application.DTO
{
    public  record RegisterResponseModel
    {
        public long  Id { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; } 
    }
}
