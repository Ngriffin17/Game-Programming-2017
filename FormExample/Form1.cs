using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormExample
{
    public partial class Form1 : Form
    {
        int x;
        int y;
        int cellSize;
        int margin=10;
        int row = -1;
        int col = -1;
        int win = 0;

        Boolean selected,win1,win2;

        Checker game = new Checker();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            UpdateSize();
        }

        private void UpdateSize()
        {
            cellSize = (Math.Min(ClientSize.Width, ClientSize.Height) - 2 * margin)/8;
            if(ClientSize.Width> ClientSize.Height)
            {
                x = (ClientSize.Width - 8 * cellSize) / 2;
                y = margin;
            }
            else
            {
                x = margin;
                y= (ClientSize.Height - 8 * cellSize) / 2;
            }
        }

        protected override void OnResize(EventArgs e)
        {

            base.OnResize(e);
            UpdateSize();
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!game.winner1() && !game.winner2())
            {
                game.capture = false;
                if (selected)
                {
                    int nx = (int)Math.Floor((e.X - x) * 1.0 / cellSize);
                    int ny = (int)Math.Floor((e.Y - y) * 1.0 / cellSize);
                    game.nx = nx;
                    game.ny = ny;
                    selected = !selected;
                    if (game.validMove(game.x, game.y, game.nx, game.ny))
                    {
                        game.move(game.x, game.y, game.nx, game.ny);
                        if (game.capture)
                            if (game.canCapture2(game.nx, game.ny))
                            {
                                game.turn = !game.turn;
                            }
                    }
                    else
                    {
                        selected = false;
                    }
                }
                else
                {
                    int nx = (int)Math.Floor((e.X - x) * 1.0 / cellSize);
                    int ny = (int)Math.Floor((e.Y - y) * 1.0 / cellSize);
                    game.x = nx;
                    game.y = ny;
                    selected = !selected;
                }
                game.capture = false;
            }
            else if (game.winner1())
            {
                win1 = true;
                win++;
            }
            else if (game.winner2())
            {
                win2 = true;
                win++;
            }
            if (win>1)
            {
                game = new Checker();
                win = 0;
                win1 = false;
                win2 = false;
            }
            if(selected)
            {
                col = (int)Math.Floor((e.X - x) * 1.0 / cellSize);
                row = (int)Math.Floor((e.Y - y) * 1.0 / cellSize);
            }
            else
            {
                col = -1;
                row = -1;
            }
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Rectangle rect = new Rectangle(x + i * cellSize, y + j * cellSize, cellSize, cellSize);
                    Rectangle rect2 = new Rectangle(x + i * cellSize + (cellSize / 16), y + j * cellSize + (cellSize / 16), cellSize - cellSize / 8, cellSize - cellSize / 8);
                    Rectangle rect3 = new Rectangle(x + i * cellSize + (cellSize / 3), y + j * cellSize + (cellSize / 3), cellSize - 2 * cellSize / 3, cellSize - 2 * cellSize / 3);
                    Brush b = new SolidBrush(Color.FromArgb(100, Color.LightGoldenrodYellow));
                    Brush b2 = new SolidBrush(Color.FromArgb(100, Color.IndianRed));
                    Brush b3 = new SolidBrush(Color.FromArgb(75, Color.Black));
                    System.Drawing.Font font = new System.Drawing.Font("Ubuntu", cellSize * 8 * 18 / 96);
                    e.Graphics.DrawRectangle(Pens.DimGray, rect);
                    if (i % 2 == 1 && j % 2 == 1) e.Graphics.FillRectangle(Brushes.Gray, rect);
                    if (i % 2 != 1 && j % 2 != 1) e.Graphics.FillRectangle(Brushes.Gray, rect);
                    if (game.board[i, j] == 2 || game.board[i, j] == 4) e.Graphics.FillEllipse(Brushes.Black, rect2);
                    if (game.board[i, j] == 4) e.Graphics.FillRectangle(Brushes.Red, rect3);
                    if (game.board[i, j] == 3 || game.board[i, j] == 6) e.Graphics.FillEllipse(Brushes.Red, rect2);
                    if (game.board[i, j] == 6) e.Graphics.FillRectangle(Brushes.Black, rect3);
                    if (i == col && j == row) e.Graphics.FillRectangle(b, rect);
                    if (win1)
                    {
                        e.Graphics.FillRectangle(b3, rect);
                        e.Graphics.DrawString("Player 1", font, Brushes.Black, x, y + cellSize);
                        e.Graphics.DrawString("Wins", font, Brushes.Black, x, y + cellSize * 3);
                    }
                    if (win2)
                    {
                        e.Graphics.FillRectangle(b2, rect);
                        e.Graphics.DrawString("Player 2", font, Brushes.Black, x, y + cellSize);
                        e.Graphics.DrawString("Wins", font, Brushes.Black, x, y + cellSize * 3);
                    }
                }
        }

    }
    
}
