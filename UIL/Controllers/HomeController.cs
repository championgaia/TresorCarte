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
        private readonly IFileManage_BLL _oIFileManage_BLL;
        private readonly IGameManage_BLL _oIGameManage_BLL;
        public HomeController(ILogger<HomeController> logger, IFileManage_BLL oIFileManage_BLL, IGameManage_BLL oIGameManage_BLL)
        {
            _logger = logger;
            _oIFileManage_BLL = oIFileManage_BLL;
            _oIGameManage_BLL = oIGameManage_BLL;
        }

        public async Task<IActionResult> Index()
        {
            GameSimmulation oGameSimmulation = new GameSimmulation();
            return View(oGameSimmulation);
        }

        [HttpPost]
        public async Task<IActionResult> Post(GameSimmulation oGameSimmulation)
        {
            if (ModelState.IsValid)
            {
                string sNameFileCopie = string.Empty;
                if (oGameSimmulation.MyFileUpload != null && !string.IsNullOrEmpty(oGameSimmulation.MyFileUpload.FileName))
                {
                    sNameFileCopie = await _oIFileManage_BLL.SaveFileUploadAsync(oGameSimmulation.MyFileUpload);
                }
                (bool bIsValide, string sMessageError, List<string> oListDescriptionFile) = await _oIFileManage_BLL.DataFileValideAsync(sNameFileCopie);
                if (bIsValide)
                {
                    string sNameFileResult = await _oIGameManage_BLL.RunGameAsync(oListDescriptionFile);
                    return RedirectToAction("Details", "Home", new { @sNameFile = sNameFileResult });
                }
                else
                {
                    ViewBag.ErrorMessage = sMessageError;
                    return View("Index", oGameSimmulation);
                }
            }
            else
            {
                return View("Index", oGameSimmulation);
            }
        }

        [HttpGet("[controller]/[action]/{sNameFile}")]
        public async Task<IActionResult> Details(string sNameFile)
        {
            GameSimmulation oGameSimmulation = new GameSimmulation
            {
                FileResult = sNameFile
            };
            return View(oGameSimmulation);
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content(Constants.FILE_NAME_EMPTY);
            MemoryStream oMemoryStream = await _oIFileManage_BLL.CreateStreamDownloadAsync(filename);
            if (oMemoryStream == null)
                return Content(Constants.FILE_NOT_FOUND);
            return File(oMemoryStream, Constants.TEXT_XML, filename);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
