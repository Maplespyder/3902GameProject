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

        public override Sprite Create(BlockType type, Vector2 location)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            rectangles.Add(new Rectangle(0, 0, 800, 600));
            switch(type)
            {
                case BlockType.QuestionBlock:
                    return new AnimatedSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/AllBlocks"),
                        new Rectangle(0, 0, 32, 32), 2, 4, 0, 3 );
                case BlockType.BrickBlock:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/AllBlocks"),
                        new Rectangle(0, 32, 32, 32));
                case BlockType.FloorBlock:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/AllBlocks"),
                        new Rectangle(32, 32, 32, 32));
                case BlockType.StairBlock:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/AllBlocks"),
                        new Rectangle(64, 32, 32, 32));
                case BlockType.UsedBlock:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/AllBlocks"),
						new Rectangle(96, 32, 32, 32));
                case BlockType.BrokenBlock:
                    return new StaticSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/BrickPiece"),
                        new Rectangle(0, 0, 16, 16));
                default:
                    return null;
            }
        }
    }
}
