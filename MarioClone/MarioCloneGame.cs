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
            var brickblock = BlockFactory.Instance.Create(BlockType.BreakableBrick, new Vector2(0, 0));
			//keyboardController.AddInputCommand((int)Keys.B, BrickBlockCommand);
            gameObjects.Add(brickblock);

            var questionblock = BlockFactory.Instance.Create(BlockType.QuestionBlock, new Vector2(40, 0));
            //keyboardController.AddInputCommand((int)Keys.Q, QuestionBlockCommand);
            gameObjects.Add(questionblock);

            var brickpiece = BlockFactory.Instance.Create(BlockType.BrickPiece, new Vector2(60, 0));

            /* all the types below currently lack an implementation, so their creation is commented out
            var floorblock = BlockFactory.Instance.Create(BlockType.FloorBlock, new Vector2(20, 0));
            gameObjects.Add(floorblock);

            var stairblock = BlockFactory.Instance.Create(BlockType.StairBlock, new Vector2(60, 0));
            gameObjects.Add(stairblock);

            var usedblock = BlockFactory.Instance.Create(BlockType.UsedBlock, new Vector2(80, 0)); ;
            gameObjects.Add(usedblock);

            var hiddenBlock = BlockFactory.Instance.Create(BlockType.HiddenBlock, new Vector2(100, 0));
            gameObjects.Add(hiddenBlock);

            var goomba = EnemyFactory.Instance.Create(EnemyType.Goomba);
            gameObjects.Add(goomba);

            var greenkoopa = MovingEnemySpriteFactory.Instance.Create(EnemyType.GreenKoopa);
            gameObjects.Add(greenkoopa);

            var redkoopa = MovingEnemySpriteFactory.Instance.Create(EnemyType.RedKoopa);
            gameObjects.Add(redkoopa);*/
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

            List<IGameObject> tempList = new List<IGameObject>();
            foreach(var obj in gameObjects)
            {
                if(obj.Update(gameTime))
                {
                    tempList.Add(obj);
                }
            }

            gameObjects.RemoveAll((x) => tempList.Remove(x));
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
            foreach (var obj in gameObjects)
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

		public static GraphicsDeviceManager ReturnGraphicsDevice
		{
			get { return graphics; }
		}

		public void ExitCommand()
        {
            Exit();
        }
    }
}
