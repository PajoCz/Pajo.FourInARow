using System;
using Pajo.FourInARow.Engine;

namespace Pajo.FourInARow.Client.Console
{
    class Program
    {
        private static Board _Board;
        private static BoardValue _PlayerRound;
        static void Main(string[] args)
        {
            ResetGame();
            ConsoleKey key;
            while((key = System.Console.ReadKey().Key) != ConsoleKey.Escape)
            {
                if (key == ConsoleKey.NumPad1)
                    _Board.AddToColumn(1, _PlayerRound);
                else if (key == ConsoleKey.NumPad2)
                    _Board.AddToColumn(2, _PlayerRound);
                else if (key == ConsoleKey.NumPad3)
                    _Board.AddToColumn(3, _PlayerRound);
                else if (key == ConsoleKey.NumPad4)
                    _Board.AddToColumn(4, _PlayerRound);
                else if (key == ConsoleKey.NumPad5)
                    _Board.AddToColumn(5, _PlayerRound);
                else if (key == ConsoleKey.NumPad6)
                    _Board.AddToColumn(6, _PlayerRound);
                else if (key == ConsoleKey.NumPad7)
                    _Board.AddToColumn(7, _PlayerRound);
                
                SetPlayerColorAndDrawBoard(_PlayerRound == BoardValue.Red ? BoardValue.Yellow : BoardValue.Red);

                var winner = _Board.CheckWinner();
                if (winner != BoardValue.Empty)
                    EndTextAndResetGame($"{winner} is winner. Press ENTER to continue");

                if (_Board.EndGameWithoutWinner())
                    EndTextAndResetGame("End of game without winner. Press ENTER to continue");
            }
            System.Console.Clear();
        }

        static void EndTextAndResetGame(string text)
        {
            System.Console.WriteLine();
            System.Console.WriteLine(text);
            while (System.Console.ReadKey().Key != ConsoleKey.Enter)
            {
            }
            ResetGame();
        }

        static void ResetGame()
        {
            _Board = new Board();
            SetPlayerColorAndDrawBoard(BoardValue.Red);
        }


        private static void SetPlayerColorAndDrawBoard(BoardValue p_BoardValue)
        {
            System.Console.Clear();
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("Esc to exit. 1 - 7 to add coin to selected column");

            _PlayerRound = p_BoardValue;
            System.Console.ForegroundColor = _PlayerRound == BoardValue.Red ? ConsoleColor.Red : ConsoleColor.Yellow;
            System.Console.WriteLine("|1|2|3|4|5|6|7|");

            for (var col = 1; col <= _Board.ColumnsCount; col++)
            for (var row = 1; row <= _Board.RowsCount; row++)
            {
                var val = _Board.GetValue(row, col);
                System.Console.ForegroundColor = ConsoleColor.White;
                if (col == 1)
                {
                    System.Console.SetCursorPosition(0, row+1);
                    System.Console.Write("|");
                }
                if (val == BoardValue.Red)
                    System.Console.ForegroundColor = ConsoleColor.Red;
                if (val == BoardValue.Yellow)
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.SetCursorPosition((col-1)*2+1, row+1);
                System.Console.Write("o");
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write("|");
                
            } 
        }
    }
}
