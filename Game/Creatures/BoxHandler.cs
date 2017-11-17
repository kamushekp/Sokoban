using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NGame.Logic;

namespace NGame.creature_box
{
    public class BoxHandler : ICreatureHandler
    {

        public bool ChangeGameState(Logic.Sokoban game, ACreature creature, UserComand comand)
        {
            
            return true;
        }
    }

}