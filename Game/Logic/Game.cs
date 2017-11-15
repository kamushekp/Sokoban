using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Game.Logic
{
    public class Game
    {
        public ICreature[,] MapOfCreatures {get;}
        public bool IsOver
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public ReadOnlyCollection<string> GameMaps { get; }
    }
}
