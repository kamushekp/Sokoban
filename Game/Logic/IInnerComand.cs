using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Logic
{
    public interface ICreatureHandler
    {
        bool ChangeGameState(Game game, ICreature creature, UserComand comand);
    }
}
