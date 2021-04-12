using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Position : IComparable
    {
        /// <summary>
        /// valeur sur l'axe Horizontal
        /// </summary>
        public int AxeHorizontal { get; set; }
        /// <summary>
        /// valeur sur l'axe vertical
        /// </summary>
        public int AxeVertical { get; set; }
        /// <summary>
        /// constructeur par déffaut
        /// </summary>
        /// <param name="iAxeHorizontal"></param>
        /// <param name="iAxeVertical"></param>
        public Position(int iAxeHorizontal, int iAxeVertical)
        {
            AxeHorizontal = iAxeHorizontal;
            AxeVertical = iAxeVertical;
        }
        /// <summary>
        /// méthode compare les deux positions sur la carte
        /// return 1 si meme position, return 0 si non
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
