using System.Collections.Generic;
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
        private ACreature[,] currentMap;
        private ContentManager content;
        private int height;
        private int width;
        private bool isOver;
        private int currentTextureSize;

        public int GetHeight() => height;
        public int GetWidth() => width;
        public bool IsOver() => isOver;
        public int GetTextureSize() => currentTextureSize;

        public ACreature[,] CurrentMap
        {
            get { return currentMap; }
            set
            {
                height = value.GetLength(0);
                width = value.GetLength(1);
                currentMap = value;
            }
        }
        
        

        public Sokoban(GraphicsDeviceManager graphics, ContentManager content)
        {
            this.content = content;

            //TODO: check if they has same size
            textures = new Dictionary<string, Texture2D>
            {
                {nameof(Box), content.Load<Texture2D>("Graphics\\box0")},
                {nameof(Empty), content.Load<Texture2D>("Graphics\\empty0")},
                {nameof(Player), content.Load<Texture2D>("Graphics\\Char4")},
            };
            var texturesSize = textures[nameof(Box)].Height;
            currentTextureSize = texturesSize;

            creator = new MapCreator(graphics, textures);
            CurrentMap = creator.Maps[0];
            isOver = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var field in this.currentMap)
            {
                field.Draw(spriteBatch);
            };
        }

        public void Update(KeyboardState currentKeyboardState)
        {
            var pressedKeys = currentKeyboardState.GetPressedKeys();
            foreach (var creature in currentMap)
            {
                if (pressedKeys.Length > 0 &&
                    creature.GetWhatReactingOn().Contains(pressedKeys[0].ToString()))
                {
                    creature.CreatureHandler.ChangeGameState(this, creature, new UserComand(currentKeyboardState));
                }
            }
        }

    }
}
