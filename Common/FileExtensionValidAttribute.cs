using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public class FileExtensionValidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is IFormFile file)
            {
                string sExtension = Path.GetExtension(file.FileName);
                return sExtension.ToLower() == ".txt";
            }
            return true;
        }
    }
}
