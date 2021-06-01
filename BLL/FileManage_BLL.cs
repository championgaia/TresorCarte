using Common;
using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumerations;

namespace BLL
{
    public interface IFileManage_BLL
    {
        Task<List<string>> ReadAsync(string sFilePath);
        Task<string> ExportAsync(string sFileName, Game oCurrentJeu);
        Task<string> SaveFileUploadAsync(IFormFile oIFormFile);
        Task<(bool, string, List<string>)> DataFileValideAsync(string sFileName, string sFullPathFile = null);
        Task<MemoryStream> CreateStreamDownloadAsync(string filename);
    }
    public class FileManage_BLL : IFileManage_BLL
    {
        /// <summary>
        /// Save a file upload in a folder and return name of file
        /// </summary>
        /// <param name="oIFormFile"></param>
        /// <returns></returns>
        public async Task<string> SaveFileUploadAsync(IFormFile oIFormFile)
        {
            string sFileNameGuid = Guid.NewGuid().ToString() + Path.GetExtension(oIFormFile.FileName);
            DirectoryInfo oDirectoryInfo = new DirectoryInfo(Constants.FOLDER_TEMPS);
            oDirectoryInfo.Create();
            string sFullPathFile = Path.Combine(oDirectoryInfo.FullName, sFileNameGuid);
            using (var stream = new FileStream(sFullPathFile, FileMode.Create))
            {
                await oIFormFile.CopyToAsync(stream);
            }
            return sFileNameGuid;
        }
        /// <summary>
        /// Read the content of file
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
        public async Task<List<string>> ReadAsync(string sFilePath)
        {
            List<string> oListDescriptionFichier = null;
            if (!string.IsNullOrEmpty(sFilePath))
            {
                FileInfo oFileInfo = new FileInfo(sFilePath);
                if (oFileInfo.Exists)
                {
                    oListDescriptionFichier = new List<string>();
                    using StreamReader sr = oFileInfo.OpenText();
                    string sLigne = "";
                    while ((sLigne = await sr.ReadLineAsync()) != null)
                    {
                        if (!sLigne.Contains(Constants.ABREVIATION_COMMENT))
                        {
                            oListDescriptionFichier.Add(sLigne);
                        }
                    }
                }
            }
            return oListDescriptionFichier;
        }
        /// <summary>
        /// export file
        /// </summary>
        /// <param name="sFileName"></param>
        /// <param name="oCurrentJeu"></param>
        /// <returns></returns>
        public async Task<string> ExportAsync(string sFileName, Game oCurrentJeu)
        {
            if (!string.IsNullOrEmpty(sFileName) && oCurrentJeu != null)
            {
                FileInfo oFileInfo = new FileInfo(Path.Combine(Constants.FOLDER_TEMPS, sFileName));
                if (oFileInfo.Exists)
                {
                    oFileInfo.Delete();
                }
                List<string> oListeInfoSortie = new List<string>
                {
                    Constants.COMMENT_MAP,
                    oCurrentJeu.Map.ToString(),
                    Constants.COMMENT_MOUNTAIN
                };
                foreach (var oCaseMontage in oCurrentJeu.Map.ListeCase.FindAll(c => c.Type == CaseType.Mountain))
                {
                    oListeInfoSortie.Add(oCaseMontage.ToString());
                }
                oListeInfoSortie.Add(Constants.COMMENT_TREASURE);
                foreach (var oCaseTresor in oCurrentJeu.Map.ListeCase.FindAll(c => c.Type == CaseType.Treasure))
                {
                    oListeInfoSortie.Add(oCaseTresor.ToString());
                }
                oListeInfoSortie.Add(Constants.COMMENT_AVENTURER);
                foreach (var oJoueur in oCurrentJeu.Players)
                {
                    oListeInfoSortie.Add(oJoueur.ToString());
                }
                using (StreamWriter sw = oFileInfo.CreateText())
                {
                    foreach (var oLigneInfo in oListeInfoSortie)
                    {
                        await sw.WriteLineAsync(oLigneInfo);
                    }
                }
                return oFileInfo.Name;
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        public async Task<(bool, string, List<string>)> DataFileValideAsync(string sFileName, string sFullPathFile = null)
        {
            bool bEstValide = true;
            string sMessageErreur= string.Empty;
            List<string> oListeDonnes = await ReadAsync(string.IsNullOrEmpty(sFullPathFile) ? Path.Combine(Constants.FOLDER_TEMPS, sFileName) : sFullPathFile);
            if (oListeDonnes != null && oListeDonnes.Count > 0)
            {
                List<string> oListeDonnesCarte = oListeDonnes.FindAll(o => o.StartsWith(Constants.ABREVIATION_MAP));
                if (oListeDonnesCarte?.Count != 1)
                {
                    bEstValide = false;
                    sMessageErreur += Constants.DATA_MAP_INVALIDE + Constants.LINE_BREAK_HTML;
                }
                List<string> oListeDonnesTresor = oListeDonnes.FindAll(o => o.StartsWith(Constants.ABREVIATION_TREASURE));
                if (oListeDonnesTresor?.Count == 0)
                {
                    bEstValide = false;
                    sMessageErreur += Constants.DATA_TREASURE_INVALIDE + Constants.LINE_BREAK_HTML;
                }
                List<string> oListeDonnesJoueur = oListeDonnes.FindAll(o => o.StartsWith(Constants.ABREVIATION_AVENTURER));
                if (oListeDonnesJoueur?.Count == 0)
                {
                    bEstValide = false;
                    sMessageErreur += Constants.DATA_AVENTURER_INVALIDE;
                }
            }
            else
            {
                bEstValide = false;
                sMessageErreur += Constants.DATA_MAP_INVALIDE + Constants.LINE_BREAK_HTML;
                sMessageErreur += Constants.DATA_TREASURE_INVALIDE + Constants.LINE_BREAK_HTML;
                sMessageErreur += Constants.DATA_AVENTURER_INVALIDE;
            }
            return (bEstValide, sMessageErreur, oListeDonnes);
        }
        /// <summary>
        /// read a file in memory
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        public async Task<MemoryStream> CreateStreamDownloadAsync(string sFileName)
        {;
            FileInfo oFileInfo = new FileInfo(Path.Combine(Constants.FOLDER_TEMPS, sFileName));
            if (!oFileInfo.Exists)
            {
                return null;
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(oFileInfo.FullName, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return memory;
        }
    }
}
