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
    class NormalThemedBlockFactory : BlockSpriteFactory
    {
        static NormalThemedBlockFactory _factory;

        public static NormalThemedBlockFactory Instance
        {
            get
            {
                if(_factory == null)
                {
                    _factory = new NormalThemedBlockFactory();
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
                    return new MotionlessSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/AllBlocks"),
                        location, new Rectangle(0, 0, 16, 16), new Vector2(0, 0), rectangles, true);
                case BlockType.BrickBlock:
                    return new MotionlessSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/AllBlocks"),
                        location, new Rectangle(0, 16, 16, 16), new Vector2(0,0), rectangles, true);
                case BlockType.FloorBlock:
                    return new MotionlessSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/AllBlocks"),
                        location, new Rectangle(16, 16, 16, 16), new Vector2(0, 0), rectangles, true);
                case BlockType.StairBlock:
                    return new MotionlessSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/AllBlocks"),
                        location, new Rectangle(32, 16, 16, 16), new Vector2(0, 0), rectangles, true);
                case BlockType.UsedBlock:
                    return new MotionlessSprite(MarioCloneGame.GameContent.Load<Texture2D>("Sprites/AllBlocks"),
                        location, new Rectangle(48, 16, 16, 16), new Vector2(0, 0), rectangles, true);
                default:
                    return null;
            }
        }
    }
}
