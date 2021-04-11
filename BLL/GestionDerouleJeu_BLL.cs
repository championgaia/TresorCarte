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
        Task ExecuterJeu();
        Task JouerUnTout(int iTour);
        Task TerminerJeu();
    }
    public class GestionDerouleJeu_BLL : IGestionDerouleJeu_BLL
    {
        private readonly IGestionCarte_BLL _oIGestionCarte_BLL;
        private readonly IGestionJoueur_BLL _oIGestionJoueur_BLL;
        private readonly IDeplacerJoueur_BLL _oIDeplacerJoueur_BLL;
        private Jeu CurrentJeu { get; set; }
        public GestionDerouleJeu_BLL(IGestionCarte_BLL oIGestionCarte_BLL, IGestionJoueur_BLL oIGestionJoueur_BLL, IDeplacerJoueur_BLL oIDeplacerJoueur_BLL)
        {
            _oIGestionCarte_BLL = oIGestionCarte_BLL;
            _oIGestionJoueur_BLL = oIGestionJoueur_BLL;
            _oIDeplacerJoueur_BLL = oIDeplacerJoueur_BLL;
        }

        public async Task ExecuterJeu()
        {
            if (CurrentJeu != null)
            {
                for (int iTour = 1; iTour <= CurrentJeu.NombreDeplacementMax; iTour++)
                {
                    await JouerUnTout(iTour);
                }
            }
        }

        public async Task InitiationJeu(List<string> oListDescriptionFichier)
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

        public async Task JouerUnTout(int iTour)
        {
            for (int iOrder = 1; iOrder <= CurrentJeu.Joueurs.Count; iOrder++)
            {
                Joueur oCurentJoueur = CurrentJeu.Joueurs[iOrder];
                Position oAncientPosition = oCurentJoueur.JoueurPosition;
                Position oNouveauPosition = await _oIDeplacerJoueur_BLL.Deplacer(oCurentJoueur, iTour);
                if (oNouveauPosition.CompareTo(oAncientPosition) == 0)
                {
                    //joueur a déplacé
                    //update case dans la carte non occupé
                    CaseCarte oAncientCase = CurrentJeu.Carte.ListeCase.FirstOrDefault(c => oAncientPosition.CompareTo(c) == 1);
                    CaseCarte oNouveauCase = CurrentJeu.Carte.ListeCase.FirstOrDefault(c => oNouveauPosition.CompareTo(c) == 1);
                    oAncientCase.EstOccupe = false;
                    oCurentJoueur.JoueurPosition = oNouveauPosition;
                    oNouveauCase.EstOccupe = true;
                    if (oNouveauCase.Type == CaseType.Tresor && oNouveauCase.NombreTresor > 0)
                    {
                        oCurentJoueur.NombreTresorTrouve ++;
                        oNouveauCase.NombreTresor --;
                    }
                }
            }
        }

        public async Task TerminerJeu()
        {
            throw new NotImplementedException();
        }
    }
}
