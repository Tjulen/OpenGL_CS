

namespace OpenTest
{
    class Core
    {
        public static void Main()
        {
            Game game = new OpenTest.Game(1280, 1000);
            game.Run(30.0);
        }
    }
}
