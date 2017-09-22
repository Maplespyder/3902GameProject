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

namespace MarioClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MarioCloneGame : Game
	{
		static GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        KeyboardController keyboardController;
        GamepadController gamepadController;
        
        static ContentManager _content;
        List<ISprite> spriteList;
        List<IGameObject> gameObjects;

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

            spriteList = new List<ISprite>();
            gameObjects = new List<IGameObject>();
            keyboardController.AddInputCommand((int)Keys.Q, new ExitCommand(this));
            gamepadController.AddInputCommand((int)Buttons.Start, new ExitCommand(this));

            var gameBounds = new List<Rectangle>()
            {
                new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)
            };


			//ICommand BrickBlockCommand = new BrickBumpCommand(new BlockObject);
			//ICommand QuestionBlockCommand = new QuestionBumpCommand(new BlockObject);

			var mario = MarioFactory.Create(new Vector2(200, 400));
            gameObjects.Add(mario);

            // TODO: use this.Content to load your game content here
            var brickblock = NormalThemedBlockSpriteFactory.Instance.Create(BlockType.BrickBlock);
			//keyboardController.AddInputCommand((int)Keys.B, BrickBlockCommand);
            spriteList.Add(brickblock);
            
            var floorblock = NormalThemedBlockSpriteFactory.Instance.Create(BlockType.FloorBlock);
            spriteList.Add(floorblock);

            var questionblock = NormalThemedBlockSpriteFactory.Instance.Create(BlockType.QuestionBlock);
			//keyboardController.AddInputCommand((int)Keys.Q, QuestionBlockCommand);
            spriteList.Add(questionblock);
            
            var stairblock = NormalThemedBlockSpriteFactory.Instance.Create(BlockType.StairBlock);
            spriteList.Add(stairblock);

            var usedblock = NormalThemedBlockSpriteFactory.Instance.Create(BlockType.UsedBlock);
            spriteList.Add(usedblock);

            var goomba = MovingEnemySpriteFactory.Instance.Create(EnemyType.Goomba);
            spriteList.Add(goomba);

            var greenkoopa = MovingEnemySpriteFactory.Instance.Create(EnemyType.GreenKoopa);
            spriteList.Add(greenkoopa);

            var redkoopa = MovingEnemySpriteFactory.Instance.Create(EnemyType.RedKoopa);
            spriteList.Add(redkoopa);
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
            
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			int i = 0;

			spriteBatch.Begin();
            foreach (var sprite in spriteList)
            {
                sprite.Draw(spriteBatch, new Vector2(i,i), 0, gameTime);
				i += 50;
            }
            foreach(var obj in gameObjects)
            {
                obj.Draw(spriteBatch, 0, gameTime);
            }
            spriteBatch.End();

			base.Draw(gameTime);
		}

        public static ContentManager GameContent
        {
            get { return _content; }
        }

		public static GraphicsDeviceManager GraphicsDevice
		{
			get { return graphics; }
		}

		public void ExitCommand()
        {
            Exit();
        }
    }
}
