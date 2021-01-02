using System;
using Pajo.FourInARow.Engine;
using Pajo.FourInARow.Engine.Solver;

namespace Pajo.FourInARow.Client.Console
{
    class Program
    {
        private static Board _Board;
        private static BoardValue _PlayerRound;
        private static bool _YellowPlayComputer;
        private static bool _RedPlayComputer;

        static void Main(string[] args)
        {
            ResetGame();
            ConsoleKey key = ConsoleKey.Spacebar;
            while (_YellowPlayComputer && _PlayerRound == BoardValue.Yellow || _RedPlayComputer && _PlayerRound == BoardValue.Red || (key = System.Console.ReadKey().Key) != ConsoleKey.Escape)
            {
                if (_YellowPlayComputer && _PlayerRound == BoardValue.Yellow ||
                    _RedPlayComputer && _PlayerRound == BoardValue.Red)
                {
                    new MinMaxSolver().Play(_Board, _PlayerRound);
                    SetPlayerColorAndDrawBoard(_PlayerRound == BoardValue.Red ? BoardValue.Yellow : BoardValue.Red);
                    CheckBoardEndGame();
                    continue;
                }

                if (key == ConsoleKey.Y)
                {
                    SetPlayerColorAndDrawBoard(_PlayerRound);
                    _YellowPlayComputer = !_YellowPlayComputer;
                    continue;
                }

                if (key == ConsoleKey.R)
                {
                    SetPlayerColorAndDrawBoard(_PlayerRound);
                    _RedPlayComputer = !_RedPlayComputer;
                    continue;
                }

                bool added = false;
                if (key == ConsoleKey.NumPad1 || key == ConsoleKey.D1)
                    added = _Board.AddToColumn(1, _PlayerRound);
                else if (key == ConsoleKey.NumPad2 || key == ConsoleKey.D2)
                    added = _Board.AddToColumn(2, _PlayerRound);
                else if (key == ConsoleKey.NumPad3 || key == ConsoleKey.D3)
                    added = _Board.AddToColumn(3, _PlayerRound);
                else if (key == ConsoleKey.NumPad4 || key == ConsoleKey.D4)
                    added = _Board.AddToColumn(4, _PlayerRound);
                else if (key == ConsoleKey.NumPad5 || key == ConsoleKey.D5)
                    added = _Board.AddToColumn(5, _PlayerRound);
                else if (key == ConsoleKey.NumPad6 || key == ConsoleKey.D6)
                    added = _Board.AddToColumn(6, _PlayerRound);
                else if (key == ConsoleKey.NumPad7 || key == ConsoleKey.D7)
                    added = _Board.AddToColumn(7, _PlayerRound);
                if (!added)
                {
                    SetPlayerColorAndDrawBoard(_PlayerRound);
                    continue;
                }

                SetPlayerColorAndDrawBoard(_PlayerRound == BoardValue.Red ? BoardValue.Yellow : BoardValue.Red);
                CheckBoardEndGame();
            }

            System.Console.Clear();
        }

        private static void CheckBoardEndGame()
        {
            var winner = _Board.CheckWinner();
            if (winner != BoardValue.Empty)
                EndTextAndResetGame($"{winner} is a winner. Press ENTER to continue");

            if (_Board.EndGameWithoutWinner())
                EndTextAndResetGame("End of a game without any winner. Press ENTER to continue");
        }

        static void EndTextAndResetGame(string text)
        {
            System.Console.WriteLine();
            System.Console.WriteLine(text);
            _YellowPlayComputer = _RedPlayComputer = false;
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
            System.Console.WriteLine("Esc to exit. '1' - '7' to add coin to selected column.");
            System.Console.WriteLine("'Y' enable/disable Yellow computer player. IsEnabled: " + _YellowPlayComputer);
            System.Console.WriteLine("'R' enable/disable Red computer player. IsEnabled: " + _RedPlayComputer);

            _PlayerRound = p_BoardValue;
            System.Console.ForegroundColor = _PlayerRound == BoardValue.Red ? ConsoleColor.Red : ConsoleColor.Yellow;
            System.Console.WriteLine("|1|2|3|4|5|6|7|");

            var lastAdded = _Board.LastAddedColumnRow ?? new Tuple<int, int>(0, 0);
            for (var col = 1; col <= _Board.ColumnsCount; col++)
            for (var row = 1; row <= _Board.RowsCount; row++)
            {
                var val = _Board.GetValue(row, col);
                System.Console.ForegroundColor = ConsoleColor.White;
                if (col == 1)
                {
                    System.Console.SetCursorPosition(0, row + 3);
                    System.Console.Write("|");
                }
                if (val == BoardValue.Red)
                    System.Console.ForegroundColor = ConsoleColor.Red;
                if (val == BoardValue.Yellow)
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.SetCursorPosition((col - 1) * 2 + 1, row + 3);
                System.Console.BackgroundColor = lastAdded.Item1 == col && lastAdded.Item2 == row ? ConsoleColor.DarkGray : ConsoleColor.Black;
                System.Console.Write("o");
                System.Console.BackgroundColor = ConsoleColor.Black;
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write("|");
                
            } 
        }
    }
}
