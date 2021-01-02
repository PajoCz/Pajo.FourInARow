using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajo.FourInARow.Engine.Solver
{
    public class BruteForceSolver
    {
        public int NextStepAddToColumn(Board p_Board, BoardValue p_BoardValue)
        {
            int bestWinCol = 0;
            var bestWinDepth = int.MaxValue;
            var bestLostCol = 0;
            var bestLostDepth = int.MinValue;
            for (int col = 1; col <= p_Board.ColumnsCount; col++)
            {
                var depth = CheckWinnerNextDepth(p_Board, p_BoardValue, p_BoardValue, col, 1);
                if (depth > 0 && depth < bestWinDepth)
                {
                    bestWinDepth = depth.Value;
                    bestWinCol = col;
                }

                if (depth < 0 && depth > bestLostDepth)
                {
                    bestLostDepth = depth.Value;
                    bestLostCol = col;
                }
            }

            if (bestLostDepth > -4)
                return bestLostCol;

            return bestWinCol > 0 ? bestWinCol : bestLostCol;
        }

        private int? CheckWinnerNextDepth(Board p_Board, BoardValue p_BoardValueWinner, BoardValue p_BoardValue,
            int p_Col, int p_Depth)
        {
            if (!p_Board.AddToColumn(p_Col, p_BoardValue))
                throw new Exception("Not supported algo - first check if can add to column");
            var winner = p_Board.CheckWinner();
            if (winner == p_BoardValueWinner)
            {
                p_Board.RemoveFromColumn(p_Col);
                return p_Depth;
            }

            if (winner == (p_BoardValueWinner == BoardValue.Red ? BoardValue.Yellow : BoardValue.Red))
            {
                p_Board.RemoveFromColumn(p_Col);
                return -p_Depth;
            }

            for (int col = 1; col <= p_Board.ColumnsCount; col++)
            {
                if (!p_Board.CanAddToColumn(col))
                    continue;

                var foundDepth = CheckWinnerNextDepth(p_Board, p_BoardValueWinner,
                    p_BoardValue == BoardValue.Red ? BoardValue.Yellow : BoardValue.Red, col, p_Depth+1);
                //if (!foundDepth.HasValue)
                //    return null;    //end of recursion

                p_Board.RemoveFromColumn(p_Col);
                if (foundDepth.HasValue)
                    return foundDepth;
            }

            return 0;
        }
    }
}
