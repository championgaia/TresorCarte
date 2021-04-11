using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IGestionCarte_BLL
    {
        Task<TresorCarte> CreateCarteTresor(List<string> oListDescriptionFichier);
    }
    public class GestionCarte_BLL : IGestionCarte_BLL
    {
        private TresorCarte TresorCarte { get; set; } = null;
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

        private async Task<TresorCarte> InitialiserCarte(string sInfoCarte)
        {
            TresorCarte = null;
            if (!string.IsNullOrEmpty(sInfoCarte) && sInfoCarte.Contains(Constants.SEPERATEUR))
            {
                string[] oCarteDonnes = sInfoCarte.Split('-');
                if (oCarteDonnes.Length >= 3)
                {
                    TresorCarte = new TresorCarte(int.Parse(oCarteDonnes[1]), int.Parse(oCarteDonnes[2]));
                }
            }
            return TresorCarte;
        }

        private async Task InitialiserCaseTresor(List<string> oListeInfoCaseTresor)
        {
            if (TresorCarte != null && TresorCarte.ListeCase != null && TresorCarte.ListeCase.Count > 0 && oListeInfoCaseTresor != null && oListeInfoCaseTresor.Count > 0)
            {
                foreach (var sInfoCaseTresor in oListeInfoCaseTresor)
                {
                    if (!string.IsNullOrEmpty(sInfoCaseTresor) && sInfoCaseTresor.Contains(Constants.SEPERATEUR))
                    {
                        string[] oCaseTresorDonnes = sInfoCaseTresor.Split(Constants.SEPERATEUR);
                        if (oCaseTresorDonnes.Length >= 4 && int.TryParse(oCaseTresorDonnes[1], out int iAxeHorizontal)
                            && int.TryParse(oCaseTresorDonnes[2], out int iAxeVertical) && int.TryParse(oCaseTresorDonnes[3], out int iNombreTresor))
                        {
                            CaseCarte oCaseCarte = TresorCarte.ListeCase.FirstOrDefault(c => c.CasePosition.AxeHorizontal == iAxeHorizontal && c.CasePosition.AxeVertical == iAxeVertical);
                            if (oCaseCarte != null)
                            {
                                oCaseCarte.Type = Enumerations.CaseType.Tresor;
                                oCaseCarte.NombreTresor = iNombreTresor;
                            }
                        }
                    }
                }
            }
        }

        private async Task InitialiserCaseMontagne(List<string> oListeCaseMontagne)
        {
            if (TresorCarte != null && TresorCarte.ListeCase != null && TresorCarte.ListeCase.Count > 0 && oListeCaseMontagne != null && oListeCaseMontagne.Count > 0)
            {
                foreach (var sInfoCaseMontagne in oListeCaseMontagne)
                {
                    if (!string.IsNullOrEmpty(sInfoCaseMontagne) && sInfoCaseMontagne.Contains(Constants.SEPERATEUR))
                    {
                        string[] oCaseMontagneDonnes = sInfoCaseMontagne.Split('-');
                        if (oCaseMontagneDonnes.Length >= 4 && int.TryParse(oCaseMontagneDonnes[1], out int iAxeHorizontal)
                            && int.TryParse(oCaseMontagneDonnes[2], out int iAxeVertical) && int.TryParse(oCaseMontagneDonnes[3], out int iNombreTresor))
                        {
                            CaseCarte oCaseCarte = TresorCarte.ListeCase.FirstOrDefault(c => c.CasePosition.AxeHorizontal == iAxeHorizontal && c.CasePosition.AxeVertical == iAxeVertical);
                            if (oCaseCarte != null)
                            {
                                oCaseCarte.Type = Enumerations.CaseType.Montagne;
                            }
                        }
                    }
                }
            }
        }
        private async Task<List<CaseCarte>> InitialiserCases()
        {
            if (TresorCarte != null)
            {
                TresorCarte.ListeCase = new List<CaseCarte>();
                for (var ligne = 0; ligne < TresorCarte.NombreCaseLargeur; ligne++)
                {
                    for (var colonne = 0; colonne < TresorCarte.NombreCaseHauteur; colonne++)
                    {
                        var oCaseMap = new CaseCarte(new Position(ligne, colonne), Enumerations.CaseType.Neutre, 0);
                        TresorCarte.ListeCase.Add(oCaseMap);
                    }
                }
            }
            return TresorCarte?.ListeCase;
        }

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

        private async Task SetCaseOccuperParJoueur(string oInfoJoueur)
        {
            if (TresorCarte != null && !string.IsNullOrEmpty(oInfoJoueur) && oInfoJoueur.Contains(Constants.SEPERATEUR))
            {
                string[] oArrayDonnesJoueur = oInfoJoueur.Split(Constants.SEPERATEUR);
                if (oArrayDonnesJoueur != null && oArrayDonnesJoueur.Length >= 6 && int.TryParse(oArrayDonnesJoueur[2], out int iAxeHorizotal)
                    && int.TryParse(oArrayDonnesJoueur[3], out int iAxeVertical))
                {
                    CaseCarte oCaseOccupeParJoueur = TresorCarte.ListeCase.FirstOrDefault(c => c.CasePosition.CompareTo(new Position(iAxeHorizotal, iAxeVertical)) == 1);
                    if (oCaseOccupeParJoueur != null)
                    {
                        oCaseOccupeParJoueur.EstOccupe = true;
                    }
                }
            }
        }
    }
}
