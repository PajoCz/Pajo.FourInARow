using System;
using System.Collections.Generic;
using System.Linq;

namespace Pajo.FourInARow.Engine
{
    public class Board
    {
        private readonly BoardValue[,] _Data;

        public readonly int ColumnsCount;
        public readonly int RowsCount;
        public readonly int SameForWin = 4;

        public Board(BoardValue[,] p_InitData)
        {
            _Data = p_InitData;
            RowsCount = p_InitData.GetLength(0);
            ColumnsCount = p_InitData.GetLength(1);
        }

        public Board(int p_ColumnsCount = 7, int p_RowsCount = 6)
        {
            ColumnsCount = p_ColumnsCount;
            RowsCount = p_RowsCount;
            _Data = new BoardValue[RowsCount, ColumnsCount];
            for (int r = 0; r < RowsCount; r++)
            for (int c = 0; c < ColumnsCount; c++)
            {
                _Data[r, c] = BoardValue.Empty;
            }
        }

        private bool TryGetValue(int p_Row, int p_Column, out BoardValue p_BoardValue)
        {
            p_BoardValue = BoardValue.Empty;
            if (p_Row < 1 || p_Row > RowsCount || p_Column < 1 || p_Column > ColumnsCount)
                return false;
            p_BoardValue = _Data[p_Row - 1, p_Column - 1];
            return true;
        }
        
        public BoardValue GetValue(int p_Row, int p_Column)
        {
            return _Data[p_Row - 1, p_Column - 1];
        }
        
        private void SetValue(int p_Row, int p_Column, BoardValue p_Value)
        {
            if (p_Column < 1 || p_Column > ColumnsCount)
                throw new ArgumentOutOfRangeException(nameof(p_Column));
            if (p_Row < 1 || p_Row > RowsCount)
                throw new ArgumentOutOfRangeException(nameof(p_Row));
            if (p_Value == BoardValue.Empty)
                throw new ArgumentOutOfRangeException(nameof(p_Value));
            _Data[p_Row - 1, p_Column - 1] = p_Value;
        }

        public bool AddToColumn(int p_Column, BoardValue p_Value)
        {
            if (p_Column < 1 || p_Column > ColumnsCount)
                throw new ArgumentOutOfRangeException(nameof(p_Column));

            for (int r = RowsCount; r > 0; r--)
            {
                if (GetValue(r, p_Column) == BoardValue.Empty)
                {
                    SetValue(r, p_Column, p_Value);
                    return true;
                }
            }

            return false;
        }

        public bool EndGameWithoutWinner()
        {
            for(int col = 1; col <= ColumnsCount; col++)
            for (int row = 1; row <= RowsCount; row++)
            {
                if (GetValue(row, col) == BoardValue.Empty)
                    return false;
            }

            return true;
        }

        public BoardValue CheckWinner()
        {
            //Check columns |
            for (int c = 1; c <= ColumnsCount; c++)
            {
                List<BoardValue> data = new List<BoardValue>(RowsCount);
                for(int r = 1; r <= RowsCount; r++)
                    data.Add(GetValue(r, c));
                var winner = Winner(data);
                if (winner != BoardValue.Empty)
                    return winner;
            }
            //Check rows -
            for(int r = 1; r <= RowsCount; r++)
            {
                List<BoardValue> data = new List<BoardValue>(RowsCount);
                for (int c = 1; c <= ColumnsCount; c++)
                    data.Add(GetValue(r, c));
                var winner = Winner(data);
                if (winner != BoardValue.Empty)
                    return winner;
            }

            //Check diagonals /
            for (int rStart = SameForWin; rStart <= RowsCount + ColumnsCount; rStart++)
            {
                List<BoardValue> data = new List<BoardValue>(RowsCount);
                for (int r = rStart; r >= 1; r--)
                {
                    BoardValue foundVal;
                    if (TryGetValue(r, rStart - r + 1, out foundVal))
                        data.Add(foundVal);
                }

                var winner = Winner(data);
                if (winner != BoardValue.Empty)
                    return winner;
            }

            //Check diagonals \
            for (int cStart = ColumnsCount - SameForWin + 1; cStart >= -RowsCount ; cStart--)
            {
                List<BoardValue> data = new List<BoardValue>(RowsCount);
                for (int c = cStart; c <= ColumnsCount; c++)
                {
                    BoardValue foundVal;
                    if (TryGetValue(c - cStart + 1, c, out foundVal))
                        data.Add(foundVal);
                }

                var winner = Winner(data);
                if (winner != BoardValue.Empty)
                    return winner;
            }

            return BoardValue.Empty;
        }

        private BoardValue Winner(List<BoardValue> p_Data)
        {
            if (p_Data == null || !p_Data.Any())
                return BoardValue.Empty;

            BoardValue lastVal = p_Data[0];
            int sameCount = lastVal != BoardValue.Empty ? 1 : 0;
            for (int i = 1; i < p_Data.Count; i++)
            {
                if (lastVal != p_Data[i])
                {
                    lastVal = p_Data[i];
                    sameCount = lastVal != BoardValue.Empty ? 1 : 0;
                }
                else if (lastVal != BoardValue.Empty)
                {
                    sameCount++;
                }

                if (sameCount == SameForWin)
                    return lastVal;
            }

            return BoardValue.Empty;
        }
    }
}