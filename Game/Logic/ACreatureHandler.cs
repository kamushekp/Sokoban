using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace NGame.Logic
{
    public abstract class ACreatureHandler
    {
        public abstract void ChangeGameState(Sokoban game, ACreature creature, UserComand comand);

        //возвращает новую локацию объекта (но не сдвигает объект!)
        public Location GetNewLoc(Sokoban game, Location location, UserComand comand)
        {
            var textureSize = game.GetTextureSize();
            var dir = comand.Comand;
            var newLoc = new Location(location);

            if (dir.IsKeyDown(Keys.Down) && location.Y + 1 < game.GetHeight())
            {
                newLoc.Y += 1;
            }

            if (dir.IsKeyDown(Keys.Up) && location.Y > 0)
            {
                newLoc.Y -= 1;
            }
            
            if (dir.IsKeyDown(Keys.Right) && location.X + 1 < game.GetWidth() )
            {
                newLoc.X += 1;
            }

            if (dir.IsKeyDown(Keys.Left) && location.X > 0)
            {
                newLoc.X -= 1;
            }

            return newLoc;
        }
    }
}
