using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Tetris.Field;
using Tetris.Models;
using Tetris.Models.Contract;

namespace Tetris.Core
{
    public class Controller
    {
        private int scores;

        private readonly List<IShape> shape = new List<IShape>()
        {
                new Cube(),
                new Stick(),
                new LRight(),
                new LLeft(),
                new TBlock(),
                new ZRight(),
                new ZLeft(),
        };

        public int Scores => scores;

        public IShape GenerateShape()
        {
            var random = new Random();
            return shape[random.Next(6)];
        }

        public void SetUp(ref int startRow, ref int startCol, ref int currentPosition)
        {
            startRow = 1;
            startCol = 5;
            currentPosition = 0;
        }

        public void InitializeShape(int startCol, int row, int col, IShape figure, int currentPosition, Grid grid, int[,] gameStat)
        {
            for (int i = 0; i < figure.Position[currentPosition].GetLength(0); i++)
            {
                for (int j = 0; j < figure.Position[currentPosition].GetLength(1); j++)
                {
                    if (figure.Position[currentPosition][i, j] == 1)
                    {
                        grid.Field[row, col] = 1;
                        EndGame(row, col, grid, gameStat);
                    }
                    col++;
                }
                col = startCol;
                row++;
            }
        }

        public void NullCurrentState(int startRow, int startCol, IShape figure, int currentPosition, int[,] gameStat, Grid grid)
        {
            for (int i = startRow; i < startRow + figure.Position[currentPosition].GetLength(0); i++)
            {
                for (int j = startCol; j < startCol + figure.Position[currentPosition].GetLength(1); j++)
                {
                    if (grid.Field[i, j] == 1 && gameStat[i, j] == 0)
                    {
                        grid.Field[i, j] = 0;
                    }
                }
            }
        }

        public void PrintGameStat(int startRow, int startCol, IShape figure, int currentPosition, int[,] gameStat, Grid grid)
        {
            for (int i = startRow; i < startRow + figure.Position[currentPosition].GetLength(0); i++)
            {
                for (int j = startCol; j < startCol + figure.Position[currentPosition].GetLength(1); j++)
                {
                    if (grid.Field[i, j] == 1)
                    {
                        gameStat[i, j] = 1;
                        grid.Field[i, j] = 0;
                    }
                }
            }
        }

        private bool IsRowFull(int row, int[,] gameStat)
        {
            int fullCells = 0;

            for (int j = 1; j < 11; j++)
            {
                if (gameStat[row, j] == 1)
                {
                    fullCells++;
                }
            }

            if (fullCells == 10)
            {
                return true;
            }

            return false;
        }


        public void DeleteFullRows(int[,] gameStat)
        {
            for (int i = 21; i >= 1; i--)
            {
                if (IsRowFull(i, gameStat))
                {
                    scores += 50;

                    for (int j = i - 1; j >= 1; j--)
                    {
                        for (int k = 0; k < 10; k++)
                        {
                            gameStat[j + 1, k] = gameStat[j, k];
                        }
                    }
                }
            }
        }

        private bool CheckForCollision(int startRow, int startCol, IShape figure, int currentPosition, Grid grid, int[,] gameStat)
        {
            if (startRow + figure.Position[currentPosition].GetLength(0) - 1 > 21)
            {
                return true;
            }

            for (int i = startRow; i < startRow + figure.Position[currentPosition].GetLength(0); i++)
            {
                for (int j = startCol; j < startCol + figure.Position[currentPosition].GetLength(1); j++)
                {
                    grid.Field[i, j] = 1;

                    if (grid.Field[i, j] == 1 && gameStat[i, j] == 1)
                    {
                        grid.Field[i, j] = 0;
                        return true;
                    }
                    grid.Field[i, j] = 0;
                }
            }

            return false;
        }

        public bool IsRowEmpty(int startRow, int startCol, IShape figure, int currentPosition, int[,] gameStat, Grid grid)
        {
            for (int i = startRow; i < startRow + figure.Position[currentPosition].GetLength(0); i++)
            {
                for (int j = startCol; j < startCol + figure.Position[currentPosition].GetLength(1); j++)
                {
                    if (grid.Field[i, j] == 1 && gameStat[i + 1, j] == 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private void EndGame(int i, int j, Grid grid, int[,] gameStat)
        {
            if (grid.Field[i, j] == gameStat[i, j])
            {
                Console.Clear();
                string textFilePath = @"..\..\..\points.txt";
                string points = "";

                try
                {
                    using (StreamReader reader = new StreamReader(textFilePath))
                    {
                        points = reader.ReadToEnd();
                    };
                }
                catch (Exception)
                {
                    using (StreamWriter writer = new StreamWriter(textFilePath))
                    {
                        writer.WriteLine(scores);
                    };
                    Console.WriteLine("YOU HAVE NEW HIGHSCORE:");
                    Console.WriteLine(scores);
                    Console.WriteLine("PLAY AGAIN?: Y/N...");
                    var key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Y)
                    {
                        Console.Clear();
                        var engine = new Engine();
                        engine.Run();
                    }
                    else if (key == ConsoleKey.N)
                    {
                        Environment.Exit(0);
                    }
                }

                if (int.Parse(points) >= scores)
                {
                    Console.WriteLine("YOUR SCORE IS:");
                    Console.WriteLine(scores);
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(textFilePath))
                    {
                        writer.WriteLine(scores);
                    };
                    Console.WriteLine("YOU HAVE NEW HIGHSCORE:");
                    Console.WriteLine(scores);
                }

                Console.WriteLine("PLAY AGAIN?: Y/N...");
                var keys = Console.ReadKey().Key;

                if (keys == ConsoleKey.Y)
                {
                    Console.Clear();
                    var engine = new Engine();
                    engine.Run();
                }
                else if (keys == ConsoleKey.N)
                {
                    Environment.Exit(0);
                }
            }
        }


        public void Commands(IShape figure, ref int currentPosition, ref int startCol, int startRow, Grid grid, int[,] gameStat)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.Spacebar)
                {
                    if (figure.GetType().Name != "Cube")
                    {
                        if (currentPosition == figure.Position.Count - 1)
                        {
                            if (!grid.InRangeCol(startCol + figure.Position[0].GetLength(1)))
                            {
                                if (!CheckForCollision(startRow, 11 - figure.Position[0].GetLength(1), figure, 0, grid, gameStat))
                                {
                                    startCol = 11 - figure.Position[0].GetLength(1);
                                }
                            }

                            if (!CheckForCollision(startRow, startCol, figure, 0, grid, gameStat))
                            {
                                currentPosition = 0;
                            }
                        }
                        else
                        {
                            if (!grid.InRangeCol(startCol + figure.Position[currentPosition + 1].GetLength(1)))
                            {
                                if (!CheckForCollision(startRow, 11 - figure.Position[currentPosition + 1].GetLength(1), figure, currentPosition + 1, grid, gameStat))
                                {
                                    startCol = 11 - figure.Position[currentPosition + 1].GetLength(1);
                                }
                            }

                            if (!CheckForCollision(startRow, startCol, figure, currentPosition + 1, grid, gameStat))
                            {
                                currentPosition++;
                            }
                        }
                    }
                }
                if (key == ConsoleKey.DownArrow)
                {
                    //TO DO
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    if (grid.InRangeCol(startCol - 1) && !CheckForCollision(startRow, startCol - 1, figure, currentPosition, grid, gameStat))
                    {
                        startCol--;
                    }
                }
                if (key == ConsoleKey.RightArrow)
                {
                    if (grid.InRangeCol(startCol + figure.Position[currentPosition].GetLength(1)) && !CheckForCollision(startRow, startCol + 1, figure, currentPosition, grid, gameStat))
                    {
                        startCol++;
                    }
                }
                if (key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Environment.Exit(0);
                }
            }
        }
    }
}
