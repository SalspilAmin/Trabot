using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Tradify.Core.Behaviors
{
    public class CustomValidationExeption : Exception
    {
        public List<string> Errors { get; set; }
        public CustomValidationExeption(List<string> errors) :base("Validation Failed")
        {
        Errors = errors;
        }
    }
}
