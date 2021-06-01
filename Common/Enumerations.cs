using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Enumerations
    {
        public enum Orientation
        {
            /// <summary>
            /// Nord
            /// </summary>
            N = 0,
            /// <summary>
            /// South
            /// </summary>
            S = 1,
            /// <summary>
            /// WEST
            /// </summary>
            O = 2,
            /// <summary>
            /// EAST
            /// </summary>
            E = 3
        }
        public enum Mouvement
        {
            /// <summary>
            /// Forward
            /// </summary>
            A = 0,
            /// <summary>
            /// Turn left
            /// </summary>
            G = 1,
            /// <summary>
            /// Turn right
            /// </summary>
            D = 2
        }
        public enum CaseType
        {
            Neutre = 0,
            Mountain = 1,
            Treasure = 2
        }
        public enum DataType
        {
            Map = 0,
            Mountain = 1,
            Treasure = 2,
            TreasureHunter = 3
        }
    }
}
