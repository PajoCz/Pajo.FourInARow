using System;
using System.Drawing;
using System.Windows.Forms;
using Pajo.FourInARow.Engine;

namespace Pajo.FourInARow.Client.WinForms
{
    public partial class MainForm : Form
    {
        private Board _Board;
        private BoardValue _PlayerRound;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetPlayerColor(BoardValue.Red);
            ResetGame();
        }

        private void SetPlayerColor(BoardValue p_BoardValue)
        {
            _PlayerRound = p_BoardValue;
            button1.BackColor = button2.BackColor = button3.BackColor = button4.BackColor = button5.BackColor =
                button6.BackColor = button7.BackColor = _PlayerRound == BoardValue.Red ? Color.Red : Color.Yellow;
        }

        private void button_Click(object sender, EventArgs e)
        {
            var column = int.Parse((sender as Button).Tag.ToString());
            if (!_Board.AddToColumn(column, _PlayerRound))
                return;

            DrawBoard();
            var winner = _Board.CheckWinner();
            if (winner != BoardValue.Empty)
            {
                MessageBox.Show($"{winner} is winner");
                ResetGame();
            }

            if (_Board.EndGameWithoutWinner())
            {
                MessageBox.Show("End of game without winner");
                ResetGame();
            }

            SetPlayerColor(_PlayerRound == BoardValue.Red ? BoardValue.Yellow : BoardValue.Red);
        }

        private void ResetGame()
        {
            _Board = new Board();
            DrawBoard();
        }

        private void DrawBoard()
        {
            var bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var gr = Graphics.FromImage(bmp);
            var coinWidth = pictureBox1.Width / _Board.ColumnsCount;
            var coinHeight = pictureBox1.Height / _Board.RowsCount;
            for (var col = 1; col <= _Board.ColumnsCount; col++)
            for (var row = 1; row <= _Board.RowsCount; row++)
            {
                var val = _Board.GetValue(row, col);
                var br = new SolidBrush(Color.White);
                if (val == BoardValue.Red)
                    br = new SolidBrush(Color.Red);
                if (val == BoardValue.Yellow)
                    br = new SolidBrush(Color.Yellow);
                gr.FillEllipse(br, (col - 1) * coinWidth, (row - 1) * coinHeight, coinWidth, coinHeight);
            }

            pictureBox1.Image = bmp;
        }
    }
}