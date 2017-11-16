using System;
using System.Collections.Generic;
using System.Text;

namespace NGame.Logic
{
    public interface ICreatureHandler
    {
        bool ChangeGameState(Sokoban game, ICreature creature, UserComand comand);
    }
}
