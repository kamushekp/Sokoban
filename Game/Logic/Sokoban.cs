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
    public sealed class Sokoban
    {
        private ContentManager content;

        private Dictionary<string, Texture2D> textures;
       
        //игровые карты существ (в том числе) в creator'e
        private MapCreator creator;
       
        //текущая карта существ
        private ACreature[,] currentMap;

        //их местоположения в пикселях
        private Vector2[,] currentLocations;

        //затраченные очки хода
        private int steps = 0;

        //непогашенные цели
        private int currentActiveTargets;

        //высота, ширина в количествах существ
        private int height;
        private int width;

        private bool isOver;
        private int currentTextureSize;
        public List<ACreature> AdditionalObjects { get; set; }

        public ACreature GetCreature(Location loc)
        {
            if (loc == null)
            {
                throw new ArgumentNullException(nameof(loc));
            }

            return CurrentMap[loc.X, loc.Y];

        }

        public void DecreaseTargetNumber()
        {
            currentActiveTargets -= 1;
        }
        public bool IsMapEnd()
        {
            return currentActiveTargets == 0;
        }

        public void RemoveCreature(Location loc)
        {
            CurrentMap[loc.X, loc.Y] = null;
        }
        public void SetCreature(Location loc, ACreature creature)
        {
            if (loc == null)
            {
                throw new ArgumentNullException(nameof(loc));
            }

            CurrentMap[loc.X, loc.Y] = creature;
            creature.Location = loc;
        }
        public void MoveCreature(Location finish, ACreature creature)
        {
            RemoveCreature(creature.Location);
            SetCreature(finish, creature);            
        }
        public void SetMap(int i)
        {
            CurrentMap = creator.Maps[i];
            currentActiveTargets = 0;
            foreach(var creature in CurrentMap)
            {
                if (creature is Target)
                {
                    currentActiveTargets++;
                }
            }
        }

        public Dictionary<string, Texture2D> Textures() => textures;
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
                //{nameof(Empty), content.Load<Texture2D>("Graphics\\empty0")},
                {nameof(Player), content.Load<Texture2D>("Graphics\\Char4")},
                {nameof(Wall), content.Load<Texture2D>("Graphics\\wall0")},
                {nameof(Target), content.Load<Texture2D>("Graphics\\target0")},
            };
            currentTextureSize = 64;

            creator = new MapCreator(graphics, textures);
            SetMap(0);
            isOver = false;
            AdditionalObjects = new List<ACreature>();
        }

        public void ReleaseCreatures(params string[] creatures)
        {
            foreach(var creature in this.currentMap)
            {
                if (creature != null && creatures.Contains(creature.GetType().Name))
                {
                    creature.IsActive = true;
                }
                
            }
        }

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

        public void Update(KeyboardState currentKeyboardState)
        {
            this.ReleaseCreatures(nameof(Player));
            var pressedKeys = currentKeyboardState.GetPressedKeys();

            foreach (var creature in currentMap)
            {
                if (creature!= null && creature.CreatureHandler != null)
                {
                    creature.CreatureHandler.ChangeGameState(this, creature, new UserComand(currentKeyboardState));
                }
            }

            foreach (var creature in AdditionalObjects)
            {
                creature.CreatureHandler.ChangeGameState(this, creature, new UserComand(currentKeyboardState));
            }

            if (IsMapEnd())
            {
                SetMap(1);
            }
        }

    }
}
