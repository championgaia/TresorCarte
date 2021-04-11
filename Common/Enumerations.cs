using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Enumerations
    {
        public enum Orientation
        {
            Nord = 0,
            Sud = 1,
            Ouest = 2,
            Est = 3
        }
        public enum Mouvement
        {
            A = 0,
            G = 1,
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
