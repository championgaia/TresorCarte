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
    public interface IMapManage_BLL
    {
        Task<TreasureMap> CreateTreasureMapAsync(List<string> oListDescriptionFile);
        Task<bool> GetValidePositionAsync(TreasureMap oMap, Position oNextPosition);
    }
    public class MapManage_BLL : IMapManage_BLL
    {
        private TreasureMap CurrentMap { get; set; } = null;
        private readonly IDataValideManage_BLL _oIDataValideManage_BLL;
        public MapManage_BLL(IDataValideManage_BLL oIDataValideManage_BLL)
        {
            _oIDataValideManage_BLL = oIDataValideManage_BLL;
        }
        /// <summary>
        /// Create a map
        /// </summary>
        /// <param name="oListDescriptionFile"></param>
        /// <returns></returns>
        public async Task<TreasureMap> CreateTreasureMapAsync(List<string> oListDescriptionFile)
        {
            CurrentMap = null;
            if (oListDescriptionFile != null && oListDescriptionFile.Count > 0)
            {
                CurrentMap = await InitializeMapAsync(oListDescriptionFile.FirstOrDefault(o => o.StartsWith(Constants.ABREVIATION_MAP)));
                if (CurrentMap != null)
                {
                    CurrentMap.ListeCase = await InitialiserCasesAsync();
                    await InitialiserCaseMountainAsync(oListDescriptionFile.FindAll(o => o.StartsWith(Constants.ABREVIATION_MOUNTAIN)));
                    await InitialiserCaseTreasureAsync(oListDescriptionFile.FindAll(o => o.StartsWith(Constants.ABREVIATION_TREASURE)));
                    await InitializePositionAdventurerAsync(oListDescriptionFile.FindAll(o => o.StartsWith(Constants.ABREVIATION_AVENTURER)));
                }
            }
            return CurrentMap;
        }
        /// <summary>
        /// Générer la carte vide (sans case)
        /// </summary>
        /// <param name="sInfoMap"></param>
        /// <returns></returns>
        private async Task<TreasureMap> InitializeMapAsync(string sInfoMap)
        {
            CurrentMap = null;
            if (await _oIDataValideManage_BLL.DataValideAsync(sInfoMap, DataType.Map))
            {
                string[] oMapData = sInfoMap.Split(Constants.SEPERATOR);
                CurrentMap = new TreasureMap(int.Parse(oMapData[1]), int.Parse(oMapData[2]));
                if (CurrentMap.NumberCaseHigh * CurrentMap.NumberCaseLarge <= 0)
                {
                    CurrentMap = null;
                }
            }
            return CurrentMap;
        }
        /// <summary>
        /// case contain treasure
        /// </summary>
        /// <param name="oListInfoCaseTreasure"></param>
        /// <returns></returns>
        private async Task InitialiserCaseTreasureAsync(List<string> oListInfoCaseTreasure)
        {
            if (CurrentMap != null && CurrentMap.ListeCase != null && CurrentMap.ListeCase.Count > 0 && oListInfoCaseTreasure != null && oListInfoCaseTreasure.Count > 0)
            {
                foreach (var sInfoCaseTreasure in oListInfoCaseTreasure)
                {
                    if (await _oIDataValideManage_BLL.DataValideAsync(sInfoCaseTreasure, DataType.Treasure))
                    {
                        string[] oCaseTreasureData = sInfoCaseTreasure.Split(Constants.SEPERATOR);
                        Position oTresurePosition = new Position(int.Parse(oCaseTreasureData[1]), int.Parse(oCaseTreasureData[2]));
                        CaseMap oCaseMap = CurrentMap.ListeCase.FirstOrDefault(c => c.CasePosition.CompareTo(oTresurePosition) == 1);
                        if (oCaseMap != null)
                        {
                            oCaseMap.Type = CaseType.Treasure;
                            oCaseMap.TreasureNumber = int.Parse(oCaseTreasureData[3]);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Mountain case
        /// </summary>
        /// <param name="oListeCaseMountain"></param>
        /// <returns></returns>
        private async Task InitialiserCaseMountainAsync(List<string> oListeCaseMountain)
        {
            if (CurrentMap != null && CurrentMap.ListeCase != null && CurrentMap.ListeCase.Count > 0 && oListeCaseMountain != null && oListeCaseMountain.Count > 0)
            {
                foreach (var sInfoCaseMountain in oListeCaseMountain)
                {
                    if (await _oIDataValideManage_BLL.DataValideAsync(sInfoCaseMountain, DataType.Mountain))
                    {
                        string[] oCaseMountainData = sInfoCaseMountain.Split(Constants.SEPERATOR);
                        Position oMontagnePosition = new Position(int.Parse(oCaseMountainData[1]), int.Parse(oCaseMountainData[2]));
                        CaseMap oCaseMap = CurrentMap.ListeCase.FirstOrDefault(c => c.CasePosition.CompareTo(oMontagnePosition) == 1);
                        if (oCaseMap != null)
                        {
                            oCaseMap.Type = CaseType.Mountain;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Generate all cases type neutre, 0 treasure, not occupied
        /// </summary>
        /// <returns></returns>
        private async Task<List<CaseMap>> InitialiserCasesAsync()
        {
            if (CurrentMap != null)
            {
                CurrentMap.ListeCase = new List<CaseMap>();
                for (var ligne = 0; ligne < CurrentMap.NumberCaseLarge; ligne++)
                {
                    for (var colonne = 0; colonne < CurrentMap.NumberCaseHigh; colonne++)
                    {
                        var oCaseMap = new CaseMap(new Position(ligne, colonne), CaseType.Neutre, 0, false);
                        CurrentMap.ListeCase.Add(oCaseMap);
                    }
                }
            }
            return CurrentMap?.ListeCase;
        }
        /// <summary>
        /// cases occupied by an Adventurer
        /// </summary>
        /// <param name="oListInfoAdventurer"></param>
        /// <returns></returns>
        private async Task InitializePositionAdventurerAsync(List<string> oListInfoAdventurer)
        {
            if (oListInfoAdventurer != null && oListInfoAdventurer.Count > 0)
            {
                foreach (var oInfoAdventurer in oListInfoAdventurer)
                {
                    await SetCaseOccupiedAsync(oInfoAdventurer);
                }
            }
        }
        /// <summary>
        /// Set a case occupied
        /// </summary>
        /// <param name="oInfoAdventurer"></param>
        /// <returns></returns>
        private async Task SetCaseOccupiedAsync(string oInfoAdventurer)
        {
            if (CurrentMap != null && await _oIDataValideManage_BLL.DataValideAsync(oInfoAdventurer, DataType.TreasureHunter))
            {
                string[] oArrayDataAdventurer = oInfoAdventurer.Split(Constants.SEPERATOR);
                CaseMap oCaseOccupiedVyAdventurer = CurrentMap.ListeCase
                                                .FirstOrDefault(c => c.CasePosition.CompareTo(new Position(int.Parse(oArrayDataAdventurer[2]), int.Parse(oArrayDataAdventurer[3]))) == 1);
                if (oCaseOccupiedVyAdventurer != null)
                {
                    oCaseOccupiedVyAdventurer.IsOccupied = true;
                }
            }
        }
        /// <summary>
        /// Valide a position on the map
        /// </summary>
        /// <param name="oMap"></param>
        /// <param name="oNextPosition"></param>
        /// <returns></returns>
        public async Task<bool> GetValidePositionAsync(TreasureMap oMap, Position oNextPosition)
        {
            if (oNextPosition.AxeHorizontal >= 0 && oNextPosition.AxeHorizontal < oMap.NumberCaseLarge &&
                oNextPosition.AxeVertical >= 0 && oNextPosition.AxeVertical < oMap.NumberCaseHigh)
            {
                CaseMap oCaseCarte = oMap.ListeCase.FirstOrDefault(c => c.CasePosition.CompareTo(oNextPosition) == 1);
                return !oCaseCarte.IsOccupied && oCaseCarte.Type != CaseType.Mountain;
            }
            return false;
        }
    }
}
