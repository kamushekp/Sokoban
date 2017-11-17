using NGame.Logic;

namespace NGame.CreaturesHandlers
{
    public class BoxHandler : ACreatureHandler
    {

        public override bool ChangeGameState(Logic.Sokoban game, ACreature creature, UserComand comand)
        {
            return true;
        }
    }

}