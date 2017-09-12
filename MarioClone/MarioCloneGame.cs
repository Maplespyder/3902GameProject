using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MarioClone.Controllers;
using MarioClone.Commands;
using MarioClone.Sprites;

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

        List<Sprite> spriteList;

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

            spriteList = new List<Sprite>();

            keyboardController.AddInputCommand((int)Keys.Q, new ExitCommand(this));
            gamepadController.AddInputCommand((int)Buttons.Start, new ExitCommand(this));

            var gameBounds = new List<Rectangle>()
            {
                new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)
            };

            // TODO: use this.Content to load your game content here
            var hunkyDory = new MotionlessSprite(Content.Load<Texture2D>("Sprites/hunkydory"), new Vector2(100, 400), new Vector2(0, 0), gameBounds, false);
            keyboardController.AddInputCommand((int)Keys.W, new ToggleSpriteCommand(hunkyDory));
            gamepadController.AddInputCommand((int)Buttons.A, new ToggleSpriteCommand(hunkyDory));
            spriteList.Add(hunkyDory);

            var sonicIdle = new AnimatedUnmovingSprite(Content.Load<Texture2D>("Sprites/sonicidle"), new Vector2(200, 400), new Vector2(0, 0), gameBounds, false, 1, 16, .1f, 39, 32, 16);
            keyboardController.AddInputCommand((int)Keys.E, new ToggleSpriteCommand(sonicIdle));
            gamepadController.AddInputCommand((int)Buttons.B, new ToggleSpriteCommand(sonicIdle));
            spriteList.Add(sonicIdle);

            var spinball = new UnanimatedMovingSprite(Content.Load<Texture2D>("Sprites/spinball"), new Vector2(100, 400), new Vector2(0, 50), gameBounds, false);
            keyboardController.AddInputCommand((int)Keys.R, new ToggleSpriteCommand(spinball));
            gamepadController.AddInputCommand((int)Buttons.X, new ToggleSpriteCommand(spinball));
            spriteList.Add(spinball);

            var mario = new AnimatedMovingSprite(Content.Load<Texture2D>("Sprites/mario"), new Vector2(200, 400), new Vector2(50, 0), gameBounds, false, 1, 4, .1f, 40, 26, 4);
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

            keyboardController.UpdateAndExecuteInputs();
            gamepadController.UpdateAndExecuteInputs();

            foreach (var sprite in spriteList)
            {
                sprite.Update(gameTime);
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
            foreach (var sprite in spriteList)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();

			base.Draw(gameTime);
		}

        public void ExitCommand()
        {
            Exit();
        }
    }
}
