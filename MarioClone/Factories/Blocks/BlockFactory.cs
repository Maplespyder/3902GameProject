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

        public IGameObject Create(BlockType type, Vector2 position)
        {
            Vector2 velocity = new Vector2(0, 0);
            switch(type)
            {
                case BlockType.BreakableBrick:
                    return new BreakableBrickObject(SpriteFactory.Create(type), velocity, position);
                case BlockType.CoinBlock:
                    //return new CoinBrickObject();
                case BlockType.BrickPiece:
                    return new GameObjects.Bricks.BrickPieceObject(SpriteFactory.Create(type), velocity, position);
                case BlockType.FloorBlock:
                    //return null;
                case BlockType.QuestionBlock:
                    return new QuestionBlockObject(SpriteFactory.Create(type), velocity, position);
                case BlockType.StairBlock:
                    //return new StairBlock();
                case BlockType.UsedBlock:
                    //return new UsedBlockObject();
                case BlockType.HiddenBlock:
                    //return new HiddenBrickObject(SpriteFactory.Create(type), velocity, position);
                default:
                    return new BreakableBrickObject(SpriteFactory.Create(type), velocity, position);
            }
        }
    }
}
