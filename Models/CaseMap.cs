using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CaseMap
    {
        /// <summary>
        /// position case
        /// </summary>
        public Position CasePosition { get; set; }
        /// <summary>
        /// type of case : neutre or moutaine or treasure
        /// </summary>
        public Enumerations.CaseType Type { get; set; }
        /// <summary>
        /// number of treasure in the case
        /// </summary>
        public int TreasureNumber { get; set; }
        /// <summary>
        /// case is occupied by a TreasureHunter
        /// </summary>
        public bool IsOccupied { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oCasePosition"></param>
        /// <param name="eType"></param>
        /// <param name="iNumberTreasure"></param>
        /// <param name="bIsOccupied"></param>
        public CaseMap(Position oCasePosition, Enumerations.CaseType eType, int iNumberTreasure, bool bIsOccupied)
        {
            CasePosition = oCasePosition;
            Type = eType;
            TreasureNumber = iNumberTreasure;
            IsOccupied = bIsOccupied;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string sCommonChaine = Constants.SEPERATOR + CasePosition?.AxeHorizontal + Constants.SEPERATOR + CasePosition?.AxeVertical;
            if (Type == Enumerations.CaseType.Mountain)
            {
                return Constants.ABREVIATION_MOUNTAIN + sCommonChaine;
            }
            else if (Type == Enumerations.CaseType.Treasure)
            {
                return Constants.ABREVIATION_TREASURE + sCommonChaine + Constants.SEPERATOR + TreasureNumber;
            }
            else
            {
                return sCommonChaine;
            }
        }
    }
}
