using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace NGame.Logic
{
    public abstract class ACreatureHandler
    {
        public abstract bool ChangeGameState(Sokoban game, ACreature creature, UserComand comand);

        public void Move(Sokoban game, ACreature creature, UserComand comand)
        {
            var dir = comand.Comand;
            if (dir.IsKeyDown(Keys.Down))
            {
                if (creature.Location.Y + 1 < game.GetHeight())
                {
                    creature.Location.Y += 1;
                    creature.PixLocation = new Vector2(creature.PixLocation.X, creature.PixLocation.Y + game.GetTextureSize());
                }
            }
        }
    }
}
