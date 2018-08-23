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
    class NormalThemedBlockSpriteFactory : BlockSpriteFactory
    {
        static NormalThemedBlockSpriteFactory _factory;

        public static NormalThemedBlockSpriteFactory Instance
        {
            get
            {
                if(_factory == null)
                {
                    _factory = new NormalThemedBlockSpriteFactory();
                }
                return _factory;
            }
        }

        public override Sprite Create(BlockType type)
        {
            switch(type)
            {
                case BlockType.QuestionBlock:
					return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/CustomBlocks"),
						new Rectangle(0, 64, 64, 64), 2, 6, 6, 11, 6);
                case BlockType.BreakableBrick:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/CustomBlocks"),
                        new Rectangle(64, 0, 64, 64));
                case BlockType.FloorBlock:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/CustomBlocks"),
                        new Rectangle(0, 0, 64, 64));
                case BlockType.StairBlock:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/CustomBlocks"),
                        new Rectangle(128, 0, 64, 64));
                case BlockType.UsedBlock:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/CustomBlocks"),
						new Rectangle(192, 0, 64, 64));
                case BlockType.BrickPiece:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/BrickPiece"),
                        new Rectangle(0, 0, 16, 16));
                case BlockType.HiddenBlock:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/CustomBlocks"),
                        new Rectangle(0, 256, 64, 64));
				case BlockType.PipeTop:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Pipe"), 
						new Rectangle(0, 0, 128, 64));
				case BlockType.PipeSegment:
					return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/Pipe"),
						new Rectangle(0, 72, 128, 64));
                case BlockType.Flagpole:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/Flagpole"),
                        new Rectangle(0, 0, 84, 510), 1, 3, 0, 2, 4);
                case BlockType.FireCannon:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("CustomSprites/CustomBlocks"),
                        new Rectangle(256, 0, 64, 64));
                default:
                    return null;
            }
        }
    }
}
