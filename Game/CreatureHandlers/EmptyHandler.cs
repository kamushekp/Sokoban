using NGame.Logic;

namespace NGame.CreaturesHandlers
{
    class EmptyHandler : ACreatureHandler
    {
        public override bool ChangeGameState(Sokoban game, ACreature creature, UserComand comand)
        {
            return true;
        }
    }
}
