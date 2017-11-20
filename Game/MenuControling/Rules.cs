using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NGame.MenuControling
{
    public sealed class Rules : IPage
    {
        private Texture2D texture;
        private GraphicsDeviceManager device;

        public Rules(ContentManager content, GraphicsDeviceManager device)
        {
            this.device = device;
            texture = content.Load<Texture2D>("Graphics\\Menus\\rules");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw
                (
                texture,
                new Rectangle(0, 0, device.PreferredBackBufferWidth, device.PreferredBackBufferHeight),
                Color.White
                );
        }

        public int Update(KeyboardState currentKeyboardState)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Enter))
            {
                return (int)GameState.menu;
            }
            else
            {
                return (int)GameState.rules;
            }
        }
    }
}
