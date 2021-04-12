using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CaseCarte
    {
        /// <summary>
        /// position de la case
        /// </summary>
        public Position CasePosition { get; set; }
        /// <summary>
        /// type de la case : neutre ou montagne ou trésor
        /// </summary>
        public Enumerations.CaseType Type { get; set; }
        /// <summary>
        /// nombre trésor se trouve sur la case
        /// </summary>
        public int NombreTresor { get; set; }
        /// <summary>
        /// l'occupation de la case
        /// </summary>
        public bool EstOccupe { get; set; }
        /// <summary>
        /// constructeur par déffaut
        /// </summary>
        /// <param name="oCasePosition"></param>
        /// <param name="eType"></param>
        /// <param name="iNombreTresor"></param>
        /// <param name="bEstOccupe"></param>
        public CaseCarte(Position oCasePosition, Enumerations.CaseType eType, int iNombreTresor, bool bEstOccupe)
        {
            CasePosition = oCasePosition;
            Type = eType;
            NombreTresor = iNombreTresor;
            EstOccupe = bEstOccupe;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string sCommonChaine = Constants.SEPERATEUR + CasePosition?.AxeHorizontal + Constants.SEPERATEUR + CasePosition?.AxeVertical;
            if (Type == Enumerations.CaseType.Montagne)
            {
                return Constants.ABREVIATION_MONTAGNE + sCommonChaine;
            }
            else if (Type == Enumerations.CaseType.Tresor)
            {
                return Constants.ABREVIATION_TRESOR + sCommonChaine + Constants.SEPERATEUR + NombreTresor;
            }
            else
            {
                return sCommonChaine;
            }
        }
    }
}
