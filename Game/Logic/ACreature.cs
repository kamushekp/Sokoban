using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGame.Logic
{
    public abstract class ACreature
    {
        public bool IsActive { get; set; }
        
        //положение по матрице объектов
        public Location Location { get; set; }

        //положение по пикселям
        public Vector2 PixLocation { get; set; }

        public Texture2D Texture { get; set; }
        
        public ICreatureHandler CreatureHandler { get; set; }

        public UserComand CurrentComand { get; set; }
        
        public abstract List<UserComand> GetWhatReactingOn();

        public ACreature(Location location, Vector2 vector2, Texture2D texture)
        {
            IsActive = true;
            PixLocation = vector2;
            Location = location;
            Texture = texture;
        }

        public void Move(int dx, int dy)
        {
            Location.X += dx;
            Location.Y += dy;
        }

        public int GetDrawingPriority()
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, PixLocation, Color.White);
        }


    }
}
