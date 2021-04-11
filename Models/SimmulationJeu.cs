using Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Models
{
    public class SimmulationJeu
    {
        [DisplayName("Fichier entré :")]
        [FileExtensionValid(ErrorMessage = "Fichier non valid")]
        public IFormFile MyFileUpload { get; set; }
        [DisplayName("Fichier généré :")]
        public string FichierResult { get; set; }
    }
}
