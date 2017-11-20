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
    public sealed class InputName : IPage
    {

        private Texture2D texture;
        private GraphicsDeviceManager Device;
        private string inputtedName;

        public string GetName() => inputtedName;

        public InputName(ContentManager content, GraphicsDeviceManager device)
        {
            Device = device;
            texture = content.Load<Texture2D>("Graphics\\Menus\\input");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw
                (
                texture,
                new Rectangle(0, 0, Device.PreferredBackBufferWidth, Device.PreferredBackBufferHeight),
                Color.White
                );
        }

        public int Update(KeyboardState currentKeyboardState)
        {

            if (inputtedName != null && inputtedName.Length >= 4 && currentKeyboardState.IsKeyDown(Keys.Enter))
            {
                return (int)GameState.nameInputted;

            }

            var pressed = currentKeyboardState.GetPressedKeys();
            if (pressed.Length != 0)inputtedName += pressed[0];

            return (int)GameState.inputtingName;
        }
    }
}
