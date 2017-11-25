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
using System.Linq;

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

        static ContentManager _content;
        GameGrid gameGrid;
        List<AbstractController> controllerList;
        static LevelCreator level;

        bool transitioningAreaP1;
        bool transitioningAreaP2;
        float opacityChangeTimeDeltaP1;
        float opacityChangeTimeDeltaP2;
        PlayerWarpingEventArgs warpArgsP1;
        PlayerWarpingEventArgs warpArgsP2;
        int warpOpacityP1;
        int warpOpacityP2;
        int opacityChangeP1;
        int opacityChangeP2;

        private Background _backgroundP1;
        private Background _backgroundP2;
        private Viewport player1Viewport;
        private Viewport player2Viewport;
            
		public MarioCloneGame()
		{  
            State = GameState.Playing;
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 1920;
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

            player1Viewport = new Viewport(0, 0, graphics.PreferredBackBufferWidth / 2 - 20, graphics.PreferredBackBufferHeight);

            Player1Camera = new Camera(player1Viewport);
            Player1Camera.Limits = new Rectangle(0, 0, 350 * 64, graphics.PreferredBackBufferWidth); //set limit of world
            gameGrid = new GameGrid(24, Player1Camera);

            player2Viewport = new Viewport(graphics.PreferredBackBufferWidth / 2 + 20, 0, graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight);
            Player2Camera = new Camera(player2Viewport);
            Player2Camera.Limits = new Rectangle(0, 0, 350 * 64, graphics.PreferredBackBufferWidth); //set limit of world

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
            _backgroundP1 = new Background(spriteBatch, Player1Camera, BackgroundType.Overworld);
            _backgroundP2 = new Background(spriteBatch, Player2Camera, BackgroundType.Overworld);
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
            Player1Camera.Limits = level.LevelAreas[0];
            Player2Camera.Limits = level.LevelAreas[0];
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

            foreach (var controller in controllerList)
            {
                controller.UpdateAndExecuteInputs();
            }

            if (State == GameState.Playing)
            {
                Player1Camera.LookAt(Player1.Position);
                List<AbstractGameObject> collidables = new List<AbstractGameObject>();
                if (!transitioningAreaP1)
                {
                    gameGrid.CurrentLeftSideViewPort = Player1Camera.Position.X;
                    gameGrid.CurrentTopSideViewPort = Player1Camera.Position.Y;
                    collidables = gameGrid.GetCurrentMovingAndPlayerGameObjects;
                }

                Player2Camera.LookAt(Player2.Position);
                if (!transitioningAreaP2)
                {
                    gameGrid.CurrentLeftSideViewPort = Player2Camera.Position.X;
                    gameGrid.CurrentTopSideViewPort = Player2Camera.Position.Y;
                    collidables = collidables.Union(gameGrid.GetCurrentMovingAndPlayerGameObjects).ToList();
                }
                
                if(transitioningAreaP1)
                {
                    collidables.RemoveAll((x) => ReferenceEquals(x, Player1));
                }
                if(transitioningAreaP2)
                {
                    collidables.RemoveAll((x) => ReferenceEquals(x, Player2));
                }

                List<AbstractGameObject> removed = CollisionManager.ProcessFrame(gameTime, collidables, gameGrid);
                
                Player1Camera.LookAt(Player1.Position);
                List<AbstractGameObject> otherObjects = new List<AbstractGameObject>();
                if (!transitioningAreaP1)
                {
                    gameGrid.CurrentLeftSideViewPort = Player1Camera.Position.X;
                    gameGrid.CurrentTopSideViewPort = Player1Camera.Position.Y;
                    otherObjects = gameGrid.GetAllCurrentStaticGameObjects;
                }

                Player2Camera.LookAt(Player2.Position);
                if(!transitioningAreaP2)
                {
                    gameGrid.CurrentLeftSideViewPort = Player2Camera.Position.X;
                    gameGrid.CurrentTopSideViewPort = Player2Camera.Position.Y;
                    otherObjects = otherObjects.Union(gameGrid.GetAllCurrentStaticGameObjects).ToList();
                }

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


                foreach (HUD hud in HUDs)
                {
                    hud.Update(gameTime);
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
                if (transitioningAreaP1)
                {
                    if (warpOpacityP1 > 255)
                    {
                        warpOpacityP1 = 255;
                        opacityChangeP1 = -5;
                        UpdateCameraForWarp(gameTime, warpArgsP1);
                    }
                    else if (warpOpacityP1 <= 0)
                    {
                        transitioningAreaP1 = false;
                        warpArgsP1 = null;
                        Player1.StateMachine.TransitionIdle();
                    }
                }

                if (transitioningAreaP2)
                {
                    if (warpOpacityP2 > 255)
                    {
                        warpOpacityP2 = 255;
                        opacityChangeP2 = -5;
                        UpdateCameraForWarp(gameTime, warpArgsP2);
                    }
                    else if (warpOpacityP2 <= 0)
                    {
                        transitioningAreaP2 = false;
                        warpArgsP2 = null;
                        Player2.StateMachine.TransitionIdle();
                    }
                }

                Vector2 parallax = new Vector2(1.0f);
				GraphicsDevice.Clear(Color.LightSkyBlue);


                GraphicsDevice.Viewport = player1Viewport;
                _backgroundP1.Draw();

                gameGrid.CurrentLeftSideViewPort = Player1Camera.Position.X;
                gameGrid.CurrentTopSideViewPort = Player1Camera.Position.Y;
                List<AbstractGameObject> player1Objects = gameGrid.GetAllCurrentGameObjects;

                spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Player1Camera.GetViewMatrix(parallax));
                if (transitioningAreaP1)
                {
                    opacityChangeTimeDeltaP1 += gameTime.ElapsedGameTime.Milliseconds;
                    if (opacityChangeTimeDeltaP1 >= 1)
                    {
                        opacityChangeTimeDeltaP1 = 0;
                        warpOpacityP1 += opacityChangeP1;
                    }
                }
                DrawPlayerHalfOfGame(gameTime, HUDs[0], Player1Camera, player1Objects, transitioningAreaP1, warpOpacityP1);
                spriteBatch.End();


                GraphicsDevice.Viewport = player2Viewport;
                _backgroundP2.Draw();

                gameGrid.CurrentLeftSideViewPort = Player2Camera.Position.X;
                gameGrid.CurrentTopSideViewPort = Player2Camera.Position.Y;
                List<AbstractGameObject> player2Objects = gameGrid.GetAllCurrentGameObjects;

                spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Player2Camera.GetViewMatrix(parallax));
                //TODO probably remove time delta
                if (transitioningAreaP2)
                {
                    opacityChangeTimeDeltaP2 += gameTime.ElapsedGameTime.Milliseconds;
                    if (opacityChangeTimeDeltaP2 >= 1)
                    {
                        opacityChangeTimeDeltaP2 = 0;
                        warpOpacityP2 += opacityChangeP2;
                    }
                }
                DrawPlayerHalfOfGame(gameTime, HUDs[1], Player2Camera, player2Objects, transitioningAreaP2, warpOpacityP2);
                spriteBatch.End();


                GraphicsDevice.Viewport = new Viewport(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

                spriteBatch.Begin(SpriteSortMode.BackToFront);
                using (Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1))
                {
                    Color[] color = { Color.Crimson };
                    pixel.SetData(color);
                    spriteBatch.Draw(pixel, new Rectangle(player1Viewport.Width, 0, 
                        player2Viewport.X - player1Viewport.Width, graphics.PreferredBackBufferHeight), Color.White);
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
        
        void DrawPlayerHalfOfGame(GameTime gameTime, HUD hud, Camera camera, List<AbstractGameObject> drawnObjects, bool transitioning, int opacity)
        {
            hud.Draw(spriteBatch, gameTime);

            foreach (var obj in drawnObjects)
            {
                obj.Draw(spriteBatch, gameTime);
            }

            if (transitioning)
            {
                //Fade to black
                using (Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1))
                {
                    Color[] color = { Color.Black };
                    pixel.SetData(color);
                    spriteBatch.Draw(pixel, camera.Limits.Value, new Color(Color.Black, opacity));
                }
            }
        }

        private void HandlePlayerDeath(object sender, PlayerDiedEventArgs e)
        {
            Camera camera = Player1Camera;
            if(ReferenceEquals(e.DeadPlayer, Player2))
            {
                camera = Player2Camera;
            }

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
                if (ReferenceEquals(e.DeadPlayer, Player1))
                {
                    _backgroundP1 = new Background(spriteBatch, camera, BackgroundType.Overworld);
                }
                else
                {
                    _backgroundP2 = new Background(spriteBatch, camera, BackgroundType.Overworld);
                }
            }
        }
        
        private void PauseForWarp(object sender, PlayerWarpingEventArgs e)
        {
            if (ReferenceEquals(e.Warper, Player1))
            {
                transitioningAreaP1 = true;
                warpOpacityP1 = 1;
                opacityChangeP1 = 5;
                warpArgsP1 = e;
                e.Warper.BecomeWarp();
            }
            else if (ReferenceEquals(e.Warper, Player2))
            {
                transitioningAreaP2 = true;
                warpOpacityP2 = 1;
                opacityChangeP2 = 5;
                warpArgsP2 = e;
                e.Warper.BecomeWarp();
            }
        }
        private void UpdateCameraForWarp(GameTime gameTime, PlayerWarpingEventArgs e)
        {
            Camera camera = Player1Camera;

            if (ReferenceEquals(e.Warper, Player2))
            {
                camera = Player2Camera;
            }
            
            camera.Limits = level.LevelAreas[e.WarpExit.LevelArea];

            if (e.WarpExit.LevelArea != 0)
            {
                if (ReferenceEquals(e.Warper, Player2))
                {
                    _backgroundP2 = new Background(spriteBatch, camera, BackgroundType.Underworld);
                }
                else
                {
                    _backgroundP1 = new Background(spriteBatch, camera, BackgroundType.Underworld);
                }
                BlockFactory.SpriteFactory = SubThemedBlockSpriteFactory.Instance;
            }
            else
            {
                if (ReferenceEquals(e.Warper, Player2))
                {
                    _backgroundP2 = new Background(spriteBatch, camera, BackgroundType.Overworld);
                }
                else
                {
                    _backgroundP1 = new Background(spriteBatch, camera, BackgroundType.Overworld);
                }
                BlockFactory.SpriteFactory = NormalThemedBlockSpriteFactory.Instance;
            }

            gameGrid.Remove(e.Warper);
            e.Warper.Position = e.WarpExit.Position - new Vector2(0, e.Warper.Sprite.SourceRectangle.Height / 2);
            e.Warper.Update(gameTime, 1);
            gameGrid.Add(e.Warper);

            camera.LookAt(e.Warper.Position);

            /*gameGrid.CurrentLeftSideViewPort = camera.Position.X;
            gameGrid.CurrentTopSideViewPort = camera.Position.Y;*/
		}

        public static ContentManager GameContent
        {
            get { return _content; }
        }

		public static GraphicsDeviceManager ReturnGraphicsDevice
		{
			get { return graphics; }
		}
        
        public static Camera Player1Camera { get; set; }

        public static Camera Player2Camera { get; set; }

        public static List<HUD> HUDs { get; private set; }

        public static Mario Player1 { get; set; }

        public static Mario Player2 { get; set; }


		public void ExitCommand()
        {
            Exit();
        }

        public void ResetLevelCommand()
        {
            Player1Camera.Limits = level.LevelAreas[0];
            _backgroundP1 = new Background(spriteBatch, Player1Camera, BackgroundType.Overworld);

            Player2Camera.Limits = level.LevelAreas[0];
            _backgroundP2 = new Background(spriteBatch, Player2Camera, BackgroundType.Overworld);

            gameGrid.ClearGrid();
			SoundPool.Instance.Reset();
			foreach (HUD hud in HUDs)
            {
                hud.Dispose();
            }
            HUDs.Clear();
            level.Grid = gameGrid;
            level.Create();

            Player1Camera.LookAt(Player1.Position);
            Player2Camera.LookAt(Player2.Position);
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