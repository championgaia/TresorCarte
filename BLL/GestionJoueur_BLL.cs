using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumerations;

namespace BLL
{
    public interface IGestionJoueur_BLL
    {
        Task<List<Joueur>> CreateListeJoueur(List<string> oListDescriptionFichier);
    }
    public class GestionJoueur_BLL : IGestionJoueur_BLL
    {
        private readonly IGestionDonnesValide_BLL _oIGestionDonnesValide_BLL;
        public GestionJoueur_BLL(IGestionDonnesValide_BLL oIGestionDonnesValide_BLL)
        {
            _oIGestionDonnesValide_BLL = oIGestionDonnesValide_BLL;
        }
        /// <summary>
        /// Créer les joueurs
        /// </summary>
        /// <param name="oListDescriptionFichier"></param>
        /// <returns></returns>
        public async Task<List<Joueur>> CreateListeJoueur(List<string> oListDescriptionFichier)
        {
            List<Joueur> oListeJoueur = null;
            if (oListDescriptionFichier != null && oListDescriptionFichier.Count > 0)
            {
                List<string> oListInfoJoueur = oListDescriptionFichier.FindAll(o => o.StartsWith(Constants.ABREVIATION_JOUEUR));
                if (oListInfoJoueur != null && oListInfoJoueur.Count > 0)
                {
                    oListeJoueur = new List<Joueur>();
                    foreach (var oInfoJoueur in oListInfoJoueur)
                    {
                        Joueur oJoueur = await CreateUnJoueur(oInfoJoueur, oListInfoJoueur.IndexOf(oInfoJoueur));
                        if (oJoueur != null)
                        {
                            oListeJoueur.Add(oJoueur);
                        }
                    }
                }
            }
            return oListeJoueur;
        }
        /// <summary>
        /// Créer un joueur avec des infos
        /// </summary>
        /// <param name="oInfoJoueur"></param>
        /// <param name="iIndexInfoJoueur"></param>
        /// <returns></returns>
        private async Task<Joueur> CreateUnJoueur(string oInfoJoueur, int iIndexInfoJoueur)
        {
            Joueur oJoueur = null;
            if (await _oIGestionDonnesValide_BLL.DonnesValide(oInfoJoueur, DonnesType.Joueur))
            {
                string[] oArrayDonnesJoueur = oInfoJoueur.Split(Constants.SEPERATEUR);
                oJoueur = new Joueur(oArrayDonnesJoueur[1], (Orientation)Enum.Parse(typeof(Orientation), oArrayDonnesJoueur[4]), oArrayDonnesJoueur[5].ToCharArray(), iIndexInfoJoueur + 1)
                {
                    JoueurPosition = new Position(int.Parse(oArrayDonnesJoueur[2]), int.Parse(oArrayDonnesJoueur[3]))
                };
            }
            return oJoueur;
        }
    }
}
