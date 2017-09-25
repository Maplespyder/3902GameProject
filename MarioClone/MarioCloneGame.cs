﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MarioClone.Controllers;
using MarioClone.Commands;
using MarioClone.Sprites;
using Microsoft.Xna.Framework.Content;
using MarioClone.Factories;
using MarioClone.GameObjects;

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
        List<IGameObject> gameObjects;
        List<AbstractController> controllers;

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

			gameObjects = new List<IGameObject>();
            controllers = new List<AbstractController>();

            KeyboardController keyboardController = new KeyboardController();
            for(int i = 0; i < 4; i++)
            {
                controllers.Add(new GamepadController((PlayerIndex)i));
            }

            foreach (AbstractController c in controllers)
            {
                c.AddInputCommand((int)Buttons.Back, new ExitCommand(this));
            }
            
			var mario = MarioFactory.Create(new Vector2(200, 400));
			keyboardController.AddInputCommand((int)Keys.U, new BecomeSuperMarioCommand(mario));
			keyboardController.AddInputCommand((int)Keys.Y, new BecomeNormalMarioCommand(mario));
			keyboardController.AddInputCommand((int)Keys.I, new BecomeFireMarioCommand(mario));
			keyboardController.AddInputCommand((int)Keys.O, new BecomeDeadMarioCommand(mario));
			keyboardController.AddInputCommand((int)Keys.W, new JumpCommand(mario));
			keyboardController.AddInputCommand((int)Keys.A, new MoveLeftCommand(mario));
			keyboardController.AddInputCommand((int)Keys.S, new CrouchCommand(mario));
			keyboardController.AddInputCommand((int)Keys.D, new MoveRightCommand(mario));
			gameObjects.Add(mario);

			// TODO: use this.Content to load your game content here

			var BrickBlock = BlockFactory.Instance.Create(BlockType.BreakableBrick, new Vector2(200, 200));
			ICommand BrickBumpCommand = new BlockBumpCommand(BrickBlock);
			keyboardController.AddInputCommand((int)Keys.B, BrickBumpCommand);
			gameObjects.Add(BrickBlock);

			var QuestionBlock = BlockFactory.Instance.Create(BlockType.QuestionBlock, new Vector2(300,300));
			ICommand QuestionBumpCommand = new BlockBumpCommand(QuestionBlock);
			keyboardController.AddInputCommand((int)Keys.X, QuestionBumpCommand);
			gameObjects.Add(QuestionBlock);

			var HiddenBlock = BlockFactory.Instance.Create(BlockType.HiddenBlock, new Vector2(100, 0));
			ICommand HiddenBlockCommand = new ShowHiddenBrickCommand(HiddenBlock);
			keyboardController.AddInputCommand((int)Keys.H, HiddenBlockCommand);
			gameObjects.Add(HiddenBlock);

			var FloorBlock = BlockFactory.Instance.Create(BlockType.FloorBlock, new Vector2(0, 100));
			gameObjects.Add(FloorBlock);

			var StairBlock = BlockFactory.Instance.Create(BlockType.StairBlock, new Vector2(40, 100));
			gameObjects.Add(StairBlock);

			var UsedBlock = BlockFactory.Instance.Create(BlockType.UsedBlock, new Vector2(80, 100));
			gameObjects.Add(UsedBlock);

			var goomba = EnemyFactory.Instance.Create(EnemyType.Goomba, new Vector2(140,0));
            gameObjects.Add(goomba);

            var GreenKoopa = EnemyFactory.Instance.Create(EnemyType.GreenKoopa, new Vector2(180, 0));
			gameObjects.Add(GreenKoopa);

            var RedKoopa = EnemyFactory.Instance.Create(EnemyType.GreenKoopa, new Vector2(220, 0));
			gameObjects.Add(RedKoopa);

            var coin = PowerUpFactory.Create(PowerUpType.Coin, new Vector2(300, 20));
            gameObjects.Add(coin);

            var flower = PowerUpFactory.Create(PowerUpType.Flower, new Vector2(200, 40));
            gameObjects.Add(flower);

            var greenMushroom = PowerUpFactory.Create(PowerUpType.GreenMushroom, new Vector2(100, 150));
            gameObjects.Add(flower);

            var redMushroom = PowerUpFactory.Create(PowerUpType.RedMushroom, new Vector2(600, 200));
            gameObjects.Add(redMushroom);
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
				if (!paused)
				{
				List<IGameObject> tempList = new List<IGameObject>();
				foreach (var obj in gameObjects)
				{
					if (obj.Update(gameTime))
					{
						tempList.Add(obj);
					}
				}

				gameObjects.RemoveAll((x) => tempList.Remove(x));
				base.Update(gameTime);
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			if (!paused)
			{
				GraphicsDevice.Clear(Color.CornflowerBlue);
				spriteBatch.Begin();
				foreach (var obj in gameObjects)
				{
					obj.Draw(spriteBatch, gameTime);
				}
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
    }
}
