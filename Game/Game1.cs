using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NGame.Logic;
using System;

namespace NGame
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sokoban sokoban;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        private const float TIMER = 0.1f;
        private float timer = TIMER;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {
            sokoban = new Sokoban(graphics, Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }
       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (timer < 0)
            {
                var pressedKeys = currentKeyboardState.GetPressedKeys();
                if (pressedKeys.Length == 1)
                {
                    sokoban.Update(currentKeyboardState, gameTime);
                }
                timer = TIMER;
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
                Matrix.CreateScale(sokoban.DrawMultiplier));

            spriteBatch.Draw(sokoban.GetBackground(0), sokoban.GetCurrentMapRectangle(), Color.White);

            sokoban.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
             
        }
    }
}
