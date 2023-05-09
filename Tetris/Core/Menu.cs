using System;
using System.Collections.Generic;
using System.IO;

namespace Tetris.Core
{

    public class Menu
    {
        static int indexMainMenu = 0;

        public static void mainMenu(Engine engine)
        {
            List<string> menuItems = new List<string>()
            {
            "Play",
            "Highscore",
            "Exit"
            };

            Console.CursorVisible = false;

            while (true)
            {
                string selectedMenuItem = drawMainMenu(menuItems);

                if (selectedMenuItem == "Play")
                {
                    engine.Run();
                    break;
                }
                else if (selectedMenuItem == "Highscore")
                {
                    Console.Clear();
                    string textFilePath = @"..\..\..\points.txt";
                    Console.WriteLine("YOUR HIGHSCORE IS:");

                    try
                    {
                        using (StreamReader reader = new StreamReader(textFilePath))
                        {
                            Console.WriteLine(reader.ReadToEnd());
                        };
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(0);
                    }

                    Console.WriteLine("Press any key to continue...");
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                else if (selectedMenuItem == "Exit")
                {
                    Environment.Exit(0);
                }
            }
        }

        private static string drawMainMenu(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i == indexMainMenu)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (indexMainMenu == items.Count - 1) { }
                else { indexMainMenu++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (indexMainMenu <= 0) { }
                else { indexMainMenu--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[indexMainMenu];
            }
            else
            {
                Console.Clear();
                return "";
            }

            Console.SetCursorPosition(0, 0);
            return "";
        }
    }
}
