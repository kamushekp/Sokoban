using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NGame.Creatures;
using NGame.NMapCreator;

namespace NGame.Logic
{
    public class Sokoban
    {
        private Dictionary<string, Texture2D> textures;
        private MapCreator creator;

        //существа и их местоположения в пикселях
        private ACreature[,] currentMap;
        private Vector2[,] currentLocations;

        private ContentManager content;

        //в количествах существ
        private int height;
        private int width;

        private bool isOver;
        private int currentTextureSize;

        public ACreature GetCreature(Location loc)
        {
            if (loc == null)
            {
                throw new ArgumentNullException(nameof(loc));
            }

            try
            {
                return CurrentMap[loc.X, loc.Y];
            }
            catch (InvalidOperationException e)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }
        }
        public void SetCreature(Location loc, ACreature creature)
        {
            if (loc == null)
            {
                throw new ArgumentNullException(nameof(loc));
            }

            try
            {
                CurrentMap[loc.X, loc.Y] = creature;
            }
            catch (InvalidOperationException e)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }
        }

        public int GetHeight() => height;
        public int GetWidth() => width;
        public bool IsOver() => isOver;
        public int GetTextureSize() => currentTextureSize;

        public ACreature[,] CurrentMap
        {
            get { return currentMap; }
            set
            {
                height = value.GetLength(1);
                width = value.GetLength(0);
                currentLocations = new Vector2[width, height];

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        currentLocations[j, i] = new Vector2((j + 1) * currentTextureSize,
                                                             (i + 1) * currentTextureSize);
                    }
                }

                currentMap = value;
            }
        }
        

        public Sokoban(GraphicsDeviceManager graphics, ContentManager content)
        {
            this.content = content;

            textures = new Dictionary<string, Texture2D>
            {
                {nameof(Box), content.Load<Texture2D>("Graphics\\box0")},
                {nameof(Empty), content.Load<Texture2D>("Graphics\\empty0")},
                {nameof(Player), content.Load<Texture2D>("Graphics\\Char4")},
            };

            currentTextureSize = 64;

            var isSizesAreSame = textures.Values.
                SelectMany(x => new int[]{ x.Width, x.Height}).
                All(x => x == currentTextureSize);
            
            creator = new MapCreator(graphics, textures);
            CurrentMap = creator.Maps[0];
            isOver = false;
        }

        public void ReleaseCreatures(params string[] creatures)
        {
            foreach(var creature in this.currentMap)
            {
                if (creatures.Contains(creature.GetType().Name))
                {
                    creature.IsActive = true;
                }
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GetHeight(); i++)
            {
                for (int j = 0; j < GetWidth(); j++)
                {
                    currentMap[j, i].Draw(spriteBatch, currentLocations[j, i]);
                }
            }
        }

        public void Update(KeyboardState currentKeyboardState)
        {
            
            this.ReleaseCreatures(nameof(Player));
            var pressedKeys = currentKeyboardState.GetPressedKeys();

            foreach (var creature in currentMap)
            {
                if (creature.GetWhatReactingOn().Contains(pressedKeys[0].ToString()))
                {
                    creature.CreatureHandler.ChangeGameState(this, creature, new UserComand(currentKeyboardState));
                }
            }
        }

    }
}
