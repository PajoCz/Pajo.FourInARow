using System;
using NUnit.Framework;

namespace Pajo.FourInARow.Engine.Test
{
    public class BoardTests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void AddToColumn_ArgumentOutOfRangeException()
        {
            Board board = new Board();
            Assert.Throws<ArgumentOutOfRangeException>(() => board.AddToColumn(0, BoardValue.Red));
            Assert.Throws<ArgumentOutOfRangeException>(() => board.AddToColumn(board.ColumnsCount+1, BoardValue.Red));
        }

        [Test]
        public void AddToColumn_CheckOverflow_Exception()
        {
            Board board = new Board();
            for (int row = 1; row <= board.RowsCount; row++)
            {
                Assert.AreEqual(true, board.AddToColumn(1, BoardValue.Red));
            }
            Assert.AreEqual(false, board.AddToColumn(1, BoardValue.Red));
        }

        [Test]
        public void AddToColumn_CheckWinnerInColumn()
        {
            Board board = new Board();
            for (int row = 1; row <= board.RowsCount; row++)
            {
                board.AddToColumn(1, BoardValue.Red);
                var winner = board.CheckWinner();
                Assert.AreEqual(row < 4 ? BoardValue.Empty : BoardValue.Red, winner);
            }

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
            });
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Yellow, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
            });
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Yellow },
            });
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Yellow, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Yellow, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
            });
            Assert.AreEqual(BoardValue.Empty, board.CheckWinner());
        }

        [Test]
        public void AddToColumn_CheckWinnerInRow()
        {
            Board board = new Board();
            for (int col = 1; col <= board.ColumnsCount; col++)
            {
                board.AddToColumn(col, BoardValue.Red);
                var winner = board.CheckWinner();
                Assert.AreEqual(col < 4 ? BoardValue.Empty : BoardValue.Red, winner);
            }

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Red, BoardValue.Red, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
            });
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Red, BoardValue.Red, BoardValue.Red, BoardValue.Red, BoardValue.Empty, BoardValue.Empty },
            });
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Red, BoardValue.Red, BoardValue.Red, BoardValue.Empty },
            });
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Red, BoardValue.Red, BoardValue.Red },
            });
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Red, BoardValue.Red, BoardValue.Empty },
            });
            Assert.AreEqual(BoardValue.Empty, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Yellow, BoardValue.Yellow, BoardValue.Yellow, BoardValue.Yellow, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Yellow, BoardValue.Red, BoardValue.Yellow, BoardValue.Red, BoardValue.Empty },
            });
            Assert.AreEqual(BoardValue.Yellow, board.CheckWinner());

        }

        [Test]
        public void AddToColumn_CheckWinnerInDiagonal()
        {
            //Diagonals /
            Board board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
            }); 
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
            }); 
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
            }); 
            Assert.AreEqual(BoardValue.Empty, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
            }); 
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            //Diagonals \
            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
            }); 
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red },
            }); 
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

            board = new Board(new[,]
            {
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Red },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
                {BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty, BoardValue.Empty },
            }); 
            Assert.AreEqual(BoardValue.Red, board.CheckWinner());

        }
    }
}