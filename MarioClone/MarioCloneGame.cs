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
		GraphicsDeviceManager graphics;
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

            var mariowalking = SuperMarioSpriteFactory.Instance.Create(MarioActionState.Walking);
            spriteList.Add(mariowalking);

            // TODO: use this.Content to load your game content here
            var brickblock = NormalThemedBlockFactory.Instance.Create(BlockType.BrickBlock, new Vector2(0, 0));
			//keyboardController.AddInputCommand((int)Keys.B, BrickBlockCommand);
            spriteList.Add(brickblock);
            
            var floorblock = NormalThemedBlockFactory.Instance.Create(BlockType.FloorBlock, new Vector2(20, 20));
            spriteList.Add(floorblock);

            var questionblock = NormalThemedBlockFactory.Instance.Create(BlockType.QuestionBlock, new Vector2(40, 40));
			//keyboardController.AddInputCommand((int)Keys.Q, QuestionBlockCommand);
            spriteList.Add(questionblock);
            
            var stairblock = NormalThemedBlockFactory.Instance.Create(BlockType.StairBlock, new Vector2(60, 60));
            spriteList.Add(stairblock);

            var usedblock = NormalThemedBlockFactory.Instance.Create(BlockType.UsedBlock, new Vector2(80, 80));
            spriteList.Add(usedblock);

            var goomba = IdleEnemySpriteFactory.Instance.Create(EnemyType.Goomba, new Vector2(100, 100));
            spriteList.Add(goomba);

            var greenkoopa = IdleEnemySpriteFactory.Instance.Create(EnemyType.GreenKoopa, new Vector2(120, 120));
            spriteList.Add(greenkoopa);

            var redkoopa = IdleEnemySpriteFactory.Instance.Create(EnemyType.RedKoopa, new Vector2(140, 140));
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

        public void ExitCommand()
        {
            Exit();
        }
    }
}
