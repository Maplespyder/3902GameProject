using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MarioClone.Controllers;
using MarioClone.Commands;
using Microsoft.Xna.Framework.Content;
using MarioClone.Collision;
using MarioClone.Level;
using MarioClone.GameObjects;
using MarioClone.Cam;
using MarioClone.Sounds;
using MarioClone.HeadsUpDisplay;
using MarioClone.EventCenter;
using MarioClone.States;
using MarioClone.Menu;
using MarioClone.Factories;

namespace MarioClone
{
    public enum GameState
    {
        Playing,
        GameOver,
        Paused,
        Win
    }

    public enum MenuOption
    {
        Replay,
        Exit
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MarioCloneGame : Game
	{
        public static GameState State;

        MenuScreen screen;

		static GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Camera camera;

        bool transitioningArea;
        float timeDelta;
        PlayerWarpingEventArgs warpArgs;
        int opacity;
        int opacityChange;

        static ContentManager _content;
        GameGrid gameGrid;
        List<AbstractController> controllerList;
        static LevelCreator level;
		private Background _background;

		public MarioCloneGame()
		{  
            State = GameState.Playing;
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 1600;
			graphics.PreferredBackBufferHeight = 960;
			_content = Content;
			Content.RootDirectory = "Content";
            HUDs = new List<HUD>();
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
			camera.Limits = new Rectangle(0, 0, 350 * 64, 960); //set limit of world
            gameGrid = new GameGrid(24, camera);
            GetCamera = camera;

			new EventSounds();
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
            _background = new Background(spriteBatch, camera, BackgroundType.Overworld);
            screen = new MenuScreen(this);

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

            level = new LevelCreator(@"Level\Sprint4MainLevel.bmp", gameGrid);
            level.Create();

			AbstractGameObject.DrawHitbox = false;
            
            keyboard.AddInputCommand((int)Keys.U, new BecomeSuperMarioCommand(Player1));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.U, new BecomeSuperMarioCommand(Player1));
            keyboard.AddInputCommand((int)Keys.Y, new BecomeNormalMarioCommand(Player1));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.Y, new BecomeNormalMarioCommand(Player1));
            keyboard.AddInputCommand((int)Keys.I, new BecomeFireMarioCommand(Player1));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.I, new BecomeFireMarioCommand(Player1));
            keyboard.AddInputCommand((int)Keys.O, new BecomeDeadMarioCommand(Player1));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.O, new BecomeDeadMarioCommand(Player1));

			keyboard.AddInputCommand((int)Keys.B, new FireBallCommand(Player1));
			keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.B, new FireBallCommand(Player1));
            keyboard.AddInputCommand((int)Keys.NumPad0, new FireBallCommand(Player2));

            keyboard.AddInputCommand((int)Keys.W, new JumpCommand(Player1));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.W, new JumpCommand(Player1));
            keyboard.AddInputCommand((int)Keys.Up, new JumpCommand(Player2));


            keyboard.AddInputCommand((int)Keys.A, new MoveLeftCommand(Player1));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.A, new MoveLeftCommand(Player1));
            keyboard.AddInputCommand((int)Keys.Left, new MoveLeftCommand(Player2));


            keyboard.AddInputCommand((int)Keys.S, new CrouchCommand(Player1));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.S, new CrouchCommand(Player1));
            keyboard.AddInputCommand((int)Keys.Down, new CrouchCommand(Player2));


            keyboard.AddInputCommand((int)Keys.D, new MoveRightCommand(Player1));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.D, new MoveRightCommand(Player1));
            keyboard.AddInputCommand((int)Keys.Right, new MoveRightCommand(Player2));

            keyboard.AddReleasedInputCommand((int)Keys.S, new ReleaseCrouchCommand(Player1));
            keyboard.AddReleasedInputCommand((int)Keys.Down, new ReleaseCrouchCommand(Player2));

            keyboard.AddReleasedInputCommand((int)Keys.A, new ReleaseMoveLeftCommand(Player1));
            keyboard.AddReleasedInputCommand((int)Keys.Left, new ReleaseMoveLeftCommand(Player2));

            keyboard.AddReleasedInputCommand((int)Keys.D, new ReleaseMoveRightCommand(Player1));
            keyboard.AddReleasedInputCommand((int)Keys.Right, new ReleaseMoveRightCommand(Player2));

			keyboard.AddInputCommand((int)Keys.M, new MuteCommand(SoundPool.Instance));
			keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.M, new MuteCommand(SoundPool.Instance));
			keyboard.AddInputCommand((int)Keys.Q, new ExitCommand(this));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.Q, new ExitCommand(this));
            keyboard.AddInputCommand((int)Keys.C, new DisplayHitboxCommand());
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.C, new DisplayHitboxCommand());
            keyboard.AddInputCommand((int)Keys.R, new ResetLevelCommand(this));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.R, new ResetLevelCommand(this));
            keyboard.AddInputCommand((int)Keys.P, new PauseCommand(this));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.P, new PauseCommand(this));

            keyboard.AddInputCommand((int)Keys.Space, new MenuMoveCommand(screen));
            keyboard.AddInputCommand((int)Keys.Enter, new MenuSelectCommand(screen));

            // Add commands to gamepads
            AddCommandToAllGamepads(Buttons.Back, new ExitCommand(this));
            AddCommandToAllGamepads(Buttons.DPadUp, new JumpCommand(Player1));
            AddCommandToAllGamepads(Buttons.DPadDown, new CrouchCommand(Player1));
            AddCommandToAllGamepads(Buttons.DPadRight, new MoveRightCommand(Player1));
            AddCommandToAllGamepads(Buttons.DPadLeft, new MoveLeftCommand(Player1));

            foreach (var gamepad in controllerList)
            {
                gamepad.AddReleasedInputCommand((int)Buttons.DPadDown, new ReleaseCrouchCommand(Player1));
                gamepad.AddReleasedInputCommand((int)Buttons.DPadRight, new ReleaseMoveRightCommand(Player1));
                gamepad.AddReleasedInputCommand((int)Buttons.DPadLeft, new ReleaseMoveLeftCommand(Player1));
            }

            // Add keyboard to list of gamepads
            controllerList.Add(keyboard);

            EventManager.Instance.RaisePlayerWarpingEvent += PauseForWarp;
            EventManager.Instance.RaisePlayerDiedEvent += HandlePlayerDeath;
            camera.Limits = level.LevelAreas[0];
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
            
            if (!transitioningArea)
            {
                foreach (var controller in controllerList)
                {
                    controller.UpdateAndExecuteInputs();
                }
            }

            if (State == GameState.Playing)
            {
                if (!transitioningArea)
                {
                    List<AbstractGameObject> collidables = gameGrid.GetCurrentMovingAndPlayerGameObjects;
                    List<AbstractGameObject> removed = CollisionManager.ProcessFrame(gameTime, collidables, gameGrid);

                    List<AbstractGameObject> otherObjects = gameGrid.GetAllCurrentStaticGameObjects;
                    foreach (AbstractGameObject obj in otherObjects)
                    {
                        if (obj.Update(gameTime, 1))
                        {
                            removed.Add(obj);
                        }
                    }

                    foreach (AbstractGameObject obj in removed)
                    {
                        gameGrid.Remove(obj);
                    }

                    camera.LookAt(Player1.Position);
                    gameGrid.CurrentLeftSideViewPort = camera.Position.X;
                    gameGrid.CurrentTopSideViewPort = camera.Position.Y;

                    foreach (HUD hud in HUDs)
                    {
                        hud.Update(gameTime);
                    }
                }

                base.Update(gameTime);
            }
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			if (State == GameState.Playing)
            {
                if (transitioningArea)
                {
                    if (opacity > 255)
                    {
                        opacity = 255;
                        opacityChange = -2;
                        UpdateCameraForWarp(gameTime, warpArgs);
                    }
                    else if (opacity <= 0)
                    {
                        UnpauseForWarp();
                    }
                }

                Vector2 parallax = new Vector2(1.0f);
				GraphicsDevice.Clear(Color.LightSkyBlue);
				_background.Draw();
				spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.GetViewMatrix(parallax));
                DrawWorld(gameTime);
                foreach (HUD hud in HUDs)
                {
                    hud.Draw(spriteBatch, gameTime);
                }

                if(transitioningArea)
                {

					//Fade to black
                    using(Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1))
                    {
                        Color[] color = { Color.Black };
                        pixel.SetData(color);
                        spriteBatch.Draw(pixel, camera.Limits.Value, new Color(Color.Black, opacity));
                    }

                    timeDelta += gameTime.ElapsedGameTime.Milliseconds;
                    if(timeDelta >= 1)
                    {
                        timeDelta = 0;
                        opacity += opacityChange;
                    }
                }
                spriteBatch.End();

				base.Draw(gameTime);
			}
            else if (State == GameState.GameOver)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin(SpriteSortMode.BackToFront);

                screen.Draw(spriteBatch, gameTime);

                spriteBatch.End();
                base.Draw(gameTime);
            }
            else if (State == GameState.Win)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin(SpriteSortMode.BackToFront);

                screen.Draw(spriteBatch, gameTime);

                spriteBatch.End();
                base.Draw(gameTime);
            }
		}

        private void PauseForWarp(object sender, PlayerWarpingEventArgs e)
        {
            transitioningArea = true;
            opacity = 1;
            opacityChange = 5;
            warpArgs = e;
        }

        private void HandlePlayerDeath(object sender, PlayerDiedEventArgs e)
        {
            if (e.DeadPlayer.Lives <= 0)
            {
                State = GameState.GameOver;
            }
            else
            {
                camera.Limits = level.LevelAreas[0];
                BlockFactory.SpriteFactory = NormalThemedBlockSpriteFactory.Instance;

                gameGrid.Remove(e.DeadPlayer);
                e.DeadPlayer.ResetToCheckpoint();
                gameGrid.Add(e.DeadPlayer);

                camera.LookAt(e.DeadPlayer.Position);
                _background = new Background(spriteBatch, camera, BackgroundType.Overworld);
            }
        }

        private void UnpauseForWarp()
        {
            transitioningArea = false;
            warpArgs = null;
        }

        private void UpdateCameraForWarp(GameTime gameTime, PlayerWarpingEventArgs e)
        {

            //this will be uncommented once the level creator is done so it doesn't crash the game.
            camera.Limits = level.LevelAreas[e.WarpExit.LevelArea];
            
            if(e.WarpExit.LevelArea == 0)
            {
                BlockFactory.SpriteFactory = NormalThemedBlockSpriteFactory.Instance;
            }
            else
            {
                BlockFactory.SpriteFactory = SubThemedBlockSpriteFactory.Instance;
            }

            gameGrid.Remove(e.Warper);
            e.Warper.Position = e.WarpExit.Position - new Vector2(0, e.Warper.Sprite.SourceRectangle.Height / 2);
            e.Warper.Update(gameTime, 1);
            gameGrid.Add(e.Warper);

            camera.LookAt(e.Warper.Position);

            gameGrid.CurrentLeftSideViewPort = camera.Position.X;
            gameGrid.CurrentTopSideViewPort = camera.Position.Y;
			if (e.WarpExit.LevelArea != 0)
			{
				_background = new Background(spriteBatch, camera, BackgroundType.Underworld);
			}
			else
			{
				_background = new Background(spriteBatch, camera, BackgroundType.Overworld);
			}

		}

        void DrawWorld(GameTime gameTime)
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
        
        public static Camera GetCamera { get; set; }

        public static ICollection<HUD> HUDs { get; private set; }

        public static Mario Player1 { get; set; }

        public static Mario Player2 { get; set; }


		public void ExitCommand()
        {
            Exit();
        }

        public void ResetLevelCommand()
        {
            camera.Limits = level.LevelAreas[0];
            _background = new Background(spriteBatch, camera, BackgroundType.Overworld);

            gameGrid.ClearGrid();
			SoundPool.Instance.Reset();
			foreach (HUD hud in HUDs)
            {
                hud.Dispose();
            }
            HUDs.Clear();
            level.Grid = gameGrid;
            level.Create();

            camera.LookAt(Player1.Position);
        }

        public void PauseCommand()
        {
            if (State == GameState.Playing)
            {
                SoundPool.Instance.MuteCommand();
                State = GameState.Paused; 
            } 
            else if (State == GameState.Paused)
            {
                SoundPool.Instance.MuteCommand();
                State = GameState.Playing;
            }
        }

        public void SetAsPlaying()
        {
            State = GameState.Playing;
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