using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumerations;

namespace BLL
{
    public interface IGestionDonnesValide_BLL
    {
        Task<bool> DonnesValide(string sDonnes, DonnesType eDonnesType);
    }
    public class GestionDonnesValide_BLL : IGestionDonnesValide_BLL
    {
        /// <summary>
        /// valider les donnes
        /// REGLE : tous les lignes doivement contenir les séparateurs
        /// ligne carte ou montagne doit contenir au moins 3 infos, les 2e et 3e info derniers doivent etre les chiffres
        /// ligne trésor doit contenir au moins 4 infos, les 2e, 3e et 4e info doivent etre les chiffres
        /// ligne joueur doit contenir au moins 6 infos, les 2e et 3e info doivent etre les chiffres
        /// </summary>
        /// <param name="sDonnes"></param>
        /// <param name="eDonnesType"></param>
        /// <returns></returns>
        public async Task<bool> DonnesValide(string sDonnes, DonnesType eDonnesType)
        {
            bool bEstValide = false;
            if (!string.IsNullOrEmpty(sDonnes) && sDonnes.Contains(Constants.SEPERATEUR))
            {
                string[] oArrayDonnes = sDonnes.Split(Constants.SEPERATEUR);
                bEstValide = eDonnesType switch
                {
                    DonnesType.Carte => oArrayDonnes.Length >= 3 && int.TryParse(oArrayDonnes[1], out _) && int.TryParse(oArrayDonnes[2], out _),
                    DonnesType.Montagne => oArrayDonnes.Length >= 3 && int.TryParse(oArrayDonnes[1], out _) && int.TryParse(oArrayDonnes[2], out _),
                    DonnesType.Tresor => oArrayDonnes.Length >= 4 && int.TryParse(oArrayDonnes[1], out _) && int.TryParse(oArrayDonnes[2], out _) && int.TryParse(oArrayDonnes[3], out _),
                    DonnesType.Joueur => oArrayDonnes.Length >= 6 && int.TryParse(oArrayDonnes[2], out _) && int.TryParse(oArrayDonnes[3], out _),
                    _ => false
                };
            }
            return bEstValide;
        }
    }
}
