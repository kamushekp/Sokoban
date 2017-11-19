using NGame.Creatures;
using NGame.Logic;
using System;
using System.Linq;

namespace NGame.CreaturesHandlers
{
    public class BoxHandler : ACreatureHandler
    {
        public override Location ChangeGameState(Sokoban game, ACreature creature, UserComand comand)
        {
            creature.IsActive = !IsBlocked(game, creature);
            return null;
        }

        private bool IsBlocked(Sokoban game, ACreature creature)
        {
            var sides = new[]
            {
                new Location(creature.Location.X - 1, creature.Location.Y),
                new Location(creature.Location.X, creature.Location.Y - 1),
                new Location(creature.Location.X + 1, creature.Location.Y),                
                new Location(creature.Location.X, creature.Location.Y + 1)
            };

            var boxOrWall = sides.Select(x => game.GetCreature(x) is Wall ||
                                            game.GetCreature(x) is Box).ToArray();
            
            for (int i = 0; i < sides.Length - 1; i++)
            {
                if (boxOrWall[i] && boxOrWall[i + 1]) return true;
            }
            if (boxOrWall[boxOrWall.Length - 1] && boxOrWall[0]) return true;

            return false;
        }
    }
}