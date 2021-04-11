using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IDeplacerJoueur_BLL
    {
        Task<Position> Deplacer(Joueur oCurrentJoueur, int iTour);
    }
    public class DeplacerJoueur_BLL : IDeplacerJoueur_BLL
    { 
        public async Task<Position> Deplacer(Joueur oCurrentJoueur, int iTour)
        {
            Position oPosition = oCurrentJoueur.JoueurPosition;
            if (await JoueurPeutEtreDeplacer(oCurrentJoueur, iTour))
            {

            }
            return oPosition;
        }

        private async Task<bool> JoueurPeutEtreDeplacer(Joueur oCurrentJoueur, int iTour)
        {
            throw new NotImplementedException();
        }
    }
}
