using Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Models
{
    public class GameSimmulation
    {
        [DisplayName("Enty File :")]
        [FileExtensionValid(ErrorMessage = "File not valid")]
        public IFormFile MyFileUpload { get; set; }
        [DisplayName("File out :")]
        public string FileResult { get; set; }
    }
}
