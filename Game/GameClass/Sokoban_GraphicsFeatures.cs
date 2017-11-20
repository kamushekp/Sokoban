using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NGame.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Logic
{
    public sealed partial class Sokoban
    {
        private ContentManager content;

        private Dictionary<string, Texture2D> textures; //текстуры сущностей
        private int currentTextureSize; //размер ящика (для этих текстур = 64)

        private int windowHeight;
        private int windowWidth;

        private Texture2D[] backgrounds;// текстуры полов уровней
     
        private Vector2[,] currentLocations; //местоположения существ в пикселях


        public Rectangle GetCurrentMapRectangle()
        {
            return creator.CurrentMapRectangle;
        }

        public Dictionary<string, Texture2D> Textures() => textures;

        public int GetTextureSize() => currentTextureSize;

        public Texture2D GetBackground(int i)
        {
            return backgrounds[i];
        }

        public float DrawMultiplier { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var creature in AdditionalObjects)
            {
                creature.Draw(spriteBatch, currentLocations[creature.Location.X, creature.Location.Y]);
            }

            for (int i = 0; i < GetHeight(); i++)
            {
                for (int j = 0; j < GetWidth(); j++)
                {
                    if (currentMap[j, i] != null)
                    {
                        currentMap[j, i].Draw(spriteBatch, currentLocations[j, i]);
                    }
                }
            }
        }

        public void SetMap(int i)
        {
            CurrentMap = creator.Maps[i];
            currentActiveTargets = 0;

            foreach (var creature in CurrentMap)
            {
                if (creature is Target)
                {
                    currentActiveTargets++;
                }
            }

            var mapWidth = CurrentMap.GetLength(0) * currentTextureSize;
            var mapHeight = CurrentMap.GetLength(1) * currentTextureSize;

            creator.CurrentMapRectangle = new Rectangle(
                (int)currentLocations[0, 0].X, (int)currentLocations[0, 0].Y,
                 mapWidth, mapHeight);

            DrawMultiplier = Math.Min((float)windowWidth / mapWidth, (float)windowHeight / mapHeight);

        }
    }
}
