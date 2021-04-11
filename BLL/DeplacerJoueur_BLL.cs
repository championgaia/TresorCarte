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
    public interface IDeplacerJoueur_BLL
    {
        Task<Position> GetNextPositionJoueur(Joueur oCurrentJoueur, int iTour);
    }
    public class DeplacerJoueur_BLL : IDeplacerJoueur_BLL
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oCurrentJoueur"></param>
        /// <param name="iTour"></param>
        /// <returns></returns>
        public async Task<Position> GetNextPositionJoueur(Joueur oCurrentJoueur, int iTour)
        {
            return await GetNextPosition(oCurrentJoueur.JoueurPosition, oCurrentJoueur.CurrentOrientation, oCurrentJoueur.SequenceMovement[iTour - 1]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oJoueurPosition"></param>
        /// <param name="eCurrentOrientation"></param>
        /// <param name="cNextMove"></param>
        /// <returns></returns>
        private async Task<Position> GetNextPosition(Position oJoueurPosition, Orientation eCurrentOrientation, char cNextMove)
        {
            Position oNextPosition = new Position(oJoueurPosition.AxeHorizontal, oJoueurPosition.AxeVertical);
            if ((eCurrentOrientation == Orientation.N && cNextMove == Constants.ABREVIATION_AVANCER) ||
                (eCurrentOrientation == Orientation.O && cNextMove == Constants.ABREVIATION_TOURNER_DROITE) ||
                (eCurrentOrientation == Orientation.E && cNextMove == Constants.ABREVIATION_TOURNER_GAUCHE))
            {
                oNextPosition.AxeVertical--;

            }
            else if ((eCurrentOrientation == Orientation.S && cNextMove == Constants.ABREVIATION_AVANCER) ||
                (eCurrentOrientation == Orientation.O && cNextMove == Constants.ABREVIATION_TOURNER_GAUCHE) ||
                (eCurrentOrientation == Orientation.E && cNextMove == Constants.ABREVIATION_TOURNER_DROITE))
            {
                oNextPosition.AxeVertical++;

            }
            else if ((eCurrentOrientation == Orientation.N && cNextMove == Constants.ABREVIATION_TOURNER_DROITE) ||
                (eCurrentOrientation == Orientation.S && cNextMove == Constants.ABREVIATION_TOURNER_GAUCHE) ||
                (eCurrentOrientation == Orientation.E && cNextMove == Constants.ABREVIATION_AVANCER))
            {
                oNextPosition.AxeHorizontal++;
            }
            else
            {
                oNextPosition.AxeHorizontal--;
            }
            return oNextPosition;
        }
    }
}
