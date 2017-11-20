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
    public sealed class ShowFiveBest
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
            if (currentKeyboardState.IsKeyDown(Keys.Enter))
            {
                return (int)GameState.menu;
            }
            else
            {
                return (int)GameState.showingBest;
            }
        }

    }
}