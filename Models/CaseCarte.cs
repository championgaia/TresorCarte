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
        public CaseCarte(Position oCasePosition, Enumerations.CaseType eType, int iNombreTresor)
        {
            CasePosition = oCasePosition;
            Type = eType;
            NombreTresor = iNombreTresor;
        }
    }
}
