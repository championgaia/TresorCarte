using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using UIL.Models;

namespace UIL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGestionFichier_BLL _oIGestionFichier_BLL;
        private readonly IGestionDerouleJeu_BLL _oIGestionDerouleJeu_BLL;
        public HomeController(ILogger<HomeController> logger, IGestionFichier_BLL oIGestionFichier_BLL, IGestionDerouleJeu_BLL oIGestionDerouleJeu_BLL)
        {
            _logger = logger;
            _oIGestionFichier_BLL = oIGestionFichier_BLL;
            _oIGestionDerouleJeu_BLL = oIGestionDerouleJeu_BLL;
        }

        public async Task<IActionResult> Index()
        {
            SimmulationJeu oSimmulationJeu = new SimmulationJeu();
            return View(oSimmulationJeu);
        }
        [HttpPost]
        public async Task<IActionResult> Post(SimmulationJeu oSimmulationJeu)
        {
            if (ModelState.IsValid)
            {
                string sNameFileCopie = string.Empty;
                if (oSimmulationJeu.MyFileUpload != null && !string.IsNullOrEmpty(oSimmulationJeu.MyFileUpload.FileName))
                {
                    sNameFileCopie = await _oIGestionFichier_BLL.SaveFileUpload(oSimmulationJeu.MyFileUpload);
                }
                (bool bEstValide, string sMessageErreur, List<string> oListeDescriptionFichier) = await _oIGestionFichier_BLL.FichierDonnesValide(sNameFileCopie);
                if (bEstValide)
                {
                    string sNameFileResult = await _oIGestionDerouleJeu_BLL.ExecuterJeu(oListeDescriptionFichier);
                    return RedirectToAction("Details", "Home", new { @sNameFile = sNameFileResult });
                }
                else
                {
                    ViewBag.ErrorMessage = sMessageErreur;
                    return View("Index", oSimmulationJeu);
                }
            }
            else
            {
                return View("Index", oSimmulationJeu);
            }
        }
        [HttpGet("[controller]/[action]/{sNameFile}")]
        public async Task<IActionResult> Details(string sNameFile)
        {
            SimmulationJeu oSimmulationJeu = new SimmulationJeu
            {
                FichierResult = sNameFile
            };
            return View(oSimmulationJeu);
        }
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("nom du fichier est vide");
            MemoryStream oMemoryStream = await _oIGestionFichier_BLL.CreateStreamDownload(filename);
            if (oMemoryStream == null)
                return Content("fichier non trouvé");
            return File(oMemoryStream, Constants.TEXT_XML, filename);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
