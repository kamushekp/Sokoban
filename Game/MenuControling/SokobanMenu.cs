using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NGame.Logic;
using System;
using System.Collections.Generic;

namespace NGame.MenuControling
{
    public class SokobanMenu
    {
        public GraphicsDeviceManager Device { get; set; }

        private enum buttons
            {
            menu = 0,
            play = 1,
            best = 2,
            maps = 3,
        }
        private const int buttonsCount = 3;
        private int currentButton;

        private const int buttonHeight = 40,
                          buttonWidth = 88;

        public Texture2D[] Buttons { get; set; }
        public Rectangle[] ButtonsRects { get; set; }
        public Color[] ButtonsColors { get; set; }
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

            Buttons[(int)buttons.play - 1] = content.Load<Texture2D>("Graphics\\MainMenu\\start");
            Buttons[(int)buttons.best - 1] = content.Load<Texture2D>("Graphics\\MainMenu\\best");
            Buttons[(int)buttons.maps - 1] = content.Load<Texture2D>("Graphics\\MainMenu\\Maps");

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
            var cmd = butns[0];

            if (butns.Length == 1)
            {
                ChangeCurrentButton(cmd);
                if (cmd == Keys.Enter)
                    return (int)buttons.play;
            }
            

            for (int i = 0; i < buttonsCount; i++)
                ButtonsColors[i] = (i != CurrentButton ? Color.White : Color.Red);

            return 0;
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

    public class NewGame
    {

    }
    public class ShowFiveBest
    {

    }
}
