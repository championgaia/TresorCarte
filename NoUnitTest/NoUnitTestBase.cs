using System;
using System.Collections.Generic;
using System.Text;

namespace NoUnitTest
{
    public class NoUnitTestBase
    {
        #region Constants des donnes
        protected const string DONNES_CATRE_VALIDE = "C - 3 - 4";
        protected const string DONNES_MONTAGNE_VALIDE = "M - 1 - 0";
        protected const string DONNES_TRESOR_VALIDE = "T - 0 - 3 - 2";
        protected const string DONNES_JOUEUR_VALIDE = "A - Lara - 1 - 1 - S - AADADAGGA";
        protected const string DONNES_CATRE_NON_VALIDE = "C - 0 - 0";
        protected List<string> Lists_Donnes_Valide = new List<string> { DONNES_CATRE_VALIDE, DONNES_MONTAGNE_VALIDE, DONNES_TRESOR_VALIDE, DONNES_JOUEUR_VALIDE };
        #endregion
        #region constant des fichiers
        protected const string REPERTOIRE_FICHIER = @"..\..\..\FichierTests";
        protected const string FICHIER_OK = "tresor_carte.txt";
        protected const string FICHIER_VIDE = "tresor_carte_vide.txt";
        protected const string FICHIER_SANS_INFO_MAP = "tresor_carte_sans_info_carte.txt";
        protected const string FICHIER_SANS_TRESOR = "tresor_carte_sans_tresor.txt";
        protected const string FICHIER_SANS_JOUEUR = "tresor_carte_sans_joueur.txt";
        #endregion
    }
}
