using System;
using System.Threading;
using Tetris.Field;

namespace Tetris.Core
{
    public class Engine
    {
        private int[,] gameStat;
        private Grid grid;
        private SideMenu sideMenu;
        private Controller controller;

        public Engine()
        {
            gameStat = new int[22, 12];
            grid = new Grid();
            sideMenu = new SideMenu();
            controller = new Controller();
        }

        public void Run()
        {
            grid.DrawBorder();
            var figure = controller.GenerateShape();
            var nextShape = controller.GenerateShape();
            int startRow = 1;
            int startCol = 5;
            int currentPosition = 0;

            while (true)
            {
                Thread.Sleep(200);
                controller.Commands(figure, ref currentPosition, ref startCol, startRow, grid, gameStat);
                int row = startRow;
                int col = startCol;
                controller.InitializeShape(startCol, row, col, figure, currentPosition, grid, gameStat);
                Console.SetCursorPosition(0, 0);
                grid.Print(gameStat);

                if (grid.InRangeRow(startRow + figure.Position[currentPosition].GetLength(0)))
                {
                    if (controller.IsRowEmpty(startRow, startCol, figure, currentPosition, gameStat, grid))
                    {
                        controller.NullCurrentState(startRow, startCol, figure, currentPosition, gameStat, grid);
                        startRow++;
                    }
                    else
                    {
                        controller.PrintGameStat(startRow, startCol, figure, currentPosition, gameStat, grid);
                        figure = nextShape;
                        nextShape = controller.GenerateShape();
                        controller.SetUp(ref startRow, ref startCol, ref currentPosition);
                    }
                }
                else
                {
                    controller.PrintGameStat(startRow, startCol, figure, currentPosition, gameStat, grid);
                    figure = nextShape;
                    nextShape = controller.GenerateShape();
                    controller.SetUp(ref startRow, ref startCol, ref currentPosition);
                }
                controller.DeleteFullRows(gameStat);
                sideMenu.ScoreInfo(controller);
                sideMenu.NextShapeInfo(nextShape);
            }
        }
    }
}

