using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NGame.Logic;
using NGame.MenuControling;
using System;
using System.Collections.Generic;
using System.IO;

namespace NGame
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public SortedDictionary<int, string> TopFive { get; set; }
        SokobanMenu sokobanMenu;
        Sokoban sokoban;
        private string playerName;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        private const float TIMER = 0.1f;
        private float timer = TIMER;

        private int gameState;

        private enum GameState
        {
            menu = 0,
            playerName = 1,
            best = 2,
            maps = 3,
            play = 4

        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameState = (int)GameState.menu;
            TopFive = new SortedDictionary<int, string>();
            LoadBestPlayers();
        }

        protected override void Initialize()
        {
            sokobanMenu = new SokobanMenu(Content, graphics);
            sokoban = new Sokoban(graphics, Content);

            base.Initialize();

            sokoban.Update(currentKeyboardState, new GameTime());
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
                    if (gameState == (int)GameState.playerName)
                    {
                        playerName = "Pasha";
                        gameState = (int)GameState.play;
                    }

                    else if (gameState == (int)GameState.menu)
                    {
                        gameState = sokobanMenu.Update(currentKeyboardState);
                    }

                    else if (gameState == (int)GameState.play)
                    {
                        var scores = sokoban.Update(currentKeyboardState, gameTime);
                        HandleScores(scores);
                    }
                }
                timer = TIMER;
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (gameState == (int)GameState.play)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
                    Matrix.CreateScale(sokoban.DrawMultiplier));

                spriteBatch.Draw(sokoban.GetBackground(0), sokoban.GetCurrentMapRectangle(), Color.White);

                sokoban.Draw(spriteBatch);

                spriteBatch.End();
            }
            else if (gameState == (int)GameState.menu)

            {
                spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
                    Matrix.CreateScale(2));

                sokobanMenu.Draw(spriteBatch);

                spriteBatch.End();
            }
           
            base.Draw(gameTime);
             
        }

        private void HandleScores(int scores)
        {
            var keys = new List<int>();

            if (scores == -1)
            {
                TopFive.Add(scores, playerName);
                var slice = new SortedDictionary<int, string>();
                int i = 0;

                foreach(var elem in slice)
                {
                    slice.Add(elem.Key, elem.Value);
                    i++;
                    if (i == 5)break;
                }

                TopFive = slice;
            }
            }

        private void LoadBestPlayers()
        {
            var dir = Environment.CurrentDirectory;
            var pathToStats = dir.Substring(0, dir.LastIndexOf("Game") + 4) + "\\Stats\\";
            var rows = File.ReadAllLines(pathToStats + "stats.txt");

            foreach(var row in rows)
            {
                var splitted = row.Split();
                int.TryParse(splitted[1], out int score);

                TopFive.Add(score, splitted[0]);
            }
        }
    }
}
