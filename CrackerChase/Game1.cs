using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using System;
using System.Collections.Generic;
using System.Text;

namespace CrackerChase
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        enum GameStates { StartScreen, Gameplay };
        GameStates currentState = GameStates.Gameplay;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        // Game World
        // These variables define the world 

        Mover cheese;
        Target cracker;
        Sprite background;
        SoundEffect BurpSound;

        int screenWidth;
        int screenHeight;

        List<Sprite> gameSprites = new List<Sprite>();
        List<Target> crackers = new List<Target>();

        SpriteFont messageFont;

        string messageString = "Hello world";

        int score;
        int timer;



        void startPlayingGame()
        {
            currentState = GameStates.Gameplay;

            foreach (Sprite s in gameSprites)
            {
                s.Reset();
            }
            foreach (Target t in crackers)
            {
                t.Reset();
            }
            messageString = "Cracker Chase";

            timer = 600;
            score = 0;

           
        }

      
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            currentState = GameStates.StartScreen;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            messageFont = Content.Load<SpriteFont>("MessageFont");

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            Texture2D cheeseTexture = Content.Load<Texture2D>("cheese");
            Texture2D cloth = Content.Load<Texture2D>("Tablecloth");
            Texture2D crackerTexture = Content.Load<Texture2D>("cracker");

            BurpSound = Content.Load<SoundEffect>("Burp");

            background = new Sprite(screenWidth, screenHeight, cloth, screenWidth, 0, 0);
            gameSprites.Add(background);

            int crackerWidth = screenWidth / 20;

            for (int i = 0; i < 100; i++)
            {
                cracker = new Target(screenWidth, screenHeight, crackerTexture, crackerWidth, 0, 0);
                gameSprites.Add(cracker);
                crackers.Add(cracker);
            }

            for (int i = 0; i < 10; i++)
            {
                cracker = new BadTarget(screenWidth, screenHeight, crackerTexture, crackerWidth, 0, 0);
                gameSprites.Add(cracker);
                crackers.Add(cracker);
            }

            int cheeseWidth = screenWidth / 15;
            cheese = new Mover(screenWidth, screenHeight, cheeseTexture, cheeseWidth, screenWidth / 2, screenHeight / 2, 500, 500);
            gameSprites.Add(cheese);

            // go to the start screen state

            messageString = "Press space to start";
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


     

        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (currentState == GameStates.Gameplay)
            { updateGamePlay(); }
            else
            { updateStartScreen(); }

            base.Update(gameTime);
        }

        private void updateGamePlay()
        {
            KeyboardState keys = Keyboard.GetState();




                if (keys.IsKeyDown(Keys.Up))
                {
                    cheese.StartMovingUp();
                }
                else
                {
                    cheese.StopMovingUp();
                }

                if (keys.IsKeyDown(Keys.Down))
                {
                    cheese.StartMovingDown();
                }
                else
                {
                    cheese.StopMovingDown();
                }

                if (keys.IsKeyDown(Keys.Left))
                {
                    cheese.StartMovingLeft();
                }
                else
                {
                    cheese.StopMovingLeft();
                }

                if (keys.IsKeyDown(Keys.Right))
                {
                    cheese.StartMovingRight();
                }
                else
                {
                    cheese.StopMovingRight();
                }

                foreach (Sprite s in gameSprites)
                {
                    s.Update(1.0f / 60.0f);
                }
                foreach (Target t in crackers)
                {
                    if (cheese.IntersectsWith(t))
                    {
                        BurpSound.Play();
                        t.Reset();
                        score = score + t.Score;
                    }
                }

                timer = timer - 1;

                int secsLeft = timer / 60;
                messageString = "Time: " + secsLeft.ToString() + " Score: " + score;

                if (timer == 0)
                {
                    messageString = " Game Over : Press Space to exit   Score: " + score.ToString();
                    gameOver();
                }
           
        }




        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (currentState == GameStates.Gameplay)
            { drawGamePlay(); }
            else
            { drawStartScreen(); }

            base.Draw(gameTime);
        }

        private void drawGamePlay()
        {
            spriteBatch.Begin();

            foreach (Sprite s in gameSprites)
            {
                s.Draw(spriteBatch);
            }
            float xPos = (screenWidth - messageFont.MeasureString(messageString).X) / 2;

            Vector2 statusPos = new Vector2(xPos, 10);

            spriteBatch.DrawString(messageFont, messageString, statusPos, Color.Red);

            spriteBatch.End();
        }

        private void updateStartScreen()
        {
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Space))
            {
                startPlayingGame();
            }
        }

        private void drawStartScreen()
        {
            spriteBatch.Begin();

            foreach (Sprite s in gameSprites)
            {
                s.Draw(spriteBatch);
            }
            float xPos = (screenWidth - messageFont.MeasureString(messageString).X) / 2;

            Vector2 statusPos = new Vector2(xPos, 10);

            spriteBatch.DrawString(messageFont, messageString, statusPos, Color.Red);

            spriteBatch.End();
        }

        private void gameOver()
        {
            currentState = GameStates.StartScreen;
            messageString = "Game Over. Press space to start again.     Score: " + score;
        }
    }
}
