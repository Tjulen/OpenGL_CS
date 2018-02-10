using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTest
{
    class Core
    {
        public static void Main()
        {
            OpenTest.Game game = new OpenTest.Game();
            game.Run(60.0);
        }
    }
}
