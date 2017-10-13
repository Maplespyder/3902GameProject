using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MarioClone.Controllers;
using MarioClone.Commands;
using MarioClone.Sprites;
using Microsoft.Xna.Framework.Content;
using MarioClone.Factories;
using MarioClone.GameObjects;
using MarioClone.Collision;

namespace MarioClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MarioCloneGame : Game
	{
		static GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        
        static ContentManager _content;
        GameGrid gameGrid;
        List<AbstractController> controllerList;

		public MarioCloneGame()
		{
			graphics = new GraphicsDeviceManager(this);
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

            gameGrid = new GameGrid(12, 16, 800);

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
            AbstractController keyboard = new KeyboardController();

            keyboard.AddInputCommand((int)Keys.Q, new ExitCommand(this));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.Q, new ExitCommand(this));
            keyboard.AddInputCommand((int)Keys.C, new DisplayHitboxCommand());
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.C, new DisplayHitboxCommand());
            AddCommandToAllGamepads(Buttons.Back, new ExitCommand(this));
            
            var mario = MarioFactory.Create(new Vector2(200, 100));

            keyboard.AddInputCommand((int)Keys.U, new BecomeSuperMarioCommand(mario));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.U, new BecomeSuperMarioCommand(mario));
            keyboard.AddInputCommand((int)Keys.Y, new BecomeNormalMarioCommand(mario));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.Y, new BecomeNormalMarioCommand(mario));
            keyboard.AddInputCommand((int)Keys.I, new BecomeFireMarioCommand(mario));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.I, new BecomeFireMarioCommand(mario));
            keyboard.AddInputCommand((int)Keys.O, new BecomeDeadMarioCommand(mario));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.O, new BecomeDeadMarioCommand(mario));

            keyboard.AddInputCommand((int)Keys.W, new JumpCommand(mario));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.W, new JumpCommand(mario));
            keyboard.AddInputCommand((int)Keys.Up, new JumpCommand(mario));
            AddCommandToAllGamepads(Buttons.A, new JumpCommand(mario));

            keyboard.AddInputCommand((int)Keys.A, new MoveLeftCommand(mario));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.A, new MoveLeftCommand(mario));
            keyboard.AddInputCommand((int)Keys.Left, new MoveLeftCommand(mario));
            AddCommandToAllGamepads(Buttons.DPadLeft, new JumpCommand(mario));

            keyboard.AddInputCommand((int)Keys.S, new CrouchCommand(mario));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.S, new CrouchCommand(mario));
            keyboard.AddInputCommand((int)Keys.Down, new CrouchCommand(mario));
            AddCommandToAllGamepads(Buttons.DPadDown, new JumpCommand(mario));

            keyboard.AddInputCommand((int)Keys.D, new MoveRightCommand(mario));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.D, new MoveRightCommand(mario));
            keyboard.AddInputCommand((int)Keys.Right, new MoveRightCommand(mario));
            AddCommandToAllGamepads(Buttons.DPadRight, new JumpCommand(mario));

			gameGrid.Add(mario);

            var BrickBlock = BlockFactory.Instance.Create(BlockType.BreakableBrick, new Vector2(200, 200));
            keyboard.AddInputCommand((int)Keys.B, new BlockBumpCommand(BrickBlock));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.B, new BlockBumpCommand(BrickBlock));
            gameGrid.Add(BrickBlock);

            var QuestionBlock = BlockFactory.Instance.Create(BlockType.QuestionBlock, new Vector2(250, 200));
            keyboard.AddInputCommand((int)Keys.X, new BlockBumpCommand(QuestionBlock));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.X, new BlockBumpCommand(QuestionBlock));
            gameGrid.Add(QuestionBlock);

            var HiddenBlock = BlockFactory.Instance.Create(BlockType.HiddenBlock, new Vector2(300, 200));
            keyboard.AddInputCommand((int)Keys.H, new ShowHiddenBrickCommand(HiddenBlock));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.H, new ShowHiddenBrickCommand(HiddenBlock));
            gameGrid.Add(HiddenBlock);

            var FloorBlock = BlockFactory.Instance.Create(BlockType.FloorBlock, new Vector2(350, 200));
            gameGrid.Add(FloorBlock);

            var StairBlock = BlockFactory.Instance.Create(BlockType.StairBlock, new Vector2(400, 200));
            gameGrid.Add(StairBlock);

            var UsedBlock = BlockFactory.Instance.Create(BlockType.UsedBlock, new Vector2(450, 200));
            keyboard.AddInputChord((int)Modifier.LeftShift, (int)Keys.X, new BlockBumpCommand(UsedBlock));
            keyboard.AddInputCommand((int)Keys.X, new BlockBumpCommand(UsedBlock));
            gameGrid.Add(UsedBlock);

            var goomba = EnemyFactory.Instance.Create(EnemyType.Goomba, new Vector2(200, 300));
            gameGrid.Add(goomba);

            var GreenKoopa = EnemyFactory.Instance.Create(EnemyType.GreenKoopa, new Vector2(250, 300));
            gameGrid.Add(GreenKoopa);

            var RedKoopa = EnemyFactory.Instance.Create(EnemyType.RedKoopa, new Vector2(300, 300));
            gameGrid.Add(RedKoopa);

            var coin = PowerUpFactory.Create(PowerUpType.Coin, new Vector2(200, 400));
            gameGrid.Add(coin);

            var flower = PowerUpFactory.Create(PowerUpType.Flower, new Vector2(250, 400));
            gameGrid.Add(flower);

            var GreenMushroom = PowerUpFactory.Create(PowerUpType.GreenMushroom, new Vector2(300, 400));
            gameGrid.Add(GreenMushroom);

            var redMushroom = PowerUpFactory.Create(PowerUpType.RedMushroom, new Vector2(350, 400));
            gameGrid.Add(redMushroom);

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
                gameGrid.UpdateWorld(gameTime);
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
				GraphicsDevice.Clear(Color.CornflowerBlue);
				spriteBatch.Begin();
                gameGrid.DrawWorld(spriteBatch, gameTime);
				spriteBatch.End();

				base.Draw(gameTime);
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

        private void AddCommandToAllGamepads(Buttons button, ICommand command)
        {
            foreach (var gamepad in controllerList)
            {
                gamepad.AddInputCommand((int)button, command);
            }
        }
    }
}
