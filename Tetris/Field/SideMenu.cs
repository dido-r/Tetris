using System;
using Tetris.Models.Contract;

namespace Tetris.Core
{
    public class SideMenu
    {
        public void ScoreInfo(Controller controller)
        {
            Console.SetCursorPosition(15, 2);
            Console.WriteLine("SCORE");
            Console.SetCursorPosition(15, 3);
            Console.WriteLine(controller.Scores);
        }

        public void NextShapeInfo(IShape shape)
        {
            Console.SetCursorPosition(15, 8);
            Console.WriteLine("NEXT");
            Console.SetCursorPosition(15, 9);
            ClearShapeInfo();

            for (int i = 0; i < shape.Position[0].GetLength(0); i++)
            {
                for (int j = 0; j < shape.Position[0].GetLength(1); j++)
                {
                    if (shape.Position[0][i, j] == 1)
                    {
                        Console.SetCursorPosition(15 + j, 9 + i);
                        Console.Write('■');
                    }
                }
                Console.WriteLine();
            }
        }

        private void ClearShapeInfo()
        {
            for (int i = 9; i < 13; i++)
            {
                for (int j = 15; j < 19; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(" ");
                }
            }
        }
    }
}
