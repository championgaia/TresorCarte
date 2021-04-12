using Common;
using System;
using System.Collections.Generic;
using System.Text;
using static Common.Enumerations;

namespace Models
{
    public class Joueur
    {
        /// <summary>
        /// nom de joueur / aventurier
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// l'ordre du joueur dans un tour
        /// </summary>
        public int OrderDeJouer { get; set; }
        /// <summary>
        /// position sur la carte du joueur
        /// </summary>
        public Position JoueurPosition { get; set; }
        /// <summary>
        /// l'orientation (N, S, O, E) du joueur
        /// </summary>
        public Orientation CurrentOrientation { get; set; }
        /// <summary>
        /// la séquence de mouvement du joueur
        /// </summary>
        public char[] SequenceMovement { get; set; }
        /// <summary>
        /// nombre de trésor trouvé par joueur
        /// </summary>
        public int NombreTresorTrouve { get; set; }
        /// <summary>
        /// constructeur par déffaut
        /// </summary>
        /// <param name="sNom"></param>
        /// <param name="eOrientation"></param>
        /// <param name="oSequence"></param>
        /// <param name="iOrderDeJouer"></param>
        public Joueur(string sNom, Orientation eOrientation, char[] oSequence, int iOrderDeJouer)
        {
            Nom = sNom;
            CurrentOrientation = eOrientation;
            SequenceMovement = oSequence;
            OrderDeJouer = iOrderDeJouer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Constants.ABREVIATION_JOUEUR + Constants.SEPERATEUR + Nom + Constants.SEPERATEUR + JoueurPosition?.AxeHorizontal + Constants.SEPERATEUR + JoueurPosition?.AxeVertical + 
                Constants.SEPERATEUR + CurrentOrientation.ToString() + Constants.SEPERATEUR + NombreTresorTrouve;
        }
    }
}
