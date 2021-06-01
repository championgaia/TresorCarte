using Common;
using System;
using System.Collections.Generic;
using System.Text;
using static Common.Enumerations;

namespace Models
{
    public class Adventurer
    {
        /// <summary>
        /// nom de joueur / aventurier
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// l'ordre du joueur dans un tour
        /// </summary>
        public int TurnOrder { get; set; }
        /// <summary>
        /// position sur la carte du joueur
        /// </summary>
        public Position AdventurerPosition { get; set; }
        /// <summary>
        /// l'orientation (N, S, O, E) du joueur
        /// </summary>
        public Orientation CurrentOrientation { get; set; }
        /// <summary>
        /// la séquence de mouvement du joueur
        /// </summary>
        public char[] SequenceMovement { get; set; }
        /// <summary>
        /// nombre de trésor trouvé par joueur
        /// </summary>
        public int NumberTreasureFound { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="eOrientation"></param>
        /// <param name="oSequence"></param>
        /// <param name="iTurnOrder"></param>
        public Adventurer(string sName, Orientation eOrientation, char[] oSequence, int iTurnOrder)
        {
            Name = sName;
            CurrentOrientation = eOrientation;
            SequenceMovement = oSequence;
            TurnOrder = iTurnOrder;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Constants.ABREVIATION_AVENTURER + Constants.SEPERATOR + Name + Constants.SEPERATOR + AdventurerPosition?.AxeHorizontal + Constants.SEPERATOR + AdventurerPosition?.AxeVertical + 
                Constants.SEPERATOR + CurrentOrientation.ToString() + Constants.SEPERATOR + NumberTreasureFound;
        }
    }
}
