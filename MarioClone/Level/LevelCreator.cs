using MarioClone.Collision;
using MarioClone.Commands;
using MarioClone.Controllers;
using MarioClone.Factories;
using MarioClone.GameObjects;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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

		private Bitmap _image;
		public GameGrid Grid { get; set; }

		public LevelCreator(string file, GameGrid grid)
		{
			using (var stream = new FileStream(file, FileMode.Open))
			{
				_image = new Bitmap(stream);
			}
			Grid = grid;
		}

		public void Create()
		{
			for (int i = 0; i < _image.Width; i++)
			{
				for (int j = 0; j < _image.Height; j++)
				{
					MakeObject(_image.GetPixel(i, j), i * BlockWidth, j * BlockHeight);
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
                    Mario.Instance.Lives = 3;
                    Mario.Instance.CoinCount = 0;

                    var mario = Mario.Instance;
                    MarioCloneGame.HUDs.Add(new HeadsUpDisplay.HUD(mario));
                    Grid.Add(mario);
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
				else if (sameColor(pixel, Colors.PipeTop))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.PipeTop, position);
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
				else if (sameColor(pixel, Colors.QuestionBlockRedMushroom))
				{
					var initializer = BlockFactory.Instance.Create(BlockType.QuestionBlock, position);
					initializer.ContainedPowerup = PowerUpType.RedMushroom;
					initializer.Position = new Vector2(initializer.Position.X, initializer.Position.Y + initializer.Sprite.SourceRectangle.Height);
					Grid.Add(initializer);
				}
			}

		}
	}
}
