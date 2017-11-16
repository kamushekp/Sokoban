using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NGame.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NGame.creature_box
{
    public class Box : ICreature
    {
        private UserComand currentComand;
        //private BoxHandler handler;
        private Vector2 location;
        private Texture2D texture;

        public Vector2 Location => location;

        //public ICreatureHandler CreatureHandler => handler;

        public Box(int x, int y, string imageFileName)
        {
            //handler = new BoxHandler();
            IsActive = true;
            location = new Vector2(x, y);
        }

        public Box(int x, int y, Texture2D texture)
        {
            //handler = new BoxHandler();
            IsActive = true;
            location = new Vector2(100*x, 100*y);
            this.texture = texture;

        }

        public bool IsActive { get; set; }

        public UserComand CurrentComand
        {
            get => currentComand;
            set => currentComand = value;
        }

        public List<UserComand> GetWhatReactingOn()
        {
            return new List<UserComand>();
        }

        public int GetDrawingPriority()
        {
            throw new NotImplementedException();
        }

        public Texture2D Texture => texture;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, Color.White);
        }
    }
}
