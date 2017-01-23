using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormExample
{
    class Checker
    {

        public int[,] board;
        public int x, y, nx, ny;

        public Boolean turn, capture, cc;

        private int p1 = 0;
        private int p2 = 0;

        public Checker()
        {
            newGame();
        }

        public void newGame()
        {
            try
            {
                board = new int[8, 8];
            }
            catch (Exception)
            { }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 1 && j % 2 == 1)
                        board[j, i] = 3;
                    else if (i % 2 == 0 && j % 2 == 0)
                        board[j, i] = 3;
                }
            }
            for (int i = 7; i > 4; i--)
            {
                for (int j = 7; j > -1; j--)
                {
                    if (i % 2 == 1 && j % 2 == 1)
                        board[j, i] = 2;
                    else if (i % 2 == 0 && j % 2 == 0)
                        board[j, i] = 2;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0 && j % 2 == 1)
                        board[j, i] = -1;
                    else if (i % 2 == 1 && j % 2 == 0)
                        board[j, i] = -1;
                }
            }
        }

        public Boolean validMove(int x, int y, int nx, int ny)
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

        public Boolean canMove(int x, int y, int nx, int ny)
        {
            try
            {
                if (board[nx, ny] == -1 || board[x, y] == 0 || board[x, y] == -1)
                    return false;
                if (!turn)
                {
                    if (board[x, y] == 2)
                    {
                        if (y > ny)
                        {
                            if (y - 1 == ny)
                                if (x + 1 == nx || x - 1 == nx)
                                    if (board[nx, ny] == 0)
                                        return true;
                            if (y - 2 == ny)
                            {
                                if (x + 2 == nx || x - 2 == nx)
                                {
                                    if (x - 2 == nx)
                                        if (board[x - 1, y - 1] == 3 || board[x - 1, y - 1] == 6)
                                            if (board[nx, ny] == 0)
                                            {
                                                p1++;
                                                capture = true;
                                                return true;
                                            }
                                    if (x + 2 == nx)
                                        if (board[x + 1, y - 1] == 3 || board[x + 1, y - 1] == 6)
                                            if (board[nx, ny] == 0)
                                            {
                                                p1++;
                                                capture = true;
                                                return true;
                                            }
                                }
                            }
                        }
                    }
                    if (board[x, y] == 4)
                    {
                        if (y > ny)
                        {
                            if (y - 1 == ny)
                                if (x + 1 == nx || x - 1 == nx)
                                    if (board[nx, ny] == 0)
                                        return true;
                            if (y - 2 == ny)
                            {
                                if (x + 2 == nx || x - 2 == nx)
                                {
                                    if (x - 2 == nx)
                                        if (board[x - 1, y - 1] == 3 || board[x - 1, y - 1] == 6)
                                            if (board[nx, ny] == 0)
                                            {
                                                p1++;
                                                capture = true;
                                                return true;
                                            }
                                    if (x + 2 == nx)
                                        if (board[x + 1, y - 1] == 3 || board[x + 1, y - 1] == 6)
                                            if (board[nx, ny] == 0)
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
                                    if (board[nx, ny] == 0)
                                        return true;
                            if (y + 2 == ny)
                            {
                                if (x + 2 == nx || x - 2 == nx)
                                {
                                    if (x - 2 == nx)
                                        if (board[x - 1, y + 1] == 3 || board[x - 1, y + 1] == 6)
                                            if (board[nx, ny] == 0)
                                            {
                                                p1++;
                                                capture = true;
                                                return true;
                                            }
                                    if (x + 2 == nx)
                                        if (board[x + 1, y + 1] == 3 || board[x + 1, y + 1] == 6)
                                            if (board[nx, ny] == 0)
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
                    if (board[x, y] == 3)
                    {
                        if (y < ny)
                        {
                            if (y + 1 == ny)
                                if (x + 1 == nx || x - 1 == nx)
                                    if (board[nx, ny] == 0)
                                        return true;
                            if (y + 2 == ny)
                                if (x + 2 == nx || x - 2 == nx)
                                    if (x - 2 == nx)
                                        if (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 4)
                                            if (board[nx, ny] == 0)
                                            {
                                                p2++;
                                                capture = true;
                                                return true;
                                            }
                            if (x + 2 == nx)
                                if (board[x + 1, y + 1] == 2 || board[x + 1, y + 1] == 4)
                                    if (board[nx, ny] == 0)
                                    {
                                        p2++;
                                        capture = true;
                                        return true;
                                    }
                        }
                    }
                    if (board[x, y] == 6)
                    {
                        if (y < ny)
                        {
                            if (y + 1 == ny)
                                if (x + 1 == nx || x - 1 == nx)
                                    if (board[nx, ny] == 0)
                                        return true;
                            if (y + 2 == ny)
                            {
                                if (x + 2 == nx || x - 2 == nx)
                                {
                                    if (x > nx)
                                        if (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 4)
                                            if (board[nx, ny] == 0)
                                            {
                                                p2++;
                                                capture = true;
                                                return true;
                                            }
                                            else
                                        if (board[x + 1, y + 1] == 2 || board[x + 1, y + 1] == 4)
                                                if (board[nx, ny] == 0)
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
                                    if (board[nx, ny] == 0)
                                        return true;
                            if (y - 2 == ny)
                            {
                                if (x + 2 == nx || x - 2 == nx)
                                {
                                    if (x - 2 == nx)
                                        if (board[x - 1, y - 1] == 2 || board[x - 1, y - 1] == 4)
                                            if (board[nx, ny] == 0)
                                            {
                                                p2++;
                                                capture = true;
                                                return true;
                                            }
                                    if (x + 2 == nx)
                                        if (board[x + 1, y - 1] == 2 || board[x + 1, y - 1] == 4)
                                            if (board[nx, ny] == 0)
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
            catch
            {
                return false;
            }
            return false;
        }

        public void kingMe()
        {
            for (int i = 0; i <= 7; i++)
                if (board[i, 0] == 2)
                    board[i, 0] = 4;
            for (int j = 0; j <= 7; j++)
                if (board[j, 7] == 3)
                    board[j, 7] = 6;
        }

        public Boolean winner1()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (board[j, i] == 3 || board[j, i] == 6)
                        if (canMove(j, i))
                            return false;
            return true;
        }

        public Boolean winner2()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (board[j, i] == 2 || board[j, i] == 4)
                        if (canMove(j, i))
                            return false;
            return true;
        }

        public Boolean canCapture1(int x, int y)
        {
            if (!turn)
            {
                if (board[x, y] == 2)
                {
                    try
                    {
                        if (board[x - 1, y - 1] == 3 || board[x - 1, y - 1] == 6)
                            if (board[x - 2, y - 2] == 0)
                                return true;
                    }
                    catch
                    { }
                    try
                    {
                        if (board[x + 1, y - 1] == 3 || board[x + 1, y - 1] == 6)
                            if (board[x + 2, y - 2] == 0)
                                return true;
                    }
                    catch
                    { }
                }
                if (board[x, y] == 4)
                {
                    try
                    {
                        if (board[x - 1, y - 1] == 3 || board[x - 1, y - 1] == 6)
                            if (board[x - 2, y - 2] == 0)
                                return true;
                    }
                    catch
                    { }
                    try
                    {
                        if (board[x + 1, y - 1] == 3 || board[x + 1, y - 1] == 6)
                            if (board[x + 2, y - 2] == 0)
                                return true;
                    }
                    catch
                    { }
                    try
                    {
                        if (board[x - 1, y + 1] == 3 || board[x - 1, y + 1] == 6)
                            if (board[x - 2, y + 2] == 0)
                                return true;
                    }
                    catch
                    { }
                    try
                    {
                        if (board[x + 1, y + 1] == 3 || board[x + 1, y + 1] == 6)
                            if (board[x + 2, y + 2] == 0)
                                return true;
                    }
                    catch
                    { }
                }
            }
            if (turn)
            {
                if (board[x, y] == 3)
                {
                    try
                    {
                        if (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 4)
                            if (board[x - 2, y + 2] == 0)
                                return true;
                    }
                    catch
                    { }
                    try
                    {
                        if (board[x + 1, y + 1] == 2 || board[x + 1, y + 1] == 4)
                            if (board[x + 2, y + 2] == 0)
                                return true;
                    }
                    catch
                    { }
                }
                if (board[x, y] == 6)
                {
                    try
                    {
                        if (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 4)
                            if (board[x - 2, y + 2] == 0)
                                return true;
                    }
                    catch
                    { }
                    try
                    {
                        if (board[x + 1, y + 1] == 2 || board[x + 1, y + 1] == 4)
                            if (board[x + 2, y + 2] == 0)
                                return true;
                    }
                    catch
                    { }
                    try
                    {
                        if (board[x - 1, y - 1] == 2 || board[x - 1, y - 1] == 4)
                            if (board[x - 2, y - 2] == 0)
                                return true;
                    }
                    catch
                    { }
                    try
                    {
                        if (board[x + 1, y - 1] == 2 || board[x + 1, y - 1] == 4)
                            if (board[x + 2, y - 2] == 0)
                                return true;
                    }
                    catch
                    { }
                }
            }
            return false;
        }

        public Boolean canCapture2(int x, int y)
        {
            if (board[x, y] == 2)
            {
                try
                {
                    if (board[x - 1, y - 1] == 3 || board[x - 1, y - 1] == 6)
                        if (board[x - 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y - 1] == 3 || board[x + 1, y - 1] == 6)
                        if (board[x + 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
            }
            if (board[x, y] == 4)
            {
                try
                {
                    if (board[x - 1, y - 1] == 3 || board[x - 1, y - 1] == 6)
                        if (board[x - 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y - 1] == 3 || board[x + 1, y - 1] == 6)
                        if (board[x + 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x - 1, y + 1] == 3 || board[x - 1, y + 1] == 6)
                        if (board[x - 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y + 1] == 3 || board[x + 1, y + 1] == 6)
                        if (board[x + 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
            }
            if (board[x, y] == 3)
            {
                try
                {
                    if (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 4)
                        if (board[x - 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y + 1] == 2 || board[x + 1, y + 1] == 4)
                        if (board[x + 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
            }
            if (board[x, y] == 6)
            {
                try
                {
                    if (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 4)
                        if (board[x - 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y + 1] == 2 || board[x + 1, y + 1] == 4)
                        if (board[x + 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x - 1, y - 1] == 2 || board[x - 1, y - 1] == 4)
                        if (board[x - 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y - 1] == 2 || board[x + 1, y - 1] == 4)
                        if (board[x + 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
            }
            return false;
        }

        public Boolean canMove(int x, int y)
        {
            if (board[x, y] == 2)
            {
                try
                {
                    if (board[x - 1, y - 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y - 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x - 1, y - 1] == 3 || board[x - 1, y - 1] == 6)
                        if (board[x - 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y - 1] == 3 || board[x + 1, y - 1] == 6)
                        if (board[x + 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
            }
            if (board[x, y] == 4)
            {
                try
                {
                    if (board[x - 1, y - 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y - 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x - 1, y + 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y + 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x - 1, y - 1] == 3 || board[x - 1, y - 1] == 6)
                        if (board[x - 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y - 1] == 3 || board[x + 1, y - 1] == 6)
                        if (board[x + 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x - 1, y + 1] == 3 || board[x - 1, y + 1] == 6)
                        if (board[x - 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y + 1] == 3 || board[x + 1, y + 1] == 6)
                        if (board[x + 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
            }
            if (board[x, y] == 3)
            {
                try
                {
                    if (board[x - 1, y + 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y + 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 4)
                        if (board[x - 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y + 1] == 2 || board[x + 1, y + 1] == 4)
                        if (board[x + 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
            }
            if (board[x, y] == 6)
            {
                try
                {
                    if (board[x - 1, y + 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y + 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x - 1, y - 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y - 1] == 0)
                        return true;
                }
                catch
                { }
                try
                {
                    if (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 4)
                        if (board[x - 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y + 1] == 2 || board[x + 1, y + 1] == 4)
                        if (board[x + 2, y + 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x - 1, y - 1] == 2 || board[x - 1, y - 1] == 4)
                        if (board[x - 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
                try
                {
                    if (board[x + 1, y - 1] == 2 || board[x + 1, y - 1] == 4)
                        if (board[x + 2, y - 2] == 0)
                            return true;
                }
                catch
                { }
            }
            return false;
        }

        public void move(int x, int y, int nx, int ny)
        {
            board[nx, ny] = board[x, y];
            board[x, y] = 0;
            turn = !turn;
            if (capture)
            {
                if (x < nx)
                {
                    if (y < ny)
                        board[x + 1, y + 1] = 0;
                    else
                        board[x + 1, y - 1] = 0;
                }
                else
                {
                    if (y < ny)
                        board[x - 1, y + 1] = 0;
                    else
                        board[x - 1, y - 1] = 0;
                }
            }
            kingMe();
        }
    }
}