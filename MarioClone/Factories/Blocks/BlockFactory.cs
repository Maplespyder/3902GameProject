﻿using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.Factories
{
    public enum BlockType
    {
        StairBlock,
        UsedBlock,
        QuestionBlock,
        CoinBlock,
        BreakableBrick,
        FloorBlock,
        BrickPiece,
        HiddenBlock
    }

    public class BlockFactory
    {
        static BlockFactory _factory;

        public static BlockSpriteFactory SpriteFactory { get; set; }

        public static BlockFactory Instance
        {
            get
            {
                if(_factory == null)
                {
                    _factory = new BlockFactory();
                }
                return _factory;
            }
        }

        protected BlockFactory()
        {
            SpriteFactory = NormalThemedBlockSpriteFactory.Instance;
        }

        public AbstractBlock Create(BlockType type, Vector2 position)
        {
            Vector2 velocity = new Vector2(0, 0);
            switch(type)
            {
                case BlockType.BreakableBrick:
                    return new BreakableBrickObject(SpriteFactory.Create(type), position, 1);
                case BlockType.CoinBlock:
                    return new CoinBrickObject(SpriteFactory.Create(type), position, 1);
                case BlockType.BrickPiece:
                    return new BrickPieceObject(SpriteFactory.Create(type),position, 1);
                case BlockType.FloorBlock:
                    return new FloorBlockObject(SpriteFactory.Create(type), position, 1);
                case BlockType.QuestionBlock:
                    return new QuestionBlockObject(SpriteFactory.Create(type), position, 1);
                case BlockType.StairBlock:
                    return new StairBlockObject(SpriteFactory.Create(type), position, 1);
                case BlockType.UsedBlock:
                    return new UsedBlockObject(SpriteFactory.Create(type),  position, 1);
                case BlockType.HiddenBlock:
                    return new HiddenBrickObject(SpriteFactory.Create(type), position, 1);
                default:
                    return new BreakableBrickObject(SpriteFactory.Create(type),  position, 1);
            }
        }
    }
}
