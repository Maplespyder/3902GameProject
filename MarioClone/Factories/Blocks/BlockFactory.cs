using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.Factories
{
    public enum BlockType
    {
        StairBlock,
        UsedBlock,
        QuestionBlock,
        CoinBlock,
        BrickBlock,
        FloorBlock,
        BrokenBlock
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

        public IGameObject Create(BlockType type, Vector2 position)
        {
            switch(type)
            {
                case BlockType.BrickBlock:
                    return new BreakableBrickObject(SpriteFactory.Create(type), new Vector2(0, 0), position);
                case BlockType.CoinBlock:
                    return new CoinBrickObject();
                case BlockType.BrokenBlock:
                    return null;
                case BlockType.FloorBlock:
                    return null;
                case BlockType.QuestionBlock:
                    return new QuestionBlockObject();
                case BlockType.StairBlock:
                    //return new StairBlock();
                case BlockType.UsedBlock:
                    //return new UsedBlockObject();
                default:
                    return new BreakableBrickObject(SpriteFactory.Create(type), new Vector2(0, 0), position);
            }
        }
    }
}
