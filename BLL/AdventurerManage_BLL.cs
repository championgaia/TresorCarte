using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumerations;

namespace BLL
{
    public interface IAdventurerManage_BLL
    {
        Task<List<Adventurer>> CreateListeAdventurerAsync(List<string> oListDescriptionFichier);
    }
    public class AdventurerManage_BLL : IAdventurerManage_BLL
    {
        private readonly IDataValideManage_BLL _oIGestionDonnesValide_BLL;
        public AdventurerManage_BLL(IDataValideManage_BLL oIGestionDonnesValide_BLL)
        {
            _oIGestionDonnesValide_BLL = oIGestionDonnesValide_BLL;
        }
        /// <summary>
        /// Create Adventurers
        /// </summary>
        /// <param name="oListDescriptionFile"></param>
        /// <returns></returns>
        public async Task<List<Adventurer>> CreateListeAdventurerAsync(List<string> oListDescriptionFile)
        {
            List<Adventurer> oListeAdventurer = null;
            if (oListDescriptionFile != null && oListDescriptionFile.Count > 0)
            {
                List<string> oListInfoAdventurer = oListDescriptionFile.FindAll(o => o.StartsWith(Constants.ABREVIATION_AVENTURER));
                if (oListInfoAdventurer != null && oListInfoAdventurer.Count > 0)
                {
                    oListeAdventurer = new List<Adventurer>();
                    foreach (var oInfoAdventurer in oListInfoAdventurer)
                    {
                        Adventurer oAdventurer = await CreateAnAdventurerAsync(oInfoAdventurer, oListInfoAdventurer.IndexOf(oInfoAdventurer));
                        if (oAdventurer != null)
                        {
                            oListeAdventurer.Add(oAdventurer);
                        }
                    }
                }
            }
            return oListeAdventurer;
        }
        /// <summary>
        /// Créer un joueur avec des infos
        /// </summary>
        /// <param name="oInfoAdventurer"></param>
        /// <param name="iIndexInfoAdventurer"></param>
        /// <returns></returns>
        private async Task<Adventurer> CreateAnAdventurerAsync(string oInfoAdventurer, int iIndexInfoAdventurer)
        {
            Adventurer oJoueur = null;
            if (await _oIGestionDonnesValide_BLL.DataValideAsync(oInfoAdventurer, DataType.TreasureHunter))
            {
                string[] oArrayAdventurerData = oInfoAdventurer.Split(Constants.SEPERATOR);
                oJoueur = new Adventurer(oArrayAdventurerData[1], (Orientation)Enum.Parse(typeof(Orientation), oArrayAdventurerData[4]), oArrayAdventurerData[5].ToCharArray(), iIndexInfoAdventurer + 1)
                {
                    AdventurerPosition = new Position(int.Parse(oArrayAdventurerData[2]), int.Parse(oArrayAdventurerData[3]))
                };
            }
            return oJoueur;
        }
    }
}
