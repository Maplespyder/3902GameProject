using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioClone.IController;
using MarioClone.ISprite;
using MarioClone.Command;
using System.Collections.Generic;

namespace MarioClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MarioCloneGame : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        KeyboardController keyboardController;
        GamepadController gamepadController;

        List<ISprite.ISprite> spriteList;

		public MarioCloneGame()
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
            keyboardController = new KeyboardController();
            gamepadController = new GamepadController(PlayerIndex.One);

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

            spriteList = new List<ISprite.ISprite>();

            keyboardController.AddInputCommand((int)Keys.Q, new ExitCommand(this));
            gamepadController.AddInputCommand((int)Buttons.Start, new ExitCommand(this));

            // TODO: use this.Content to load your game content here
            ISprite.ISprite hunkyDory = new MotionlessSprite(Content.Load<Texture2D>("Sprites/hunkydory"), new Vector2(0, 0));
            keyboardController.AddInputCommand((int)Keys.W, new ToggleSpriteCommand(hunkyDory));
            gamepadController.AddInputCommand((int)Buttons.A, new ToggleSpriteCommand(hunkyDory));
            spriteList.Add(hunkyDory);

            ISprite.ISprite sonicIdle = new AnimatedUnmovingSprite(Content.Load<Texture2D>("Sprites/sonicidle"),
                new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), 1, 16);
            keyboardController.AddInputCommand((int)Keys.E, new ToggleSpriteCommand(sonicIdle));
            gamepadController.AddInputCommand((int)Buttons.B, new ToggleSpriteCommand(sonicIdle));
            spriteList.Add(sonicIdle);

            ISprite.ISprite spinball = new UnanimatedMovingSprite(Content.Load<Texture2D>("Sprites/spinball"), 
                new Vector2(0, 0), new Vector2(2, 1));

            keyboardController.AddInputCommand((int)Keys.R, new ToggleSpriteCommand(spinball));
            gamepadController.AddInputCommand((int)Buttons.X, new ToggleSpriteCommand(spinball));
            spriteList.Add(spinball);

            ISprite.ISprite mario = new AnimatedMovingSprite(Content.Load<Texture2D>("Sprites/mario"), 
                new Vector2(0, graphics.PreferredBackBufferHeight / 2), new Vector2(2, 0), 1, 4);

            keyboardController.AddInputCommand((int)Keys.T, new ToggleSpriteCommand(mario));
            gamepadController.AddInputCommand((int)Buttons.Y, new ToggleSpriteCommand(mario));
            spriteList.Add(mario);
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

            // TODO: Add your update logic here
            keyboardController.UpdateAndExecuteInputs();
            gamepadController.UpdateAndExecuteInputs();

            foreach (ISprite.ISprite sprite in spriteList)
            {
                if (sprite.Visible)
                {
                    if(sprite.Position.X < graphics.PreferredBackBufferWidth 
                        && (sprite.Position.Y < graphics.PreferredBackBufferHeight))
                    {
                        sprite.Update();
                    }
                    else
                    {
                        sprite.ToggleVisible();
                    }
                }
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

            spriteBatch.Begin();
            foreach (ISprite.ISprite sprite in spriteList)
            {
                if (sprite.Visible)
                {
                    if ((sprite.Position.X < graphics.PreferredBackBufferWidth)
                        && (sprite.Position.Y < graphics.PreferredBackBufferHeight))
                    {
                        spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.GetCurrentFrame(), Color.White);
                    } 
                    else
                    {
                        sprite.ToggleVisible();
                    }
                }
            }
            spriteBatch.End();
			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}


        public void ExitCommand()
        {
            Exit();
        }
    }
}
