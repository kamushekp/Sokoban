using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Game.NMapCreator;

namespace Game
{
    static class Program
    {
        static void Main()
        {
            var game = new Game.Logic.Game();
            Application.Run(new Game.GUI.GameWindow(game));
        }
    }
}
