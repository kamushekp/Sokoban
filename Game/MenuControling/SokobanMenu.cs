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


    public class SokobanMenu : IPage
    {
        public GraphicsDeviceManager Device { get; set; }
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
            var cmd = butns[0];

            if (butns.Length == 1)
            {
                ChangeCurrentButton(cmd);
                if (cmd == Keys.Enter)
                {
                    return currentButton + 1;
                }
            }
            

            for (int i = 0; i < buttonsCount; i++)
                ButtonsColors[i] = (i != CurrentButton ? Color.White : Color.Red);

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

    public class InputName : IPage
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

            if (inputtedName != null &&inputtedName.Length >= 4 && currentKeyboardState.IsKeyDown(Keys.Enter))
            {
                return (int)GameState.nameInputted;
                
            }

            inputtedName += currentKeyboardState.GetPressedKeys()[0];
            return (int)GameState.inputtingName;
        }
    }

    public class ShowFiveBest
    {
        public GraphicsDeviceManager Device { get; set; }
        public List<Tuple<int, string>> TopFive;
        private SpriteFont font;

        public ShowFiveBest(ContentManager content, GraphicsDeviceManager device, List<Tuple<int, string>> topFive)
        {
            TopFive = topFive;
            var dir = Environment.CurrentDirectory;
            var pathToStats = dir.Substring(0, dir.LastIndexOf("Game") + 4) + "\\";
            font = content.Load<SpriteFont>("Score");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var x = 100;
            var y = 100;
            foreach (var elem in TopFive)
            {
                spriteBatch.DrawString(font, String.Format("Player {0}: {1} steps to win", elem.Item2, elem.Item1), new Vector2(x, y), Color.Black);
                y += 50;
            }

        }

        public int Update(KeyboardState currentKeyboardState)
        {
            return (int)GameState.menu;
        }

    }

    public class Congrats : IPage
    {
        private Texture2D texture;
        private GraphicsDeviceManager device;

        public Congrats(ContentManager content, GraphicsDeviceManager device)
        {
            this.device = device;
            texture = content.Load<Texture2D>("Graphics\\Menus\\win");
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
            return (int)GameState.menu;
        }
    }
}
