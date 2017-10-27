﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_SimpleSample
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
	enum GameState 
	{
		playing,
		paused
	}
	
	
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D playerTexture;
        AnimatedSprite playerSprite;
		GameState currentGameState = GameState.playing;
		
		SpriteFont spriteFont;
        bool isPauseKeyHeld = false;

        Sprite testSprite;
        //TestSprite playerSprite;
        string collisionText = "";
        SpriteFont HUDFont;


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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("professor_walk_cycle_no_hat");

            playerSprite = new AnimatedSprite(playerTexture, Vector2.Zero, 4, 9);
            testSprite = new Sprite(playerTexture, new Vector2(300, 200));
            HUDFont = Content.Load<SpriteFont>("HUDFont");
            //playerSprite = new TestSprite(playerTexture, Vector2.Zero);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyboardState = Keyboard.GetState();


            if( keyboardState.IsKeyDown(Keys.P) && !isPauseKeyHeld)
            {

                if (currentGameState == GameState.playing)
                        currentGameState = GameState.paused;
                else currentGameState = GameState.playing;
            }


            //This should be in the Input Manager - differentiate between pressed and held
            isPauseKeyHeld = keyboardState.IsKeyUp(Keys.P) ? false : true;



            // TODO: Add your update logic here
            switch (currentGameState)
			{
				case GameState.playing:
				{
					playerSprite.Update(gameTime);
                    testSprite.Update(gameTime);
                    collisionText = playerSprite.IsCollidingWith(testSprite) ? "there is a collision" : " there is no collision";

                    //check collisions

                    collisionText = playerSprite.IsCollidingWith(testSprite) ? "there is a collision" : " there is no collision";
                }
				break;
				
				case GameState.paused:
				{
					
				}
				
				break;
				
			}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();


			
			switch (currentGameState)
			{
				case GameState.playing:
				{
					playerSprite.Draw(spriteBatch);
                    testSprite.Draw(spriteBatch);
                    spriteBatch.DrawString(HUDFont, collisionText, new Vector2(300, 0), Color.Red);
                    }
				break;
				case GameState.paused:
				{
                    spriteBatch.DrawString(HUDFont, "Game Paused", Vector2.Zero, Color.White);

                }
                break;
			}

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
