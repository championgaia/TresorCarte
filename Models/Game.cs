using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Game
    {
        /// <summary>
        /// 
        /// </summary>
        public TreasureMap Map { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Adventurer> Players { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaxTurn { get; set; }
    }
}
