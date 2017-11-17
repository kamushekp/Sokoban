using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NGame.creature_box;
using NGame.NMapCreator;

namespace NGame.Logic
{
    public class Sokoban
    {
        private Dictionary<string, Texture2D> textures;
        private MapCreator creator;
        private ACreature[,] currentMap;
        private ContentManager content;
        
        private bool isOver;
    
        public ACreature[,] CurrentMap() => currentMap;
        public bool IsOver() => isOver;
        

        public Sokoban(GraphicsDeviceManager graphics, ContentManager content)
        {
            this.content = content;

            textures = new Dictionary<string, Texture2D>
            {
                {nameof(Box), content.Load<Texture2D>("Graphics\\box0")}
            };

            creator = new MapCreator(graphics, textures);
            currentMap = creator.Maps[0];
            isOver = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var field in this.currentMap)
            {
                field.Draw(spriteBatch);
            };
        }

    }
}
