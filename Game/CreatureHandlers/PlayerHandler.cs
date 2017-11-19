using Microsoft.Xna.Framework.Input;
using NGame.Creatures;
using NGame.Logic;


namespace NGame.CreaturesHandlers
{
    class PlayerHandler : ACreatureHandler
    {
        public override Location ChangeGameState(Sokoban game, ACreature player, UserComand comand)
        {
            HandleAnimations(player, comand);

            Location savePlayerLocation = new Location(player.Location);
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
            return player.Location.Sub(savePlayerLocation);
        }

        private void HandleAnimations(ACreature player, UserComand comand)
        {
            var button = comand.Comand.GetPressedKeys()[0].ToString();
            if (button == "Right" || button == "Left" || button == "Up" || button == "Down")
            {
                player.CurrentAnimation = player.Animations[button];
            }
            
        }
    }
}
