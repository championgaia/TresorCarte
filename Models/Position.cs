using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Position : IComparable
    {
        /// <summary>
        /// 
        /// </summary>
        public int AxeHorizontal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AxeVertical { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iAxeHorizontal"></param>
        /// <param name="iAxeVertical"></param>
        public Position(int iAxeHorizontal, int iAxeVertical)
        {
            AxeHorizontal = iAxeHorizontal;
            AxeVertical = iAxeVertical;
        }
        /// <summary>
        /// methode compare two positions on the map
        /// return 1 if same position, return 0 if not
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is Position oPosition)
            {
                return Convert.ToInt32(AxeHorizontal == oPosition.AxeHorizontal && AxeVertical == oPosition.AxeVertical);
            }
            return 0;
        }
    }
}
