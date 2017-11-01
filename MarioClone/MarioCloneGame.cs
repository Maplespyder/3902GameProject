using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MarioClone.Controllers;
using MarioClone.Commands;
using Microsoft.Xna.Framework.Content;
using MarioClone.Factories;
using MarioClone.Collision;
using MarioClone.Level;
using MarioClone.GameObjects;
using System.IO;
using MarioClone.Cam;

namespace MarioClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MarioCloneGame : Game
	{
		static GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Camera camera;
        
        static ContentManager _content;
        GameGrid gameGrid;
        List<AbstractController> controllerList;
        LevelCreator level;
		private Background _background;

		public MarioCloneGame()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 1600;
			graphics.PreferredBackBufferHeight = 960;
			_content = Content;
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
            controllerList = new List<AbstractController>
            {
                new GamepadController(PlayerIndex.One),
                new GamepadController(PlayerIndex.Two),
                new GamepadController(PlayerIndex.Three),
                new GamepadController(PlayerIndex.Four)
            };

			camera = new Camera(GraphicsDevice.Viewport);
			camera.Limits = new Rectangle(0, 0, 4800, 960); //set limit of world
			gameGrid = new GameGrid(12, 4800, camera);
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
			_background = new Background(spriteBatch, camera);

			GameContent.Load<Texture2D>("Sprites/ItemSpriteSheet");
            GameContent.Load<Texture2D>("Sprites/FireFlower");
            GameContent.Load<Texture2D>("Sprites/Coin");
            GameContent.Load<Texture2D>("Sprites/SmallMario");
            GameContent.Load<Texture2D>("Sprites/BigMario");
            GameContent.Load<Texture2D>("Sprites/SuperMario");
            GameContent.Load<Texture2D>("Sprites/AllBlocks");
            GameContent.Load<Texture2D>("Sprites/Goomba");
            GameContent.Load<Texture2D>("Sprites/GreenKoopa");
            GameContent.Load<Texture2D>("Sprites/RedKoopa");        
        }

        protected override void BeginRun()
        {
            var keyboard = new KeyboardController();

            level = new LevelCreator(@"Level\Sprint3Attempt2.bmp", gameGrid);
            level.Create();

            AbstractGameObject.DrawHitbox = false;

            keyboard.AddInputCommand((int)Keys.U, new BecomeSuperMarioCommand(Mario.Instance));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.U, new BecomeSuperMarioCommand(Mario.Instance));
            keyboard.AddInputCommand((int)Keys.Y, new BecomeNormalMarioCommand(Mario.Instance));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.Y, new BecomeNormalMarioCommand(Mario.Instance));
            keyboard.AddInputCommand((int)Keys.I, new BecomeFireMarioCommand(Mario.Instance));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.I, new BecomeFireMarioCommand(Mario.Instance));
            keyboard.AddInputCommand((int)Keys.O, new BecomeDeadMarioCommand(Mario.Instance));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.O, new BecomeDeadMarioCommand(Mario.Instance));

            keyboard.AddInputCommand((int)Keys.W, new JumpCommand(Mario.Instance));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.W, new JumpCommand(Mario.Instance));
            keyboard.AddInputCommand((int)Keys.Up, new JumpCommand(Mario.Instance));


            keyboard.AddInputCommand((int)Keys.A, new MoveLeftCommand(Mario.Instance));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.A, new MoveLeftCommand(Mario.Instance));
            keyboard.AddInputCommand((int)Keys.Left, new MoveLeftCommand(Mario.Instance));


            keyboard.AddInputCommand((int)Keys.S, new CrouchCommand(Mario.Instance));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.S, new CrouchCommand(Mario.Instance));
            keyboard.AddInputCommand((int)Keys.Down, new CrouchCommand(Mario.Instance));


            keyboard.AddInputCommand((int)Keys.D, new MoveRightCommand(Mario.Instance));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.D, new MoveRightCommand(Mario.Instance));
            keyboard.AddInputCommand((int)Keys.Right, new MoveRightCommand(Mario.Instance));

            keyboard.AddReleasedInputCommand((int)Keys.S, new ReleaseCrouchCommand(Mario.Instance));
            keyboard.AddReleasedInputCommand((int)Keys.Down, new ReleaseCrouchCommand(Mario.Instance));

            keyboard.AddReleasedInputCommand((int)Keys.A, new ReleaseMoveLeftCommand(Mario.Instance));
            keyboard.AddReleasedInputCommand((int)Keys.Left, new ReleaseMoveLeftCommand(Mario.Instance));

            keyboard.AddReleasedInputCommand((int)Keys.D, new ReleaseMoveRightCommand(Mario.Instance));
            keyboard.AddReleasedInputCommand((int)Keys.Right, new ReleaseMoveRightCommand(Mario.Instance));

            keyboard.AddInputCommand((int)Keys.Q, new ExitCommand(this));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.Q, new ExitCommand(this));
            keyboard.AddInputCommand((int)Keys.C, new DisplayHitboxCommand());
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.C, new DisplayHitboxCommand());
            keyboard.AddInputCommand((int)Keys.R, new ResetLevelCommand(this));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.R, new ResetLevelCommand(this));

            // Add commands to gamepads
            AddCommandToAllGamepads(Buttons.Back, new ExitCommand(this));
            AddCommandToAllGamepads(Buttons.DPadUp, new JumpCommand(Mario.Instance));
            AddCommandToAllGamepads(Buttons.DPadDown, new CrouchCommand(Mario.Instance));
            AddCommandToAllGamepads(Buttons.DPadRight, new MoveRightCommand(Mario.Instance));
            AddCommandToAllGamepads(Buttons.DPadLeft, new MoveLeftCommand(Mario.Instance));

            foreach (var gamepad in controllerList)
            {
                gamepad.AddReleasedInputCommand((int)Buttons.DPadDown, new ReleaseCrouchCommand(Mario.Instance));
                gamepad.AddReleasedInputCommand((int)Buttons.DPadRight, new ReleaseMoveRightCommand(Mario.Instance));
                gamepad.AddReleasedInputCommand((int)Buttons.DPadLeft, new ReleaseMoveLeftCommand(Mario.Instance));
            }

            // Add keyboard to list of gamepads
            controllerList.Add(keyboard);
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
            
			foreach(var controller in controllerList)
            {
                controller.UpdateAndExecuteInputs();
            }
  
			if (!paused)
			{
                List<AbstractGameObject> collidables = gameGrid.GetCurrentMovingAndPlayerGameObjects;
                List<AbstractGameObject> removed = CollisionManager.ProcessFrame(gameTime, collidables, gameGrid);

                List<AbstractGameObject> otherObjects = gameGrid.GetAllCurrentStaticGameObjects;
                foreach(AbstractGameObject obj in otherObjects)
                {
                    if(obj.Update(gameTime, 1))
                    {
                        removed.Add(obj);
                    }
                }

                foreach(AbstractGameObject obj in removed)
                {
                    gameGrid.Remove(obj);
                }

                camera.LookAt(Mario.Instance.Position);
                gameGrid.CurrentLeftSideViewPort = camera.Position.X;
                base.Update(gameTime);
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// Somewhere in your LoadContent() method:
			if (!paused)
			{
				Vector2 parallax = new Vector2(1.0f);
				GraphicsDevice.Clear(Color.LightSkyBlue);
				_background.Draw();
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(parallax));
                DrawWorld(gameTime);
				spriteBatch.End();

				base.Draw(gameTime);
			}
		}

        private void DrawWorld(GameTime gameTime)
        {
            List<AbstractGameObject> allObjects = gameGrid.GetAllCurrentGameObjects;
            foreach (var obj in allObjects)
            {
                obj.Draw(spriteBatch, gameTime);
            }
        }

        public static ContentManager GameContent
        {
            get { return _content; }
        }

		public static GraphicsDeviceManager ReturnGraphicsDevice
		{
			get { return graphics; }
		}

		private static bool paused = false;
		public static bool Paused
		{
			set { paused = value; }
			get { return paused; }
		}

		public void ExitCommand()
        {
            Exit();
        }

        public void ResetLevelCommand()
        {
            gameGrid = new GameGrid(12, 4800, camera);
            level.Grid = gameGrid;
            level.Create();
        }

        private void AddCommandToAllGamepads(Buttons button, ICommand command)
        {
            foreach (var gamepad in controllerList)
            {
                gamepad.AddInputCommand((int)button, command);
            }
        }
    }
}