using Tetris.Core;

namespace Tetris
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var engine = new Engine();
            Menu.mainMenu(engine);
        }
    }
}
