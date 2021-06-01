using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumerations;

namespace BLL
{
    public interface IGameManage_BLL
    {
        Task<string> RunGameAsync(List<string> oListDescriptionFile);
        Task<string> FinishGameAsync();
        Task SetGameAsync(Game oGame);
    }
    public class GameManage_BLL : IGameManage_BLL
    {
        private readonly IMapManage_BLL _oIMapManage_BLL;
        private readonly IAdventurerManage_BLL _oIAdventurerManage_BLL;
        private readonly IDeplacementManage_BLL _oIDeplacementManage_BLL;
        private readonly IFileManage_BLL _oIFileManage_BLL;
        private Game CurrentGame { get; set; }
        public GameManage_BLL(IMapManage_BLL oIMapManage_BLL, IAdventurerManage_BLL oIAdventurerManage_BLL, IDeplacementManage_BLL oIDeplacementManage_BLL, IFileManage_BLL oIFileManage_BLL)
        {
            _oIMapManage_BLL = oIMapManage_BLL;
            _oIAdventurerManage_BLL = oIAdventurerManage_BLL;
            _oIDeplacementManage_BLL = oIDeplacementManage_BLL;
            _oIFileManage_BLL = oIFileManage_BLL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oListDescriptionFile"></param>
        /// <returns></returns>
        public async Task<string> RunGameAsync(List<string> oListDescriptionFile)
        {
            await InitiationGameAsync(oListDescriptionFile);
            if (CurrentGame != null && CurrentGame.MaxTurn > 0)
            {
                for (int iTour = 1; iTour < CurrentGame.MaxTurn; iTour++)
                {
                    await AdventurerMakeTurnAsync(iTour);
                }
                return await FinishGameAsync();
            }
            return null;
        }
        /// <summary>
        /// Initialize a game (creation map, adventurers, turns) 
        /// </summary>
        /// <param name="oListDescriptionFile"></param>
        /// <returns></returns>
        private async Task InitiationGameAsync(List<string> oListDescriptionFile)
        {
            if (oListDescriptionFile != null && oListDescriptionFile.Count > 0)
            {
                CurrentGame = new Game
                {
                    Map = await _oIMapManage_BLL.CreateTreasureMapAsync(oListDescriptionFile),
                    Players = await _oIAdventurerManage_BLL.CreateListeAdventurerAsync(oListDescriptionFile)
                };
                CurrentGame.MaxTurn = CurrentGame.Players.Max(j => j.SequenceMovement.Length);
            }

        }
        /// <summary>
        /// an adventurer do a turn
        /// </summary>
        /// <param name="iTurn"></param>
        /// <returns></returns>
        private async Task AdventurerMakeTurnAsync(int iTurn)
        {
            for (int iOrder = 1; iOrder <= CurrentGame.Players.Count; iOrder++)
            {
                Adventurer oCurrentAdventurer = CurrentGame.Players[iOrder - 1];
                Position oAncientPosition = oCurrentAdventurer.AdventurerPosition;
                if (oCurrentAdventurer.SequenceMovement.Length >= iTurn)
                {
                    //find a next position and next orientation
                    (Position oNewPosition, Orientation eNextOrientation)  = await _oIDeplacementManage_BLL.GetNextPositionAdventurerAsync(oCurrentAdventurer, iTurn);
                    if (oNewPosition.CompareTo(oAncientPosition) == 0 && await _oIMapManage_BLL.GetValidePositionAsync(CurrentGame.Map, oNewPosition))
                    {
                        //adventurer can be moved
                        //leave the started case unoccupied
                        CaseMap oAncientCase = CurrentGame.Map.ListeCase.FirstOrDefault(c => oAncientPosition.CompareTo(c.CasePosition) == 1);
                        oAncientCase.IsOccupied = false;
                        //occuper le case d'arrive
                        CaseMap oNewCase = CurrentGame.Map.ListeCase.FirstOrDefault(c => oNewPosition.CompareTo(c.CasePosition) == 1);
                        oNewCase.IsOccupied = true;
                        //changer position du current joueur
                        oCurrentAdventurer.AdventurerPosition = oNewPosition;
                        //recuperer trésor s'il y en a, et décompte le nombre de trésor
                        if (oNewCase.Type == CaseType.Treasure && oNewCase.TreasureNumber > 0)
                        {
                            oCurrentAdventurer.NumberTreasureFound++;
                            oNewCase.TreasureNumber--;
                        }
                    }
                    //set prochaine orientation pour le joueur
                    oCurrentAdventurer.CurrentOrientation = eNextOrientation;
                }
            }
        }
        /// <summary>
        /// Finish the game and send the name of output file
        /// </summary>
        /// <returns></returns>
        public async Task<string> FinishGameAsync()
        {
            return await _oIFileManage_BLL.ExportAsync(Guid.NewGuid().ToString() + Constants.EXTENSION_TEXT, CurrentGame);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oGame"></param>
        /// <returns></returns>
        public async Task SetGameAsync(Game oGame)
        {
            CurrentGame = oGame;
        }
    }
}
