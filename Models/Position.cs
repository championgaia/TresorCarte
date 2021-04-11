using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Position : IComparable
    {
        public int AxeHorizontal { get; set; }
        public int AxeVertical { get; set; }
        public Position(int iAxeHorizontal, int iAxeVertical)
        {
            AxeHorizontal = iAxeHorizontal;
            AxeVertical = iAxeVertical;
        }

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
