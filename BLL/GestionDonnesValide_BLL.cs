﻿using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumerations;

namespace BLL
{
    public interface IGestionDonnesValide_BLL
    {
        Task<bool> DonnesValide(string sDonnes, DonnesType eDonnesType) 
    }
    public class GestionDonnesValide_BLL : IGestionDonnesValide_BLL
    {
        public async Task<bool> DonnesValide(string sDonnes, DonnesType eDonnesType)
        {
            bool bEstValide = false;
            if (!string.IsNullOrEmpty(sDonnes) && sDonnes.Contains(Constants.SEPERATEUR))
            {
                string[] oArrayDonnes = sDonnes.Split(Constants.SEPERATEUR);
                switch (eDonnesType)
                {
                    case DonnesType.Carte:
                        {
                            bEstValide = oArrayDonnes.Length >= 3 && int.TryParse(oArrayDonnes[1], out _) && int.TryParse(oArrayDonnes[2], out _);
                            break;
                        }
                    case DonnesType.Montagne:
                        {
                            bEstValide = oArrayDonnes.Length >= 3 && int.TryParse(oArrayDonnes[1], out _) && int.TryParse(oArrayDonnes[2], out _);
                            break;
                        }
                    case DonnesType.Tresor:
                        {
                            bEstValide = oArrayDonnes.Length >= 4 && int.TryParse(oArrayDonnes[1], out _) && int.TryParse(oArrayDonnes[2], out _) && int.TryParse(oArrayDonnes[3], out _);
                            break;
                        }
                    case DonnesType.Joueur:
                        {
                            bEstValide = oArrayDonnes.Length >= 6 && int.TryParse(oArrayDonnes[2], out _) && int.TryParse(oArrayDonnes[3], out _);
                            break;
                        }
                }
            }
            return bEstValide;
        }
    }
}