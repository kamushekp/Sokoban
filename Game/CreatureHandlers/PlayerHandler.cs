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
                var nextLocation = GetNewLoc(game, player, comand);
                var nextCreature = game.GetCreature(nextLocation);

                var nextnextLocation = GetNewLoc(game, nextCreature, comand);
                var nextnextCreature = game.GetCreature(nextnextLocation);

                if (nextLocation != nextnextLocation)
                {
                    if (nextCreature is Box && nextCreature.IsActive)
                    {
                        if (nextnextCreature is Empty)
                        {
                            SwapCreatures(game, nextCreature, nextnextCreature);
                            SwapCreatures(game, player, nextnextCreature);
                        }

                        else if (nextnextCreature is Target)
                        {
                            nextnextCreature.IsActive = false;
                            nextCreature.IsActive = false;
                            game.DecreaseTargetNumber();

                            SwapCreatures(game, nextCreature, nextnextCreature);
                            SwapCreatures(game, player, new Empty(nextnextCreature.Location, game.Textures()[nameof(Empty)]));
                        }
                    }

                    if (nextCreature is Empty)
                    {
                        SwapCreatures(game, player, nextCreature);
                    }

                }
                player.IsActive = false;
            }
        }

        private void SwapCreatures(Sokoban game, ACreature first, ACreature second)
        {
            game.SetCreature(second.Location, first);
            game.SetCreature(first.Location, second);

            var saveLoc = first.Location;
            first.Location = second.Location;
            second.Location = saveLoc;
        }
    }
}
