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
        nameInputted = 7,
        rules = 8,
        gameover = 9
    }


    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int gameState;

        ShowFiveBest showFiveBest;
        SokobanMenu sokobanMenu;
        Sokoban sokoban;
        InputName inputName;
        Congrats congrats;
        Rules rules;
        GameOver gamover;

        private string playerName;
        public List<Tuple<int, string>> TopFive { get; set; }

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        private const float TIMER = 0.1f;
        private float timer = TIMER;

        bool isGameIsFirstGameInSession = true;

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
            //разные менюшечки + игра
            sokobanMenu = new SokobanMenu(Content, graphics);
            sokoban = new Sokoban(Content, graphics);
            showFiveBest = new ShowFiveBest(Content, graphics, TopFive);
            inputName = new InputName(Content, graphics);
            congrats = new Congrats(Content, graphics);
            rules = new Rules(Content, graphics);
            gamover = new GameOver(Content, graphics);

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
                    switch ((GameState)gameState)
                    {
                        case GameState.menu:
                            {
                                gameState = sokobanMenu.Update(currentKeyboardState);
                                break;
                            }

                        case GameState.inputtingName:
                            {
                                gameState = inputName.Update(currentKeyboardState);
                                break;
                            }

                        case GameState.nameInputted:
                            {
                                playerName = inputName.GetName();
                                gameState = (int)GameState.playing;
                                break;
                            }

                        case GameState.playing:
                            {
                                if (isGameIsFirstGameInSession)
                                {
                                    var scores = sokoban.Update(currentKeyboardState, gameTime);
                                    

                                    if (scores > 0)
                                    {
                                        HandleScores(scores);
                                        gameState = (int)GameState.congrats;
                                    }

                                    else if (scores == -2)
                                    {
                                        gameState = (int)GameState.gameover;
                                    }

                                }
                                else
                                {
                                    sokoban = new Sokoban(Content, graphics);
                                    //sokoban.Draw(spriteBatch);
                                    isGameIsFirstGameInSession = true;
                                }
                                break;
                            }

                        case GameState.showingBest:
                            {
                                gameState = showFiveBest.Update(currentKeyboardState);
                                break;
                            }

                        case GameState.congrats:
                            {
                                SaveBestPlayers();
                                gameState = congrats.Update(currentKeyboardState);
                                break;
                            }

                        case GameState.rules:
                            {
                                gameState = rules.Update(currentKeyboardState);
                                break;
                            }

                        case GameState.gameover:
                            {
                                gameState = (int)GameState.menu;
                                isGameIsFirstGameInSession = false;
                                break;
                            }
                    }
                }

                timer = TIMER;
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch((GameState)gameState)
            {
                case GameState.playing:
                    {
                        spriteBatch.Begin
                            (
                            SpriteSortMode.Immediate,
                            null, null, null, null, null,
                            Matrix.CreateScale(sokoban.DrawMultiplier)
                            );

                        spriteBatch.Draw(sokoban.GetBackground(0), sokoban.GetCurrentMapRectangle(), Color.White);
                        sokoban.Draw(spriteBatch);

                         break;
                    }
                case GameState.menu:
                    {
                        spriteBatch.Begin(
                            SpriteSortMode.Immediate,
                            null, null, null, null, null,
                            Matrix.CreateScale(2)
                                          );
                        sokobanMenu.Draw(spriteBatch);

                        break;
                    }
                case GameState.showingBest:
                    {
                        spriteBatch.Begin();
                        showFiveBest.Draw(spriteBatch);

                        break;

                    }
                case GameState.inputtingName:
                    {
                        spriteBatch.Begin();
                        inputName.Draw(spriteBatch);

                        break;
                    }
                case GameState.congrats:
                    {
                        spriteBatch.Begin();
                        congrats.Draw(spriteBatch);

                        break;
                    }
                case GameState.rules:
                    {
                        spriteBatch.Begin();
                        rules.Draw(spriteBatch);

                        break;
                    }
                case GameState.gameover:
                    {
                        spriteBatch.Begin();
                        gamover.Draw(spriteBatch);

                        break;
                    }
            }

            spriteBatch.End();
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
                isGameIsFirstGameInSession = false;
            }
            }

        private void LoadBestPlayers()
        {
            var dir = Environment.CurrentDirectory;
            var pathToStats = FindFile(dir, "stats");
            var rows = File.ReadAllLines(pathToStats);

            var topCount = 5;
            foreach(var row in rows)
            {
                if (topCount == 0) break;
                var splitted = row.Split();
                int.TryParse(splitted[1], out int score);

                TopFive.Add(new Tuple<int, string>(score, splitted[0]));
                topCount--;
            }
        }

        private void SaveBestPlayers()
        {
            var dir = Environment.CurrentDirectory;
            var pathToStats = FindFile(dir, "stats");
            var gameInfo = new List<string>();

            foreach (var elem in TopFive)
            {
                gameInfo.Add(String.Format("{0} {1}", elem.Item2, elem.Item1));
            }

            File.WriteAllLines(pathToStats, gameInfo);
        }

        private string FindFile(string path, string name)
        {

            int i = 0;
            while (i < 15)
            {
                var files = Directory.GetFiles(path);

                foreach (var dir in files)
                {
                    if (dir.Contains(name))
                    {
                        return dir;
                    }
                }
                path = Directory.GetParent(path).ToString();
                i++;
            }
            return "";
        }
    }
}

