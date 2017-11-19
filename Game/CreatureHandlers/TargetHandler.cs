
using NGame.Logic;

namespace NGame.CreatureHandlers
{
    class TargetHandler : ACreatureHandler
    {
        public override void ChangeGameState(Sokoban game, ACreature creature, UserComand comand)
        {
            if (game.CurrentMap[creature.Location.X, creature.Location.Y] == null)
                game.SetCreature(creature.Location, creature);
        }
    }
}
