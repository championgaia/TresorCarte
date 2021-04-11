using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumerations;

namespace BLL
{
    public interface IGestionCarte_BLL
    {
        Task<TresorCarte> CreateCarteTresor(List<string> oListDescriptionFichier);
    }
    public class GestionCarte_BLL : IGestionCarte_BLL
    {
        private TresorCarte TresorCarte { get; set; } = null;
        private readonly IGestionDonnesValide_BLL _oIGestionDonnesValide_BLL;
        public GestionCarte_BLL(IGestionDonnesValide_BLL oIGestionDonnesValide_BLL)
        {
            _oIGestionDonnesValide_BLL = oIGestionDonnesValide_BLL;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oListDescriptionFichier"></param>
        /// <returns></returns>
        public async Task<TresorCarte> CreateCarteTresor(List<string> oListDescriptionFichier)
        {
            TresorCarte = null;
            if (oListDescriptionFichier != null && oListDescriptionFichier.Count > 0)
            {
                TresorCarte = await InitialiserCarte(oListDescriptionFichier.FirstOrDefault(o => o.StartsWith(Constants.ABREVIATION_CARTE)));
                TresorCarte.ListeCase = await InitialiserCases();
                await InitialiserCaseMontagne(oListDescriptionFichier.FindAll(o => o.StartsWith(Constants.ABREVIATION_MONTAGNE)));
                await InitialiserCaseTresor(oListDescriptionFichier.FindAll(o => o.StartsWith(Constants.ABREVIATION_TRESOR)));
                await InitialiserPositionJoueurs(oListDescriptionFichier.FindAll(o => o.StartsWith(Constants.ABREVIATION_JOUEUR)));
            }
            return TresorCarte;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sInfoCarte"></param>
        /// <returns></returns>
        private async Task<TresorCarte> InitialiserCarte(string sInfoCarte)
        {
            TresorCarte = null;
            if (await _oIGestionDonnesValide_BLL.DonnesValide(sInfoCarte, DonnesType.Carte))
            {
                string[] oCarteDonnes = sInfoCarte.Split(Constants.SEPERATEUR);
                TresorCarte = new TresorCarte(int.Parse(oCarteDonnes[1]), int.Parse(oCarteDonnes[2]));
            }
            return TresorCarte;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oListeInfoCaseTresor"></param>
        /// <returns></returns>
        private async Task InitialiserCaseTresor(List<string> oListeInfoCaseTresor)
        {
            if (TresorCarte != null && TresorCarte.ListeCase != null && TresorCarte.ListeCase.Count > 0 && oListeInfoCaseTresor != null && oListeInfoCaseTresor.Count > 0)
            {
                foreach (var sInfoCaseTresor in oListeInfoCaseTresor)
                {
                    if (await _oIGestionDonnesValide_BLL.DonnesValide(sInfoCaseTresor, DonnesType.Tresor))
                    {
                        string[] oCaseTresorDonnes = sInfoCaseTresor.Split(Constants.SEPERATEUR);
                        Position oTresorPosition = new Position(int.Parse(oCaseTresorDonnes[1]), int.Parse(oCaseTresorDonnes[2]));
                        CaseCarte oCaseCarte = TresorCarte.ListeCase.FirstOrDefault(c => c.CasePosition.CompareTo(oTresorPosition) == 1);
                        if (oCaseCarte != null)
                        {
                            oCaseCarte.Type = CaseType.Tresor;
                            oCaseCarte.NombreTresor = int.Parse(oCaseTresorDonnes[3]);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oListeCaseMontagne"></param>
        /// <returns></returns>
        private async Task InitialiserCaseMontagne(List<string> oListeCaseMontagne)
        {
            if (TresorCarte != null && TresorCarte.ListeCase != null && TresorCarte.ListeCase.Count > 0 && oListeCaseMontagne != null && oListeCaseMontagne.Count > 0)
            {
                foreach (var sInfoCaseMontagne in oListeCaseMontagne)
                {
                    if (await _oIGestionDonnesValide_BLL.DonnesValide(sInfoCaseMontagne, DonnesType.Montagne))
                    {
                        string[] oCaseMontagneDonnes = sInfoCaseMontagne.Split(Constants.SEPERATEUR);
                        Position oMontagnePosition = new Position(int.Parse(oCaseMontagneDonnes[1]), int.Parse(oCaseMontagneDonnes[2]));
                        CaseCarte oCaseCarte = TresorCarte.ListeCase.FirstOrDefault(c => c.CasePosition.CompareTo(oMontagnePosition) == 1);
                        if (oCaseCarte != null)
                        {
                            oCaseCarte.Type = CaseType.Montagne;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<List<CaseCarte>> InitialiserCases()
        {
            if (TresorCarte != null)
            {
                TresorCarte.ListeCase = new List<CaseCarte>();
                for (var ligne = 0; ligne < TresorCarte.NombreCaseLargeur; ligne++)
                {
                    for (var colonne = 0; colonne < TresorCarte.NombreCaseHauteur; colonne++)
                    {
                        var oCaseMap = new CaseCarte(new Position(ligne, colonne), CaseType.Neutre, 0, false);
                        TresorCarte.ListeCase.Add(oCaseMap);
                    }
                }
            }
            return TresorCarte?.ListeCase;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oListInfoJoueur"></param>
        /// <returns></returns>
        private async Task InitialiserPositionJoueurs(List<string> oListInfoJoueur)
        {
            if (oListInfoJoueur != null && oListInfoJoueur.Count > 0)
            {
                foreach (var oInfoJoueur in oListInfoJoueur)
                {
                    await SetCaseOccuperParJoueur(oInfoJoueur);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oInfoJoueur"></param>
        /// <returns></returns>
        private async Task SetCaseOccuperParJoueur(string oInfoJoueur)
        {
            if (TresorCarte != null && await _oIGestionDonnesValide_BLL.DonnesValide(oInfoJoueur, DonnesType.Joueur))
            {
                string[] oArrayDonnesJoueur = oInfoJoueur.Split(Constants.SEPERATEUR);
                CaseCarte oCaseOccupeParJoueur = TresorCarte.ListeCase
                                                .FirstOrDefault(c => c.CasePosition.CompareTo(new Position(int.Parse(oArrayDonnesJoueur[2]), int.Parse(oArrayDonnesJoueur[3]))) == 1);
                if (oCaseOccupeParJoueur != null)
                {
                    oCaseOccupeParJoueur.EstOccupe = true;
                }
            }
        }
    }
}
