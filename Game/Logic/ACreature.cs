using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGame.Logic
{
    public abstract class ACreature
    {
        public Animation CreatureAnimation;

        public bool IsActive { get; set; }
        
        //положение по матрице объектов
        public Location Location { get; set; }

        public Texture2D Texture { get; set; }
        
        public ACreatureHandler CreatureHandler { get; set; }

        public UserComand CurrentComand { get; set; }
        
        public abstract List<String> GetWhatReactingOn();

        public ACreature(Location location, Texture2D texture)
        {
            IsActive = true;
            Location = location;
            Texture = texture;
        }

        public abstract int GetDrawingPriority();

        public void Draw(SpriteBatch spriteBatch, Vector2 PixLocation)
        {
            spriteBatch.Draw(Texture, PixLocation, Color.White);
        }
    }
}
