using System;
using System.Collections.Generic;
using System.Linq;

namespace Pajo.FourInARow.Engine.Solver
{
    public class MinMaxSolver
    {
        public void Play(Board p_Board, BoardValue p_BoardValuePlayer)
        {
            var random = new Random();

            var moves = new List<Tuple<int, int>>();
            for (var i = 1; i <= p_Board.ColumnsCount; i++)
            {
                if (!p_Board.AddToColumn(i, p_BoardValuePlayer))
                    continue;
                moves.Add(Tuple.Create(i, MinMax(6, p_Board, p_BoardValuePlayer, false)));
                p_Board.RemoveFromColumn(i);
            }

            var maxMoveScore = moves.Max(t => t.Item2);
            var bestMoves = moves.Where(t => t.Item2 == maxMoveScore).ToList();
            p_Board.AddToColumn(bestMoves[random.Next(0, bestMoves.Count)].Item1, p_BoardValuePlayer);
        }

        private static int MinMax(int p_Depth, Board p_Board, BoardValue p_BoardValuePlayer, bool p_MaximizingPlayer)
        {
            var opponentPlayer = p_BoardValuePlayer == BoardValue.Red ? BoardValue.Yellow : BoardValue.Red;
            if (p_Depth <= 0)
                return 0;

            var winner = p_Board.CheckWinner();
            if (winner == p_BoardValuePlayer)
                return p_Depth;
            if (winner != BoardValue.Empty)
                return -p_Depth;
            if (p_Board.EndGameWithoutWinner())
                return 0;


            var bestValue = p_MaximizingPlayer ? -1 : 1;
            for (var i = 1; i <= p_Board.ColumnsCount; i++)
            {
                if (!p_Board.AddToColumn(i, p_MaximizingPlayer ? p_BoardValuePlayer : opponentPlayer))
                    continue;
                var v = MinMax(p_Depth - 1, p_Board, p_BoardValuePlayer, !p_MaximizingPlayer);
                bestValue = p_MaximizingPlayer ? Math.Max(bestValue, v) : Math.Min(bestValue, v);
                p_Board.RemoveFromColumn(i);
            }

            return bestValue;
        }
    }
}