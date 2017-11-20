
using NGame.Logic;

namespace NGame.CreatureHandlers
{
    class TargetHandler : ACreatureHandler
    {
        public override Location ChangeGameState(Sokoban game, ACreature creature, UserComand comand)
        {
            //если кнопка находилась под игроком, она в стеке скрытых объектов.
            //если теперь это уже не так, то вытащим ее из стека на карту
            if (game.CurrentMap[creature.Location.X, creature.Location.Y] == null)
            {
                game.SetCreature(creature.Location, creature);
            }
                
            return null;
        }
    }
}
