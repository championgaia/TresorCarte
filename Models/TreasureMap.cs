using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TreasureMap
    {
        /// <summary>
        /// Number Case Large
        /// </summary>
        public int NumberCaseLarge { get; set; }
        /// <summary>
        /// Number Case High
        /// </summary>
        public int NumberCaseHigh { get; set; }
        /// <summary>
        /// All the case
        /// </summary>
        public List<CaseMap> ListeCase { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iNumberCaseLarge"></param>
        /// <param name="iNumberCaseHigh"></param>
        public TreasureMap(int iNumberCaseLarge, int iNumberCaseHigh)
        {
            NumberCaseLarge = iNumberCaseLarge;
            NumberCaseHigh = iNumberCaseHigh;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Constants.ABREVIATION_MAP + Constants.SEPERATOR + NumberCaseLarge + Constants.SEPERATOR + NumberCaseHigh;
        }
    }
}
