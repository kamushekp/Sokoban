using Microsoft.Xna.Framework.Input;
using NGame.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.CreaturesHandlers
{
    class PlayerHandler : ACreatureHandler
    {
        public override bool ChangeGameState(Sokoban game, ACreature creature, UserComand comand)
        {
            Move(game, creature, comand);
            return true;
        }
    }
}
