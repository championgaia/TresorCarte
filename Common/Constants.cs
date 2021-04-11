using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Constants
    {
        public const string SEPERATEUR = " - ";
        public const char ABREVIATION_AVANCER = 'A';
        public const char ABREVIATION_TOURNER_GAUCHE = 'G';
        public const char ABREVIATION_TOURNER_DROITE = 'D';

        public const string ABREVIATION_CARTE = "C";
        public const string ABREVIATION_MONTAGNE = "M";
        public const string ABREVIATION_TRESOR = "T";
        public const string ABREVIATION_JOUEUR = "A";
        public const string ABREVIATION_COMMENTAIRE= @"#";

        public const string COMMENTAIRE_CARTE = @"# {C comme Carte} - {Nb. de case en largeur} - {Nb. de case en hauteur}";
        public const string COMMENTAIRE_MONTAGNE = @"# {M comme Montagne} - {Axe horizontal} - {Axe vertical}";
        public const string COMMENTAIRE_TRESOR = @"# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}";
        public const string COMMENTAIRE_JOUEUR = @"# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Nb.trésors ramassés}";

        public const string DOSSIER_TEMPS = @"C:\TEMPS";

        public const string DONNES_CARTE_INVALIDE = @"- Le fichier ne contient aucune ligne Carte ";
        public const string DONNES_TRESOR_INVALIDE = @"- Le fichier ne contient aucune ligne Trésor ";
        public const string DONNES_JOUEUR_INVALIDE = @"- Le fichier ne contient aucune ligne Aventurier ";

        public const string TEXT_XML = "text/xml";
    }
}
