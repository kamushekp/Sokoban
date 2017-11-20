using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NGame.Logic;
using System;
using System.Collections.Generic;
using NGame;

namespace NGame.MenuControling
{
    public sealed class SokobanMenu : IPage
    {
        public GraphicsDeviceManager Device { get; set; }
        private const int buttonsCount = 3;
        private int currentButton;

        private const int buttonHeight = 40,
                          buttonWidth = 88;

        public Texture2D[] Buttons { get; set; }
        public Rectangle[] ButtonsRects { get; set; }
        public Color[] ButtonsColors { get; set; }

        private int[] modes = new[] { 1, 2, 8 };

        public int CurrentButton
        {
            get { return currentButton; }
            set
            {
                currentButton = value % buttonsCount;
                if (currentButton < 0) currentButton = 0;
            }
        }

        public SokobanMenu(ContentManager content, GraphicsDeviceManager device)
        {
            Device = device;

            Buttons = new Texture2D[buttonsCount];
            ButtonsRects = new Rectangle[buttonsCount];
            ButtonsColors = new Color[buttonsCount];

            Buttons[0] = content.Load<Texture2D>("Graphics\\MainMenu\\start");
            Buttons[1] = content.Load<Texture2D>("Graphics\\MainMenu\\best");
            Buttons[2] = content.Load<Texture2D>("Graphics\\MainMenu\\Maps");

            var x = (Device.PreferredBackBufferWidth - buttonWidth) / 5;
            var y = (Device.PreferredBackBufferHeight- buttonsCount * buttonHeight -
                (buttonsCount % 2) * buttonsCount) / 5;

            for (int i = 0; i < buttonsCount; i++)
            {
                ButtonsColors[i] = Color.White;
                ButtonsRects[i] = new Rectangle(x, y, buttonWidth, buttonHeight);
                    y += buttonHeight;
            }
        }
            
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < buttonsCount; i++)
            {
                spriteBatch.Draw(Buttons[i], ButtonsRects[i], ButtonsColors[i]);
            }

        }

        public int Update(KeyboardState currentKeyboardState)
        {
            var butns = currentKeyboardState.GetPressedKeys();
            if (butns.Length != 0)
            {
                var cmd = butns[0];

                if (butns.Length == 1)
                {
                    ChangeCurrentButton(cmd);
                    if (cmd == Keys.Enter)
                    {
                        return modes[currentButton];
                    }
                }

                for (int i = 0; i < buttonsCount; i++)
                    ButtonsColors[i] = (i != CurrentButton ? Color.White : Color.Red);
            }
            return (int)GameState.menu;
        }

        private void ChangeCurrentButton(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    CurrentButton -= 1;
                    break;

                case Keys.Down:
                    CurrentButton += 1;
                    break;

                default:
                    break;
            }
        }
    }

}
