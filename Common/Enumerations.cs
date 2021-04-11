using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Enumerations
    {
        public enum Orientation
        {
            /// <summary>
            /// Nord
            /// </summary>
            N = 0,
            /// <summary>
            /// Sud
            /// </summary>
            S = 1,
            /// <summary>
            /// Ouest
            /// </summary>
            O = 2,
            /// <summary>
            /// Est
            /// </summary>
            E = 3
        }
        public enum Mouvement
        {
            /// <summary>
            /// Avancer
            /// </summary>
            A = 0,
            /// <summary>
            /// tourner à gauche
            /// </summary>
            G = 1,
            /// <summary>
            /// Tourner à droite
            /// </summary>
            D = 2
        }
        public enum CaseType
        {
            Neutre = 0,
            Montagne = 1,
            Tresor = 2
        }
        public enum DonnesType
        {
            Carte = 0,
            Montagne = 1,
            Tresor = 2,
            Joueur = 3
        }
    }
}
