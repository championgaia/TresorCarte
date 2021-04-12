using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Jeu
    {
        /// <summary>
        /// la carte de trésor
        /// </summary>
        public TresorCarte Carte { get; set; }
        /// <summary>
        /// liste des joueurs/aventuriers
        /// </summary>
        public List<Joueur> Joueurs { get; set; }
        /// <summary>
        /// nombre de tour
        /// </summary>
        public int NombreDeplacementMax { get; set; }
    }
}
