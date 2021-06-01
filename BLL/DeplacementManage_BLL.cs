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
    public interface IDeplacementManage_BLL
    {
        Task<(Position, Orientation)> GetNextPositionAdventurerAsync(Adventurer oCurrentAdventurer, int iTurn);
    }
    public class DeplacementManage_BLL : IDeplacementManage_BLL
    {
        
        /// <summary>
        /// Get next postion and next orientation for an adventurer
        /// </summary>
        /// <param name="oCurrentAdventurer"></param>
        /// <param name="iTurn"></param>
        /// <returns></returns>
        public async Task<(Position, Orientation)> GetNextPositionAdventurerAsync(Adventurer oCurrentAdventurer, int iTurn)
        {
            return await GetNextPosition(oCurrentAdventurer.AdventurerPosition, oCurrentAdventurer.CurrentOrientation, oCurrentAdventurer.SequenceMovement[iTurn - 1]);
        }
        /// <summary>
        /// Get next postion and next orientation for an adventurer by his current position, his current orientation and his next move
        /// </summary>
        /// <param name="oAdventurerPosition"></param>
        /// <param name="eCurrentOrientation"></param>
        /// <param name="cNextMove"></param>
        /// <returns></returns>
        private async Task<(Position, Orientation)> GetNextPosition(Position oAdventurerPosition, Orientation eCurrentOrientation, char cNextMove)
        {
            Position oNextPosition = new Position(oAdventurerPosition.AxeHorizontal, oAdventurerPosition.AxeVertical);
            if ((eCurrentOrientation == Orientation.N && cNextMove == Constants.ABREVIATION_FORWARD) ||
                (eCurrentOrientation == Orientation.O && cNextMove == Constants.ABREVIATION_TURN_RIGHT) ||
                (eCurrentOrientation == Orientation.E && cNextMove == Constants.ABREVIATION_TURN_LEFT))
            {
                oNextPosition.AxeVertical--;
                eCurrentOrientation = Orientation.N;
            }
            else if ((eCurrentOrientation == Orientation.S && cNextMove == Constants.ABREVIATION_FORWARD) ||
                (eCurrentOrientation == Orientation.O && cNextMove == Constants.ABREVIATION_TURN_LEFT) ||
                (eCurrentOrientation == Orientation.E && cNextMove == Constants.ABREVIATION_TURN_RIGHT))
            {
                oNextPosition.AxeVertical++;
                eCurrentOrientation = Orientation.S;
            }
            else if ((eCurrentOrientation == Orientation.N && cNextMove == Constants.ABREVIATION_TURN_RIGHT) ||
                (eCurrentOrientation == Orientation.S && cNextMove == Constants.ABREVIATION_TURN_LEFT) ||
                (eCurrentOrientation == Orientation.E && cNextMove == Constants.ABREVIATION_FORWARD))
            {
                oNextPosition.AxeHorizontal++;
                eCurrentOrientation = Orientation.E;
            }
            else
            {
                oNextPosition.AxeHorizontal--;
                eCurrentOrientation = Orientation.O;
            }
            return (oNextPosition, eCurrentOrientation);
        }
    }
}
