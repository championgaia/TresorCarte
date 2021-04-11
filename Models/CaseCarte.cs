using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CaseCarte
    {
        public Position CasePosition { get; set; }
        public Enumerations.CaseType Type { get; set; }
        public int NombreTresor { get; set; }
        public bool EstOccupe { get; set; }
        public CaseCarte(Position oCasePosition, Enumerations.CaseType eType, int iNombreTresor, bool bEstOccupe)
        {
            CasePosition = oCasePosition;
            Type = eType;
            NombreTresor = iNombreTresor;
            EstOccupe = bEstOccupe;
        }
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
