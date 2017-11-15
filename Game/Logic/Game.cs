using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Game.NMapCreator;

namespace Game.Logic
{
    public class Game
    {
        private ICreature[,] currentMap;
        private MapCreator creator;

        public ICreature[,] CurrentMap() => currentMap;
        public bool IsOver
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Game()
        {
            creator = new MapCreator();
            currentMap = creator.Maps[0];
        }

    }
}
