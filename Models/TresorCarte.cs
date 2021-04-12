using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TresorCarte
    {
        /// <summary>
        /// Nombre de case en largeur
        /// </summary>
        public int NombreCaseLargeur { get; set; }
        /// <summary>
        /// Nombre de case en hauteur
        /// </summary>
        public int NombreCaseHauteur { get; set; }
        /// <summary>
        /// ensemble des cases
        /// </summary>
        public List<CaseCarte> ListeCase { get; set; }
        /// <summary>
        /// constructeur par déffaut
        /// </summary>
        /// <param name="iNombreCaseLargeur"></param>
        /// <param name="iNombreCaseHauteur"></param>
        public TresorCarte(int iNombreCaseLargeur, int iNombreCaseHauteur)
        {
            NombreCaseLargeur = iNombreCaseLargeur;
            NombreCaseHauteur = iNombreCaseHauteur;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Constants.ABREVIATION_CARTE + Constants.SEPERATEUR + NombreCaseLargeur + Constants.SEPERATEUR + NombreCaseHauteur;
        }
    }
}
