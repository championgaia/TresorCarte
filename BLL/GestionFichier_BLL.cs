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
    public interface IGestionFichier_BLL
    {
        Task<List<string>> Lecture(string sFilePath);
        Task<string> Ecriture(string sFileName, Jeu oCurrentJeu);
        Task<string> SaveFileUpload(IFormFile oIFormFile);
        Task<(bool, string, List<string>)> FichierDonnesValide(string sFileName, string sFullPathFile = null);
        Task<MemoryStream> CreateStreamDownload(string filename);
    }
    public class GestionFichier_BLL : IGestionFichier_BLL
    {
        /// <summary>
        /// Save un fichier upload dans un dossier et retourner le nom de fichier sauvegardé
        /// </summary>
        /// <param name="oIFormFile"></param>
        /// <returns></returns>
        public async Task<string> SaveFileUpload(IFormFile oIFormFile)
        {
            string sFileNameGuid = Guid.NewGuid().ToString() + Path.GetExtension(oIFormFile.FileName);
            DirectoryInfo oDirectoryInfo = new DirectoryInfo(Constants.DOSSIER_TEMPS);
            oDirectoryInfo.Create();
            string sFullPathFile = Path.Combine(oDirectoryInfo.FullName, sFileNameGuid);
            using (var stream = new FileStream(sFullPathFile, FileMode.Create))
            {
                await oIFormFile.CopyToAsync(stream);
            }
            return sFileNameGuid;
        }
        /// <summary>
        /// Lecture un fichier et retourner son contenu
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
        public async Task<List<string>> Lecture(string sFilePath)
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
                        if (!sLigne.Contains(Constants.ABREVIATION_COMMENTAIRE))
                        {
                            oListDescriptionFichier.Add(sLigne);
                        }
                    }
                }
            }
            return oListDescriptionFichier;
        }
        /// <summary>
        /// Ecrit le fichier de sortie
        /// </summary>
        /// <param name="sFileName"></param>
        /// <param name="oCurrentJeu"></param>
        /// <returns></returns>
        public async Task<string> Ecriture(string sFileName, Jeu oCurrentJeu)
        {
            if (!string.IsNullOrEmpty(sFileName) && oCurrentJeu != null)
            {
                FileInfo oFileInfo = new FileInfo(Path.Combine(Constants.DOSSIER_TEMPS, sFileName));
                if (oFileInfo.Exists)
                {
                    oFileInfo.Delete();
                }
                List<string> oListeInfoSortie = new List<string>
                {
                    Constants.COMMENTAIRE_CARTE,
                    oCurrentJeu.Carte.ToString(),
                    Constants.COMMENTAIRE_MONTAGNE
                };
                foreach (var oCaseMontage in oCurrentJeu.Carte.ListeCase.FindAll(c => c.Type == CaseType.Montagne))
                {
                    oListeInfoSortie.Add(oCaseMontage.ToString());
                }
                oListeInfoSortie.Add(Constants.COMMENTAIRE_TRESOR);
                foreach (var oCaseTresor in oCurrentJeu.Carte.ListeCase.FindAll(c => c.Type == CaseType.Tresor))
                {
                    oListeInfoSortie.Add(oCaseTresor.ToString());
                }
                oListeInfoSortie.Add(Constants.COMMENTAIRE_JOUEUR);
                foreach (var oJoueur in oCurrentJeu.Joueurs)
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
        public async Task<(bool, string, List<string>)> FichierDonnesValide(string sFileName, string sFullPathFile = null)
        {
            bool bEstValide = true;
            string sMessageErreur= string.Empty;
            List<string> oListeDonnes = await Lecture(string.IsNullOrEmpty(sFullPathFile) ? Path.Combine(Constants.DOSSIER_TEMPS, sFileName) : sFullPathFile);
            if (oListeDonnes != null && oListeDonnes.Count > 0)
            {
                List<string> oListeDonnesCarte = oListeDonnes.FindAll(o => o.StartsWith(Constants.ABREVIATION_CARTE));
                if (oListeDonnesCarte?.Count != 1)
                {
                    bEstValide = false;
                    sMessageErreur += Constants.DONNES_CARTE_INVALIDE + Constants.SAUT_LIGNE_HTML;
                }
                List<string> oListeDonnesTresor = oListeDonnes.FindAll(o => o.StartsWith(Constants.ABREVIATION_TRESOR));
                if (oListeDonnesTresor?.Count == 0)
                {
                    bEstValide = false;
                    sMessageErreur += Constants.DONNES_TRESOR_INVALIDE + Constants.SAUT_LIGNE_HTML;
                }
                List<string> oListeDonnesJoueur = oListeDonnes.FindAll(o => o.StartsWith(Constants.ABREVIATION_JOUEUR));
                if (oListeDonnesJoueur?.Count == 0)
                {
                    bEstValide = false;
                    sMessageErreur += Constants.DONNES_JOUEUR_INVALIDE;
                }
            }
            else
            {
                bEstValide = false;
                sMessageErreur += Constants.DONNES_CARTE_INVALIDE + Constants.SAUT_LIGNE_HTML;
                sMessageErreur += Constants.DONNES_TRESOR_INVALIDE + Constants.SAUT_LIGNE_HTML;
                sMessageErreur += Constants.DONNES_JOUEUR_INVALIDE;
            }
            return (bEstValide, sMessageErreur, oListeDonnes);
        }
        /// <summary>
        /// lecture le fichier en memoire
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        public async Task<MemoryStream> CreateStreamDownload(string sFileName)
        {;
            FileInfo oFileInfo = new FileInfo(Path.Combine(Constants.DOSSIER_TEMPS, sFileName));
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
