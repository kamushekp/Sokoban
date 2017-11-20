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
    enum GameState
    {
        menu = 0,
        inputtingName = 1,
        showingBest = 2,
        maps = 3,
        playing = 4,
        congrats = 5,
        nameInputted = 7

    }


    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public List<Tuple<int, string>> TopFive { get; set; }

        ShowFiveBest showFiveBest;
        SokobanMenu sokobanMenu;
        Sokoban sokoban;
        InputName inputName;
        Congrats congrats;

        private string playerName;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        private const float TIMER = 0.1f;
        private float timer = TIMER;

        private int gameState;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameState = (int)GameState.menu;

            TopFive = new List<Tuple<int, string>>();
            LoadBestPlayers();
        }

        protected override void Initialize()
        {
            sokobanMenu = new SokobanMenu(Content, graphics);
            sokoban = new Sokoban(graphics, Content);
            showFiveBest = new ShowFiveBest(Content, graphics, TopFive);
            inputName = new InputName(Content, graphics);
            congrats = new Congrats(Content, graphics);

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                SaveBestPlayers();
                Exit();
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (timer < 0)
            {
                var pressedKeys = currentKeyboardState.GetPressedKeys();

                if (pressedKeys.Length == 1)
                {
                    if (gameState == (int)GameState.inputtingName)
                    {
                        gameState = inputName.Update(currentKeyboardState);
                    }

                    if (gameState == (int)GameState.nameInputted)
                    {
                        playerName = inputName.GetName();
                        gameState = (int)GameState.playing;
                    }

                    else if (gameState == (int)GameState.menu)
                    {
                        gameState = sokobanMenu.Update(currentKeyboardState);
                    }

                    else if (gameState == (int)GameState.playing)
                    {
                        var scores = sokoban.Update(currentKeyboardState, gameTime);
                        HandleScores(scores);

                        if (scores != -1)
                        {
                            gameState = (int)GameState.congrats;
                        }
                    }

                    else if (gameState == (int)GameState.showingBest)
                    {
                        gameState = showFiveBest.Update(currentKeyboardState);
                    }

                    else if (gameState == (int)GameState.congrats)
                    {
                        SaveBestPlayers();
                        gameState = congrats.Update(currentKeyboardState);
                    }
                }
                timer = TIMER;
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (gameState == (int)GameState.playing)
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

            else if (gameState == (int)GameState.showingBest)
            {
                spriteBatch.Begin();

                showFiveBest.Draw(spriteBatch);

                spriteBatch.End();

            }

            else if (gameState == (int)GameState.inputtingName)
            {
                spriteBatch.Begin();

                inputName.Draw(spriteBatch);

                spriteBatch.End();
            }

            else if (gameState == (int)GameState.congrats)
            {
                spriteBatch.Begin();

                congrats.Draw(spriteBatch);

                spriteBatch.End();
            }

            base.Draw(gameTime);
             
        }

        private void HandleScores(int scores)
        {
            var keys = new List<Tuple<int, string>>();

            if (scores != -1)
            {
                TopFive.Add(new Tuple<int, string>(scores, playerName));
                TopFive.Sort();
                var slice = new List<Tuple<int, string>>();
                int i = 0;

                foreach(var elem in TopFive)
                {
                    slice.Add(elem);
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

                TopFive.Add(new Tuple<int, string>(score, splitted[0]));
            }
        }
        private void SaveBestPlayers()
        {
            var dir = Environment.CurrentDirectory;
            var pathToStats = dir.Substring(0, dir.LastIndexOf("Game") + 4) + "\\Stats\\stats.txt";
            var gameInfo = new List<string>();

            foreach (var elem in TopFive)
            {
                gameInfo.Add(String.Format("{0} {1}", elem.Item2, elem.Item1));
            }

            File.WriteAllLines(pathToStats, gameInfo);
        }
    }
}
