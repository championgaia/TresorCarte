using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumerations;

namespace BLL
{
    public interface IDataValideManage_BLL
    {
        Task<bool> DataValideAsync(string sData, DataType eDataType);
    }
    public class DataValideManage_BLL : IDataValideManage_BLL
    {
        /// <summary>
        /// valide datas
        /// RULE : all the ligne must contain a separator
        /// line map or mountain should contain 3 infos, les 2nd et 3rd info should be number
        /// line treasure should contain 4 infos, les 2nd, 3rd et 4th info should be number
        /// line adventurer should contain 6 infos, les 2nd et 3rd info should be number
        /// </summary>
        /// <param name="sData"></param>
        /// <param name="eDataType"></param>
        /// <returns></returns>
        public async Task<bool> DataValideAsync(string sData, DataType eDataType)
        {
            bool bIsValide = false;
            if (!string.IsNullOrEmpty(sData) && sData.Contains(Constants.SEPERATOR))
            {
                string[] oArrayData = sData.Split(Constants.SEPERATOR);
                bIsValide = eDataType switch
                {
                    DataType.Map => oArrayData.Length >= 3 && int.TryParse(oArrayData[1], out _) && int.TryParse(oArrayData[2], out _),
                    DataType.Mountain => oArrayData.Length >= 3 && int.TryParse(oArrayData[1], out _) && int.TryParse(oArrayData[2], out _),
                    DataType.Treasure => oArrayData.Length >= 4 && int.TryParse(oArrayData[1], out _) && int.TryParse(oArrayData[2], out _) && int.TryParse(oArrayData[3], out _),
                    DataType.TreasureHunter => oArrayData.Length >= 6 && int.TryParse(oArrayData[2], out _) && int.TryParse(oArrayData[3], out _),
                    _ => false
                };
            }
            return bIsValide;
        }
    }
}
