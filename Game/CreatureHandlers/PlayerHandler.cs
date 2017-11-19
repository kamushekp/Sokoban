using NGame.Creatures;
using NGame.Logic;


namespace NGame.CreaturesHandlers
{
    class PlayerHandler : ACreatureHandler
    {
        public override void ChangeGameState(Sokoban game, ACreature player, UserComand comand)
        {
            if (player.IsActive)
            {
                var nextLocation = GetNewLoc(game, player.Location, comand);
                var nextCreature = game.GetCreature(nextLocation);

                var nextnextLocation = GetNewLoc(game, nextLocation, comand);
                var nextnextCreature = game.GetCreature(nextnextLocation);

                if (nextLocation != nextnextLocation)
                {
                    if (nextCreature is Box && nextCreature.IsActive)
                    {
                        if (nextnextCreature is null)
                        {
                            game.MoveCreature(nextnextLocation, nextCreature);
                            game.MoveCreature(nextLocation, player);
                        }

                        else if (nextnextCreature is Target)
                        {
                            nextnextCreature.IsActive = false;
                            nextCreature.IsActive = false;
                            game.DecreaseTargetNumber();

                            game.MoveCreature(nextnextLocation, nextCreature);
                            game.MoveCreature(nextLocation, player);
                        }
                    }

                    if (nextCreature is null)
                    {
                        game.MoveCreature(nextLocation, player);
                    }

                    if (nextCreature is Target)
                    {
                        game.MoveCreature(nextLocation, player);
                        game.AdditionalObjects.Add(nextCreature);
                    }

                }
                player.IsActive = false;
            }
        }
    }
}
