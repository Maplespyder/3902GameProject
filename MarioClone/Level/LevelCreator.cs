using MarioClone.Collision;
using MarioClone.Factories;
using MarioClone.GameObjects;
using MarioClone.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MarioClone.Level
{
    public class LevelCreator
	{
		private const int KoopaHeight = 112;
		private const int GoombaHeight = 62;
		private const int MarioHeight = 128;
		private const int PipeTopHeight = 64;
		private const int PipeTopWidth = 128;


		private const int BlockWidth = 64;
		private const int BlockHeight = 64;

        bool aboveGround;
        private string file;
        private int xOffsetFromUnderground;
        private int yOffsetFromUnderground;
		private Bitmap _image;
        private Bitmap _subLevelImageSwap;
        private PipeTop danglingWarp;

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> LevelAreas { get; private set; }

		public GameGrid Grid { get; set; }

		public LevelCreator(string file, GameGrid grid)
		{
            this.file = file;
			using (var stream = new FileStream(this.file, FileMode.Open))
			{
				_image = new Bitmap(stream);
			}
			Grid = grid;
		}

		public void Create()
        {
            aboveGround = true;
            LevelAreas = new Dictionary<int, Microsoft.Xna.Framework.Rectangle>();

            LevelAreas.Add(0, new Microsoft.Xna.Framework.Rectangle(0, 0, _image.Width * BlockWidth, _image.Height * BlockHeight));
            CreationLoop(0, 0);
		}

        //rename to xOffset, yOffset
        private void CreationLoop(int xOffset, int yOffset)
        {
            for (int i = 0; i < _image.Width; i++)
            {
                for (int j = 0; j < _image.Height; j++)
                {
                    MakeObject(_image.GetPixel(i, j), (i + xOffset) * BlockWidth, (j + yOffset) * BlockHeight);
                }
            }
        }

		private void MakeObject(System.Drawing.Color pixel, int x, int y)
		{
			Func<System.Drawing.Color, System.Drawing.Color, bool> sameColor = (c1, c2) => (c1.R == c2.R && c1.G == c2.G && c1.B == c2.B);
			var position = new Vector2(x, y);

			if (!sameColor(pixel, Colors.Empty))
			{
				if (sameColor(pixel, Colors.MarioSpawn) && Mario.Instance == null)
				{
                    position = new Vector2(position.X, position.Y - (MarioHeight - 64));
					var mario = MarioFactory.Create(position);
                    MarioCloneGame.HUDs.Add(new HeadsUpDisplay.HUD(mario));

                    Grid.Add(mario);
				}
				else if (sameColor(pixel, Colors.MarioSpawn) && Mario.Instance != null)
				{
                    Mario.Instance.Position = new Vector2(position.X, position.Y - (MarioHeight - 64));
					Mario.Instance.ActionState = MarioIdle.Instance;
					Mario.Instance.Velocity = new Vector2(0, 0);
					Mario.Instance.PowerupState = MarioNormal.Instance;
					Mario.Instance.SpriteFactory = NormalMarioSpriteFactory.Instance;
					Mario.Instance.PreviousActionState = MarioIdle.Instance;
					Mario.Instance.Sprite = NormalMarioSpriteFactory.Instance.Create(MarioAction.Idle);
					Mario.Instance.Orientation = Facing.Right;
                    Mario.Instance.Lives--;
                    Mario.Instance.CoinCount = 0;

                    Mario.Instance.AdjustForCheckpoint();

                    var mario = Mario.Instance;
                    MarioCloneGame.HUDs.Add(new HeadsUpDisplay.HUD(mario));
                    Grid.Add(mario);
				}
                else if (sameColor(pixel, Colors.MarioCheckpoint))
                {
                    Mario.Instance.Spawns.Add(position);
                }
				else if (sameColor(pixel, Colors.QuestionBlock))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.QuestionBlock, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.QuestionBlockGreenMushroom))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.QuestionBlock, position);
					initializer.ContainedPowerup = PowerUpType.GreenMushroom;
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.QuestionBlockFireFlower))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.QuestionBlock, position);
					initializer.ContainedPowerup = PowerUpType.Flower;
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.BrickBlock))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.BreakableBrick, position);
					initializer.CoinCount = 3;
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.UsedBlock))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.UsedBlock, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.FloorBlock))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.FloorBlock, position);
					initializer.Position = new Vector2(initializer.Position.X, (initializer.Position.Y + initializer.Sprite.SourceRectangle.Height));
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.StairBlock))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.StairBlock, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.HiddenBlock))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.HiddenBlock, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.Goomba))
				{
					position = new Vector2(position.X, position.Y - (GoombaHeight - 64));
					var initializer = EnemyFactory.Create(EnemyType.Goomba, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.Piranha))
				{
					var initializer = EnemyFactory.Create(EnemyType.Piranha, position);
					initializer.Position = new Vector2(initializer.Position.X + (PipeTopWidth/4), initializer.Position.Y +
						(initializer.Sprite.SourceRectangle.Height) + PipeTopHeight);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.GreenKoopa))
				{
					position = new Vector2(position.X, position.Y);
					var initializer = EnemyFactory.Create(EnemyType.GreenKoopa, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.RedKoopa))
				{
					position = new Vector2(position.X, position.Y);
					var initializer = EnemyFactory.Create(EnemyType.RedKoopa, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.RedMushroom))
				{
					var initializer = PowerUpFactory.Create(PowerUpType.RedMushroom, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.GreenMushroom))
				{
					var initializer = PowerUpFactory.Create(PowerUpType.GreenMushroom, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.FireFlower))
				{
					var initializer = PowerUpFactory.Create(PowerUpType.Flower, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.Starman))
				{
					var initializer = PowerUpFactory.Create(PowerUpType.Star, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.Coin))
				{
					var initializer = PowerUpFactory.Create(PowerUpType.Coin, position);
					initializer.State = new CoinStaticState(initializer);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.PipeSegment))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.PipeSegment, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
                else if (sameColor(pixel, Colors.Flagpole))
                {
                    var initializer = BlockFactory.Instance.Create(BlockType.Flagpole, position);
                    initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
                    Grid.Add(initializer);
                }
                else if (sameColor(pixel, Colors.Flag))
                {
                    var initializer = BlockFactory.Instance.Create(BlockType.Flag, position);
                    initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
                    Grid.Add(initializer);
                }
                else if (sameColor(pixel, Colors.PipeTop))
				{
                    PipeTop initializer = (PipeTop)BlockFactory.Instance.Create(BlockType.PipeTop, position);
                    initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
                    if(aboveGround)
                    {
                        initializer.LevelArea = 0;
                    }
                    else
                    {
                        String temp = Path.GetFileNameWithoutExtension(file);
                        initializer.LevelArea = temp[temp.Length - 1] - '0';
                    }
                    
                    for (int i = (y / BlockHeight) - yOffsetFromUnderground - 1; i < _image.Height; i++)
                    {
                        System.Drawing.Color tempPixel = _image.GetPixel((x / BlockWidth) - xOffsetFromUnderground, i);
                        //"false" will be replaced with pixel.R == Colors.WarpSpot.R && pixel.G == Colors.WarpSpot.G
                        if(aboveGround && tempPixel.R == Colors.WarpPoint.R && tempPixel.G == Colors.WarpPoint.G)
                        {
                            if (danglingWarp != null)
                            {
                                danglingWarp.WarpEnd = initializer;
                                initializer.WarpEnd = danglingWarp;
                                danglingWarp = null;
                            }
                            else
                            {
                                danglingWarp = initializer;
                                aboveGround = false;

                                String newFile = String.Concat(Path.GetFileNameWithoutExtension(file), tempPixel.B, Path.GetExtension(file));
                                String tempHolder = file;
                                newFile = Path.Combine(Path.GetDirectoryName(file), newFile);

                                _subLevelImageSwap = _image;
                                using (var stream = new FileStream(newFile, FileMode.Open))
                                {
                                    _image = new Bitmap(stream);
                                }

                                file = newFile;
                                LevelAreas.Add(tempPixel.B, 
                                    new Microsoft.Xna.Framework.Rectangle(x, (i + 1) * BlockHeight, _image.Width * BlockWidth, MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight));
                                xOffsetFromUnderground = x / BlockWidth;
                                yOffsetFromUnderground = i;

                                BlockFactory.SpriteFactory = SubThemedBlockSpriteFactory.Instance;
                                CreationLoop(x / BlockWidth, i + 1);
                                BlockFactory.SpriteFactory = NormalThemedBlockSpriteFactory.Instance;

                                xOffsetFromUnderground = 0;
                                yOffsetFromUnderground = 0;
                                file = tempHolder;
                                aboveGround = true;
                                using (var stream = new FileStream(file, FileMode.Open))
                                {
                                    _image = new Bitmap(stream);
                                }
                            }

                            break;
                        }
                        //"false" will be replaced with pixel.R == Colors.WarpSpot.R && pixel.G == Colors.WarpSpot.G
                        else if (tempPixel.R == Colors.WarpPoint.R && tempPixel.G == Colors.WarpPoint.G)
                        {
                            if (danglingWarp != null)
                            {
                                danglingWarp.WarpEnd = initializer;
                                initializer.WarpEnd = danglingWarp;
                                danglingWarp = null;
                            }
                            else
                            {
                                danglingWarp = initializer;
                            }
                        }
                    }

					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.QuestionBlockRedMushroom))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.QuestionBlock, position);
					initializer.ContainedPowerup = PowerUpType.RedMushroom;
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
                else if(pixel.R == Colors.WarpPoint.R && pixel.G == Colors.WarpPoint.G)
                {
                    if((x / BlockWidth) - 1 - xOffsetFromUnderground >= 0)
                    {
                        //fill in the blank space with some surrounding to blend in
                        MakeObject(_image.GetPixel((x / BlockWidth) - 1 - xOffsetFromUnderground, (y / BlockHeight) - yOffsetFromUnderground - 1), x, y);
                    }
                    else if((x / BlockWidth) + 1 < _image.Width)
                    {
                        MakeObject(_image.GetPixel((x / BlockWidth) + 1 - xOffsetFromUnderground, (y / BlockHeight) - yOffsetFromUnderground - 1), x, y);
                    }
                    else
                    {
                        MakeObject(Colors.FloorBlock, (x / BlockWidth) - xOffsetFromUnderground, (y / BlockHeight) - yOffsetFromUnderground - 1);
                    }
                }
			}

		}
	}
}
