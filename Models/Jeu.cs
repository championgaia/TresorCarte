using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Jeu
    {
        public TresorCarte Carte { get; set; }
        public List<Joueur> Joueurs { get; set; }
        public int NombreDeplacementMax { get; set; }
    }
}
