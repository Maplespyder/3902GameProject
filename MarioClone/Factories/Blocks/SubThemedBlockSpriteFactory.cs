using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Factories
{
	class SubThemedBlockSpriteFactory : BlockSpriteFactory
	{
		static SubThemedBlockSpriteFactory _factory;

		public static SubThemedBlockSpriteFactory Instance
		{
			get
			{
				if (_factory == null)
				{
					_factory = new SubThemedBlockSpriteFactory();
				}
				return _factory;
			}
		}

		public override Sprite Create(BlockType type)
		{
			switch (type)
			{
				case BlockType.QuestionBlock:
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SubAllBlocks"),
						new Rectangle(0, 0, 64, 64), 2, 4, 0, 3, 4);
				case BlockType.BreakableBrick:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SubAllBlocks"),
						new Rectangle(0, 64, 64, 64));
				case BlockType.FloorBlock:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SubAllBlocks"),
						new Rectangle(64, 64, 64, 64));
				case BlockType.StairBlock:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SubAllBlocks"),
						new Rectangle(128, 64, 64, 64));
				case BlockType.UsedBlock:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SubAllBlocks"),
						new Rectangle(192, 64, 64, 64));
				case BlockType.BrickPiece:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/BrickPiece"),
						new Rectangle(0, 0, 32, 32));
				case BlockType.HiddenBlock:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/SubAllBlocks"),
						new Rectangle(0, 64, 64, 64));
				case BlockType.PipeTop:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Pipe"),
						new Rectangle(0, 0, 128, 64));
				case BlockType.PipeSegment:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Pipe"),
						new Rectangle(0, 72, 124, 108));
				default:
					return null;
			}
		}
	}
}
