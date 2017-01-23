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


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            UpdateSize();
        }

        private void UpdateSize()
        {
            cellSize = (Math.Min(ClientSize.Width, ClientSize.Height) - 2 * margin)/3;
            if(ClientSize.Width> ClientSize.Height)
            {
                x = (ClientSize.Width - 3 * cellSize) / 2;
                y = margin;
            }
            else
            {
                x = margin;
                y= (ClientSize.Height - 3 * cellSize) / 2;
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
            col = (int)Math.Floor((e.X - x)*1.0 / cellSize);
            row = (int)Math.Floor((e.Y - y)*1.0 / cellSize);
            Refresh();
            //base.OnMouseDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            //base.OnPaint(e);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    Rectangle rect = new Rectangle(x + i * cellSize, y + j * cellSize, cellSize, cellSize);
                    if (i == col && j == row) e.Graphics.FillRectangle(Brushes.Yellow, rect);
                    e.Graphics.DrawRectangle(Pens.Chocolate,rect);
                    System.Drawing.Font font = new System.Drawing.Font("Ubuntu", cellSize*3 * 72 / 96 / 4);
                    e.Graphics.DrawString("X", font, Brushes.Black, x + cellSize / 8, y + cellSize / 8);
                }
        }
        
        int[][] board;
        int x, y, nx, ny;
        Boolean turn, capture, cc;

        private int p1 = 0;
        private int p2 = 0;

        public Checkers()
        {
            newGame();
        }

        /**
         * Creates a new game
         */

        public void newGame()
        {
            try
            {
                board = new int[8][8];
            }
            catch (Exception e)
            { }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 1 && j % 2 == 1)
                        board[j][i] = 3;
                    else if (i % 2 == 0 && j % 2 == 0)
                        board[j][i] = 3;
                }
            }
            for (int i = 7; i > 4; i--)
            {
                for (int j = 7; j > -1; j--)
                {
                    if (i % 2 == 1 && j % 2 == 1)
                        board[j][i] = 2;
                    else if (i % 2 == 0 && j % 2 == 0)
                        board[j][i] = 2;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0 && j % 2 == 1)
                        board[j][i] = -1;
                    else if (i % 2 == 1 && j % 2 == 0)
                        board[j][i] = -1;
                }
            }
        }

        /**
         * Checks the rules of checkers to see if your move is valid, forces captures
         * 
         * @param x x coordinate of the current location
         * @param y y coordinate of the current location
         * @param nx x coordinate of the new location
         * @param ny y coordinate of the new location
         */

        public boolean validMove(int x, int y, int nx, int ny)
        {
            cc = false;
            this.x = x;
            this.y = y;
            this.nx = nx;
            this.ny = ny;
            if (canMove(x, y, nx, ny))
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (canCapture1(i, j))
                            cc = true;
                if (cc)
                {
                    if (capture)
                        return true;
                }
                else
                    return true;
            }
            return false;
        }

        /**
         * Checks to see if the origin and the destination are legal moves
         * 
         * @param x x coordinate of the current location
         * @param y y coordinate of the current location
         * @param nx x coordinate of the new location
         * @param ny y coordinate of the new location
         */

        public boolean canMove(int x, int y, int nx, int ny)
        {
            try
            {
                if (board[nx][ny] == -1 || board[x][y] == 0 || board[x][y] == -1)
                    return false;
                if (!turn)
                {
                    if (board[x][y] == 2)
                    {
                        if (y > ny)
                        {
                            if (y - 1 == ny)
                                if (x + 1 == nx || x - 1 == nx)
                                    if (board[nx][ny] == 0)
                                        return true;
                            if (y - 2 == ny)
                            {
                                if (x + 2 == nx || x - 2 == nx)
                                {
                                    if (x - 2 == nx)
                                        if (board[x - 1][y - 1] == 3 || board[x - 1][y - 1] == 6)
                                            if (board[nx][ny] == 0)
                                            {
                                                p1++;
                                                capture = true;
                                                return true;
                                            }
                                    if (x + 2 == nx)
                                        if (board[x + 1][y - 1] == 3 || board[x + 1][y - 1] == 6)
                                            if (board[nx][ny] == 0)
                                            {
                                                p1++;
                                                capture = true;
                                                return true;
                                            }
                                }
                            }
                        }
                    }
                    if (board[x][y] == 4)
                    {
                        if (y > ny)
                        {
                            if (y - 1 == ny)
                                if (x + 1 == nx || x - 1 == nx)
                                    if (board[nx][ny] == 0)
                                        return true;
                            if (y - 2 == ny)
                            {
                                if (x + 2 == nx || x - 2 == nx)
                                {
                                    if (x - 2 == nx)
                                        if (board[x - 1][y - 1] == 3 || board[x - 1][y - 1] == 6)
                                            if (board[nx][ny] == 0)
                                            {
                                                p1++;
                                                capture = true;
                                                return true;
                                            }
                                    if (x + 2 == nx)
                                        if (board[x + 1][y - 1] == 3 || board[x + 1][y - 1] == 6)
                                            if (board[nx][ny] == 0)
                                            {
                                                p1++;
                                                capture = true;
                                                return true;
                                            }
                                }
                            }
                        }
                        else
                        {
                            if (y + 1 == ny)
                                if (x + 1 == nx || x - 1 == nx)
                                    if (board[nx][ny] == 0)
                                        return true;
                            if (y + 2 == ny)
                            {
                                if (x + 2 == nx || x - 2 == nx)
                                {
                                    if (x - 2 == nx)
                                        if (board[x - 1][y + 1] == 3 || board[x - 1][y + 1] == 6)
                                            if (board[nx][ny] == 0)
                                            {
                                                p1++;
                                                capture = true;
                                                return true;
                                            }
                                    if (x + 2 == nx)
                                        if (board[x + 1][y + 1] == 3 || board[x + 1][y + 1] == 6)
                                            if (board[nx][ny] == 0)
                                            {
                                                p1++;
                                                capture = true;
                                                return true;
                                            }
                                }
                            }
                        }
                    }
                }
                if (turn)
                {
                    if (board[x][y] == 3)
                    {
                        if (y < ny)
                        {
                            if (y + 1 == ny)
                                if (x + 1 == nx || x - 1 == nx)
                                    if (board[nx][ny] == 0)
                                        return true;
                            if (y + 2 == ny)
                                if (x + 2 == nx || x - 2 == nx)
                                    if (x - 2 == nx)
                                        if (board[x - 1][y + 1] == 2 || board[x - 1][y + 1] == 4)
                                            if (board[nx][ny] == 0)
                                            {
                                                p2++;
                                                capture = true;
                                                return true;
                                            }
                            if (x + 2 == nx)
                                if (board[x + 1][y + 1] == 2 || board[x + 1][y + 1] == 4)
                                    if (board[nx][ny] == 0)
                                    {
                                        p2++;
                                        capture = true;
                                        return true;
                                    }
                        }
                    }
                    if (board[x][y] == 6)
                    {
                        if (y < ny)
                        {
                            if (y + 1 == ny)
                                if (x + 1 == nx || x - 1 == nx)
                                    if (board[nx][ny] == 0)
                                        return true;
                            if (y + 2 == ny)
                            {
                                if (x + 2 == nx || x - 2 == nx)
                                {
                                    if (x > nx)
                                        if (board[x - 1][y + 1] == 2 || board[x - 1][y + 1] == 4)
                                            if (board[nx][ny] == 0)
                                            {
                                                p2++;
                                                capture = true;
                                                return true;
                                            }
                                            else
                                        if (board[x + 1][y + 1] == 2 || board[x + 1][y + 1] == 4)
                                                if (board[nx][ny] == 0)
                                                {
                                                    p2++;
                                                    capture = true;
                                                    return true;
                                                }
                                }
                            }
                        }
                        if (y > ny)
                        {
                            if (y - 1 == ny)
                                if (x + 1 == nx || x - 1 == nx)
                                    if (board[nx][ny] == 0)
                                        return true;
                            if (y - 2 == ny)
                            {
                                if (x + 2 == nx || x - 2 == nx)
                                {
                                    if (x - 2 == nx)
                                        if (board[x - 1][y - 1] == 2 || board[x - 1][y - 1] == 4)
                                            if (board[nx][ny] == 0)
                                            {
                                                p2++;
                                                capture = true;
                                                return true;
                                            }
                                    if (x + 2 == nx)
                                        if (board[x + 1][y - 1] == 2 || board[x + 1][y - 1] == 4)
                                            if (board[nx][ny] == 0)
                                            {
                                                p2++;
                                                capture = true;
                                                return true;
                                            }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        /**
         * Kings the pieces at each end
         */

        public void kingMe()
        {
            for (int i = 0; i <= 7; i++)
                if (board[i][0] == 2)
                    board[i][0] = 4;
            for (int j = 0; j <= 7; j++)
                if (board[j][7] == 3)
                    board[j][7] = 6;
        }

        /**
         * Checks to see if player 1 won
         */

        public boolean winner1()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (board[j][i] == 3 || board[j][i] == 6)
                        if (canMove(j, i))
                            return false;
            return true;
        }

        /**
         * Checks to see if player 2 won
         */

        public boolean winner2()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (board[j][i] == 2 || board[j][i] == 4)
                        if (canMove(j, i))
                            return false;
            return true;
        }

        /**
         * Checks to see if a piece has an available capture based on turn
         * 
         * @param x x coordinate of the current location
         * @param y y coordinate of the current location
         */

        public boolean canCapture1(int x, int y)
        {
            if (!turn)
            {
                if (board[x][y] == 2)
                {
                    try
                    {
                        if (board[x - 1][y - 1] == 3 || board[x - 1][y - 1] == 6)
                            if (board[x - 2][y - 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        if (board[x + 1][y - 1] == 3 || board[x + 1][y - 1] == 6)
                            if (board[x + 2][y - 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                }
                if (board[x][y] == 4)
                {
                    try
                    {
                        if (board[x - 1][y - 1] == 3 || board[x - 1][y - 1] == 6)
                            if (board[x - 2][y - 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        if (board[x + 1][y - 1] == 3 || board[x + 1][y - 1] == 6)
                            if (board[x + 2][y - 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        if (board[x - 1][y + 1] == 3 || board[x - 1][y + 1] == 6)
                            if (board[x - 2][y + 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        if (board[x + 1][y + 1] == 3 || board[x + 1][y + 1] == 6)
                            if (board[x + 2][y + 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                }
            }
            if (turn)
            {
                if (board[x][y] == 3)
                {
                    try
                    {
                        if (board[x - 1][y + 1] == 2 || board[x - 1][y + 1] == 4)
                            if (board[x - 2][y + 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        if (board[x + 1][y + 1] == 2 || board[x + 1][y + 1] == 4)
                            if (board[x + 2][y + 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                }
                if (board[x][y] == 6)
                {
                    try
                    {
                        if (board[x - 1][y + 1] == 2 || board[x - 1][y + 1] == 4)
                            if (board[x - 2][y + 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        if (board[x + 1][y + 1] == 2 || board[x + 1][y + 1] == 4)
                            if (board[x + 2][y + 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        if (board[x - 1][y - 1] == 2 || board[x - 1][y - 1] == 4)
                            if (board[x - 2][y - 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        if (board[x + 1][y - 1] == 2 || board[x + 1][y - 1] == 4)
                            if (board[x + 2][y - 2] == 0)
                                return true;
                    }
                    catch (Exception e)
                    { }
                }
            }
            return false;
        }

        /**
         * Checks to see if the piece has an available capture
         * 
         * @param x x coordinate of the current location
         * @param y y coordinate of the current location
         */

        public boolean canCapture2(int x, int y)
        {
            if (board[x][y] == 2)
            {
                try
                {
                    if (board[x - 1][y - 1] == 3 || board[x - 1][y - 1] == 6)
                        if (board[x - 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y - 1] == 3 || board[x + 1][y - 1] == 6)
                        if (board[x + 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
            }
            if (board[x][y] == 4)
            {
                try
                {
                    if (board[x - 1][y - 1] == 3 || board[x - 1][y - 1] == 6)
                        if (board[x - 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y - 1] == 3 || board[x + 1][y - 1] == 6)
                        if (board[x + 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x - 1][y + 1] == 3 || board[x - 1][y + 1] == 6)
                        if (board[x - 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y + 1] == 3 || board[x + 1][y + 1] == 6)
                        if (board[x + 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
            }
            if (board[x][y] == 3)
            {
                try
                {
                    if (board[x - 1][y + 1] == 2 || board[x - 1][y + 1] == 4)
                        if (board[x - 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y + 1] == 2 || board[x + 1][y + 1] == 4)
                        if (board[x + 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
            }
            if (board[x][y] == 6)
            {
                try
                {
                    if (board[x - 1][y + 1] == 2 || board[x - 1][y + 1] == 4)
                        if (board[x - 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y + 1] == 2 || board[x + 1][y + 1] == 4)
                        if (board[x + 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x - 1][y - 1] == 2 || board[x - 1][y - 1] == 4)
                        if (board[x - 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y - 1] == 2 || board[x + 1][y - 1] == 4)
                        if (board[x + 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
            }
            return false;
        }

        /**
         * Checks the position to see if the piece has an available move
         * 
         * @param x x coordinate of the current location
         * @param y y coordinate of the current location
         */

        public boolean canMove(int x, int y)
        {
            if (board[x][y] == 2)
            {
                try
                {
                    if (board[x - 1][y - 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y - 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x - 1][y - 1] == 3 || board[x - 1][y - 1] == 6)
                        if (board[x - 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y - 1] == 3 || board[x + 1][y - 1] == 6)
                        if (board[x + 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
            }
            if (board[x][y] == 4)
            {
                try
                {
                    if (board[x - 1][y - 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y - 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x - 1][y + 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y + 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x - 1][y - 1] == 3 || board[x - 1][y - 1] == 6)
                        if (board[x - 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y - 1] == 3 || board[x + 1][y - 1] == 6)
                        if (board[x + 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x - 1][y + 1] == 3 || board[x - 1][y + 1] == 6)
                        if (board[x - 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y + 1] == 3 || board[x + 1][y + 1] == 6)
                        if (board[x + 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
            }
            if (board[x][y] == 3)
            {
                try
                {
                    if (board[x - 1][y + 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y + 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x - 1][y + 1] == 2 || board[x - 1][y + 1] == 4)
                        if (board[x - 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y + 1] == 2 || board[x + 1][y + 1] == 4)
                        if (board[x + 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
            }
            if (board[x][y] == 6)
            {
                try
                {
                    if (board[x - 1][y + 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y + 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x - 1][y - 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y - 1] == 0)
                        return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x - 1][y + 1] == 2 || board[x - 1][y + 1] == 4)
                        if (board[x - 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y + 1] == 2 || board[x + 1][y + 1] == 4)
                        if (board[x + 2][y + 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x - 1][y - 1] == 2 || board[x - 1][y - 1] == 4)
                        if (board[x - 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
                try
                {
                    if (board[x + 1][y - 1] == 2 || board[x + 1][y - 1] == 4)
                        if (board[x + 2][y - 2] == 0)
                            return true;
                }
                catch (Exception e)
                { }
            }
            return false;
        }

        /**
         * Moves the piece, also captures if available
         * 
         * @param x x coordinate of the current location
         * @param y y coordinate of the current location
         * @param nx x coordinate of the new location
         * @param ny y coordinate of the new location
         */

        public void move(int x, int y, int nx, int ny)
        {
            board[nx][ny] = board[x][y];
            board[x][y] = 0;
            turn = !turn;
            if (capture)
            {
                if (x < nx)
                {
                    if (y < ny)
                        board[x + 1][y + 1] = 0;
                    else
                        board[x + 1][y - 1] = 0;
                }
                else
                {
                    if (y < ny)
                        board[x - 1][y + 1] = 0;
                    else
                        board[x - 1][y - 1] = 0;
                }
            }
            kingMe();
        }

        /**
         * Prints the board
         */

        public void print()
        {
            System.out.print("   0 1 2 3 4 5 6 7");
            for (int j = 0; j < 8; j++)
            {
                System.out.print("\n" + (j) + " ");
                for (int i = 0; i < 8; i++)
                {
                    if (board[i][j] == 3)
                        System.out.print("[]");
                    if (board[i][j] == 6)
                        System.out.print("][");
                    if (board[i][j] == 2)
                        System.out.print("()");
                    if (board[i][j] == 4)
                        System.out.print(")(");
                    if (board[i][j] == -1)
                        System.out.print("  ");
                    if (board[i][j] == 0)
                        System.out.print("__");
                }
            }
            System.out.print("\n");
        }

    }
    
}
