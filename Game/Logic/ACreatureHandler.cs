using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace NGame.Logic
{
    public abstract class ACreatureHandler
    {
        public abstract void ChangeGameState(Sokoban game, ACreature creature, UserComand comand);

        //возвращает новую локацию объекта (но не сдвигает объект!)
        public Location GetNewLoc(Sokoban game, ACreature creature, UserComand comand)
        {
            var textureSize = game.GetTextureSize();
            var dir = comand.Comand;
            var newLoc = new Location(creature.Location);

            if (dir.IsKeyDown(Keys.Down) && creature.Location.Y + 1 < game.GetHeight())
            {
                newLoc.Y += 1;
            }

            if (dir.IsKeyDown(Keys.Up) && creature.Location.Y > 0)
            {
                newLoc.Y -= 1;
            }
            
            if (dir.IsKeyDown(Keys.Right) && creature.Location.X + 1 < game.GetWidth() )
            {
                newLoc.X += 1;
            }

            if (dir.IsKeyDown(Keys.Left) && creature.Location.X > 0)
            {
                newLoc.X -= 1;
            }

            return newLoc;
        }
    }
}
