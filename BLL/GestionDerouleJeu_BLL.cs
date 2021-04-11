using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumerations;

namespace BLL
{
    public interface IGestionDerouleJeu_BLL
    {
        Task InitiationJeu(List<string> oListDescriptionFichier);
        Task<string> ExecuterJeu(List<string> oListDescriptionFichier);
        Task JouerUnTout(int iTour);
        Task<string> TerminerJeu();
    }
    public class GestionDerouleJeu_BLL : IGestionDerouleJeu_BLL
    {
        private readonly IGestionCarte_BLL _oIGestionCarte_BLL;
        private readonly IGestionJoueur_BLL _oIGestionJoueur_BLL;
        private readonly IDeplacerJoueur_BLL _oIDeplacerJoueur_BLL;
        private readonly IGestionFichier_BLL _oIGestionFichier_BLL;
        private Jeu CurrentJeu { get; set; }
        public GestionDerouleJeu_BLL(IGestionCarte_BLL oIGestionCarte_BLL, IGestionJoueur_BLL oIGestionJoueur_BLL, IDeplacerJoueur_BLL oIDeplacerJoueur_BLL, IGestionFichier_BLL oIGestionFichier_BLL)
        {
            _oIGestionCarte_BLL = oIGestionCarte_BLL;
            _oIGestionJoueur_BLL = oIGestionJoueur_BLL;
            _oIDeplacerJoueur_BLL = oIDeplacerJoueur_BLL;
            _oIGestionFichier_BLL = oIGestionFichier_BLL;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oListDescriptionFichier"></param>
        /// <returns></returns>
        public async Task<string> ExecuterJeu(List<string> oListDescriptionFichier)
        {
            await InitiationJeu( oListDescriptionFichier);
            if (CurrentJeu != null)
            {
                for (int iTour = 1; iTour <= CurrentJeu.NombreDeplacementMax; iTour++)
                {
                    await JouerUnTout(iTour);
                }
                return await TerminerJeu();
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oListDescriptionFichier"></param>
        /// <returns></returns>
        public async Task InitiationJeu(List<string> oListDescriptionFichier)
        {
            if (oListDescriptionFichier != null && oListDescriptionFichier.Count > 0)
            {
                CurrentJeu = new Jeu
                {
                    Carte = await _oIGestionCarte_BLL.CreateCarteTresor(oListDescriptionFichier),
                    Joueurs = await _oIGestionJoueur_BLL.CreateListeJoueur(oListDescriptionFichier)
                };
                CurrentJeu.NombreDeplacementMax = CurrentJeu.Joueurs.Max(j => j.SequenceMovement.Length) - 1;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTour"></param>
        /// <returns></returns>
        public async Task JouerUnTout(int iTour)
        {
            for (int iOrder = 1; iOrder <= CurrentJeu.Joueurs.Count; iOrder++)
            {
                Joueur oCurentJoueur = CurrentJeu.Joueurs[iOrder - 1];
                Position oAncientPosition = oCurentJoueur.JoueurPosition;
                if (oCurentJoueur.SequenceMovement.Length >= iTour)
                {
                    Position oNouveauPosition = await _oIDeplacerJoueur_BLL.GetNextPositionJoueur(oCurentJoueur, iTour);
                    if (await GetValidePosition(CurrentJeu.Carte, oNouveauPosition) && oNouveauPosition.CompareTo(oAncientPosition) == 0)
                    {
                        //joueur a déplacé
                        //liberer le case de départ
                        CaseCarte oAncientCase = CurrentJeu.Carte.ListeCase.FirstOrDefault(c => oAncientPosition.CompareTo(c.CasePosition) == 1);
                        oAncientCase.EstOccupe = false;
                        //occuper le case d'arrive
                        CaseCarte oNouveauCase = CurrentJeu.Carte.ListeCase.FirstOrDefault(c => oNouveauPosition.CompareTo(c.CasePosition) == 1);
                        oNouveauCase.EstOccupe = true;
                        //changer position du current joueur
                        oCurentJoueur.JoueurPosition = oNouveauPosition;
                        //recuperer trésor s'il y en a, et décompte le nombre de trésor
                        if (oNouveauCase.Type == CaseType.Tresor && oNouveauCase.NombreTresor > 0)
                        {
                            oCurentJoueur.NombreTresorTrouve++;
                            oNouveauCase.NombreTresor--;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oCarte"></param>
        /// <param name="oProchainPrevuPosition"></param>
        /// <returns></returns>
        private async Task<bool> GetValidePosition(TresorCarte oCarte, Position oProchainPrevuPosition)
        {
            if (oProchainPrevuPosition.AxeHorizontal >= 0 && oProchainPrevuPosition.AxeHorizontal < oCarte.NombreCaseLargeur &&
                oProchainPrevuPosition.AxeVertical >= 0 && oProchainPrevuPosition.AxeVertical < oCarte.NombreCaseHauteur)
            {
                CaseCarte oCaseCarte = oCarte.ListeCase.FirstOrDefault(c => c.CasePosition.CompareTo(oProchainPrevuPosition) == 1);
                return !oCaseCarte.EstOccupe && oCaseCarte.Type != CaseType.Montagne;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> TerminerJeu()
        {
            return await _oIGestionFichier_BLL.Ecriture(Guid.NewGuid().ToString() + ".txt", CurrentJeu);
        }
    }
}
