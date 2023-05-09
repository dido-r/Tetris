using System;

namespace Tetris.Field
{
    public class Grid
    {
        private readonly int[,] grid;
        public Grid()
        {
            grid = new int[22, 12];
            Console.CursorVisible = false;
        }
        public int[,] Field => grid;

        public void DrawBorder()
        {
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            grid[i, j] = 3;
                        }
                        else if (j == 11)
                        {
                            grid[i, j] = 4;
                        }
                        else
                        {
                            grid[i, j] = 5;
                        }
                    }
                    else if (i == 21)
                    {
                        if (j == 0)
                        {
                            grid[i, j] = 6;
                        }
                        else if (j == 11)
                        {
                            grid[i, j] = 7;
                        }
                        else
                        {
                            grid[i, j] = 5;
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            grid[i, j] = 8;
                        }
                        if (j == 11)
                        {
                            grid[i, j] = 9;
                        }
                    }
                }
            }
        }

        public void Print(int[,] gameStat)
        {
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (gameStat[i, j] == 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write('■');
                    }
                    if (grid[i, j] == 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write('■');
                    }
                    if (grid[i, j] == 0 && gameStat[i, j] == 0)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(" ");
                    }
                    if (grid[i, j] == 3)
                    {
                        Console.Write("╔");
                    }
                    if (grid[i, j] == 4)
                    {
                        Console.Write("╗");
                    }
                    if (grid[i, j] == 5)
                    {
                        Console.Write("═");
                    }
                    if (grid[i, j] == 6)
                    {
                        Console.Write("╚");
                    }
                    if (grid[i, j] == 7)
                    {
                        Console.Write("╝");
                    }
                    if (grid[i, j] == 8)
                    {
                        Console.Write("║");
                    }
                    if (grid[i, j] == 9)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("║");
                    }
                }
                Console.WriteLine();
            }
        }
        public bool InRangeRow(int row)
        {
            return row >= 1 && row < 21;
        }

        public bool InRangeCol(int col)
        {
            return col >= 1 && col < 11;
        }
    }
}
