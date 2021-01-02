using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            _Data[p_Row - 1, p_Column - 1] = p_Value;
        }

        public bool CanAddToColumn(int p_Column)
        {
            if (p_Column < 1 || p_Column > ColumnsCount)
                throw new ArgumentOutOfRangeException(nameof(p_Column));

            return GetValue(1, p_Column) == BoardValue.Empty;
        }

        public Tuple<int, int> LastAddedColumnRow { get; private set; }
        
        public bool AddToColumn(int p_Column, BoardValue p_Value)
        {
            if (p_Column < 1 || p_Column > ColumnsCount)
                throw new ArgumentOutOfRangeException(nameof(p_Column));

            for (int r = RowsCount; r > 0; r--)
            {
                if (GetValue(r, p_Column) == BoardValue.Empty)
                {
                    LastAddedColumnRow = new Tuple<int, int>(p_Column, r);
                    SetValue(r, p_Column, p_Value);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveFromColumn(int p_Column)
        {
            if (p_Column < 1 || p_Column > ColumnsCount)
                throw new ArgumentOutOfRangeException(nameof(p_Column));

            for (int r = 1; r <= RowsCount; r++)
            {
                if (GetValue(r, p_Column) != BoardValue.Empty)
                {
                    SetValue(r, p_Column, BoardValue.Empty);
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
            for (var c = 1; c <= ColumnsCount; c++)
            for (var r = 1; r <= RowsCount; r++)
            {
                var val = GetValue(r, c);
                if (val == BoardValue.Empty)
                    continue;

                var columns = c + SameForWin - 1 <= ColumnsCount;
                var rows = r + SameForWin - 1 <= RowsCount;

                if (!columns && !rows)
                    continue;

                var forwardDiagonal = columns && rows;
                var backwardDiagonal = columns && r - SameForWin >= 0;

                for (var k = 1; k < SameForWin; k++)
                {
                    columns = columns && val == GetValue(r, c + k);
                    rows = rows && val == GetValue(r + k, c);
                    forwardDiagonal = forwardDiagonal && val == GetValue(r + k, c + k);
                    backwardDiagonal = backwardDiagonal && val == GetValue(r - k, c + k);
                    if (!columns && !rows && !forwardDiagonal && !backwardDiagonal)
                        break;
                }

                if (columns || rows || forwardDiagonal || backwardDiagonal)
                    return val;
            }

            return BoardValue.Empty;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 1; row <= RowsCount; row++)
            {
                for (int col = 1; col <= ColumnsCount; col++)
                {
                    var val = GetValue(row, col);
                    if (val == BoardValue.Empty)
                        sb.Append("o");
                    else if (val == BoardValue.Red)
                        sb.Append("R");
                    else if (val == BoardValue.Yellow)
                        sb.Append("Y");
                    if (col < ColumnsCount)
                        sb.Append("|");
                }

                sb.Append("||");
            }

            return sb.ToString();
        }
    }
}