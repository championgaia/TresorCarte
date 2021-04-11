using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TresorCarte
    {
        public int NombreCaseLargeur { get; set; }
        public int NombreCaseHauteur { get; set; }
        public List<CaseCarte> ListeCase { get; set; }
        public TresorCarte(int iNombreCaseLargeur, int iNombreCaseHauteur)
        {
            NombreCaseLargeur = iNombreCaseLargeur;
            NombreCaseHauteur = iNombreCaseHauteur;
        }
        public override string ToString()
        {
            return Constants.ABREVIATION_CARTE + Constants.SEPERATEUR + NombreCaseLargeur + Constants.SEPERATEUR + NombreCaseHauteur;
        }
    }
}
