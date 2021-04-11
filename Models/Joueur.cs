﻿using System;
using System.Collections.Generic;
using System.Text;
using static Common.Enumerations;

namespace Models
{
    public class Joueur
    {
        public string Nom { get; set; }
        public int OrderDeJouer { get; set; }
        public Position JoueurPosition { get; set; }
        public Orientation CurrentOrientation { get; set; }
        public char[] SequenceMovement { get; set; }
        public int NombreTresorTrouve { get; set; }
        public Joueur(string sNom, Orientation eOrientation, char[] oSequence, int iOrderDeJouer)
        {
            Nom = sNom;
            CurrentOrientation = eOrientation;
            SequenceMovement = oSequence;
            OrderDeJouer = iOrderDeJouer;
        }
    }
}
