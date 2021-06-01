using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Constants
    {
        public const string SEPERATOR = " - ";
        public const char ABREVIATION_FORWARD = 'A';
        public const char ABREVIATION_TURN_LEFT = 'G';
        public const char ABREVIATION_TURN_RIGHT = 'D';

        public const string ABREVIATION_MAP = "C";
        public const string ABREVIATION_MOUNTAIN = "M";
        public const string ABREVIATION_TREASURE = "T";
        public const string ABREVIATION_AVENTURER= "A";
        public const string ABREVIATION_COMMENT= @"#";

        public const string COMMENT_MAP = @"# {C comme Carte} - {Nb. de case en largeur} - {Nb. de case en hauteur}";
        public const string COMMENT_MOUNTAIN = @"# {M comme Montagne} - {Axe horizontal} - {Axe vertical}";
        public const string COMMENT_TREASURE = @"# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}";
        public const string COMMENT_AVENTURER = @"# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Nb.trésors ramassés}";

        public const string FOLDER_TEMPS = @"C:\TEMPS";

        public const string DATA_MAP_INVALIDE = @"- Le fichier ne contient aucune ligne Carte ";
        public const string DATA_TREASURE_INVALIDE = @"- Le fichier ne contient aucune ligne Trésor ";
        public const string DATA_AVENTURER_INVALIDE = @"- Le fichier ne contient aucune ligne Aventurier ";

        public const string TEXT_XML = "text/xml";
        public const string EXTENSION_TEXT = ".txt";

        public const string LINE_BREAK_HTML = @"<br />";

        public const string FILE_NAME_EMPTY = "nom du fichier est vide";
        public const string FILE_NOT_FOUND = "fichier non trouvé";
    }
}
