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
    public interface IGestionDerouleJeu_BLL
    {
        Task<string> ExecuterJeu(List<string> oListDescriptionFichier);
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
            await InitiationJeu(oListDescriptionFichier);
            if (CurrentJeu != null && CurrentJeu.NombreDeplacementMax > 0)
            {
                for (int iTour = 1; iTour < CurrentJeu.NombreDeplacementMax; iTour++)
                {
                    await JouerUnTout(iTour);
                }
                return await TerminerJeu();
            }
            return null;
        }
        /// <summary>
        /// Initialiser le jeu (creation carte, les joueurs, les tours) 
        /// </summary>
        /// <param name="oListDescriptionFichier"></param>
        /// <returns></returns>
        private async Task InitiationJeu(List<string> oListDescriptionFichier)
        {
            if (oListDescriptionFichier != null && oListDescriptionFichier.Count > 0)
            {
                CurrentJeu = new Jeu
                {
                    Carte = await _oIGestionCarte_BLL.CreateCarteTresor(oListDescriptionFichier),
                    Joueurs = await _oIGestionJoueur_BLL.CreateListeJoueur(oListDescriptionFichier)
                };
                CurrentJeu.NombreDeplacementMax = CurrentJeu.Joueurs.Max(j => j.SequenceMovement.Length);
            }

        }
        /// <summary>
        /// Joueur un tour des tous les joueurs
        /// </summary>
        /// <param name="iTour"></param>
        /// <returns></returns>
        private async Task JouerUnTout(int iTour)
        {
            for (int iOrder = 1; iOrder <= CurrentJeu.Joueurs.Count; iOrder++)
            {
                Joueur oCurrentJoueur = CurrentJeu.Joueurs[iOrder - 1];
                Position oAncientPosition = oCurrentJoueur.JoueurPosition;
                if (oCurrentJoueur.SequenceMovement.Length >= iTour)
                {
                    //chercher la nouvelle prosition et prochaine orientation
                    (Position oNouvellePosition, Orientation eNextOrientation)  = await _oIDeplacerJoueur_BLL.GetNextPositionJoueur(oCurrentJoueur, iTour);
                    if (oNouvellePosition.CompareTo(oAncientPosition) == 0 && await _oIGestionCarte_BLL.GetValidePosition(CurrentJeu.Carte, oNouvellePosition))
                    {
                        //joueur a déplacé
                        //liberer le case de départ
                        CaseCarte oAncientCase = CurrentJeu.Carte.ListeCase.FirstOrDefault(c => oAncientPosition.CompareTo(c.CasePosition) == 1);
                        oAncientCase.EstOccupe = false;
                        //occuper le case d'arrive
                        CaseCarte oNouveauCase = CurrentJeu.Carte.ListeCase.FirstOrDefault(c => oNouvellePosition.CompareTo(c.CasePosition) == 1);
                        oNouveauCase.EstOccupe = true;
                        //changer position du current joueur
                        oCurrentJoueur.JoueurPosition = oNouvellePosition;
                        //recuperer trésor s'il y en a, et décompte le nombre de trésor
                        if (oNouveauCase.Type == CaseType.Tresor && oNouveauCase.NombreTresor > 0)
                        {
                            oCurrentJoueur.NombreTresorTrouve++;
                            oNouveauCase.NombreTresor--;
                        }
                    }
                    //set prochaine orientation pour le joueur
                    oCurrentJoueur.CurrentOrientation = eNextOrientation;
                }
            }
        }
        /// <summary>
        /// Termier le jeu en renvoyant le nom de fichier sortie
        /// </summary>
        /// <returns></returns>
        public async Task<string> TerminerJeu()
        {
            return await _oIGestionFichier_BLL.Ecriture(Guid.NewGuid().ToString() + Constants.EXTENSION_TEXTE, CurrentJeu);
        }
    }
}
