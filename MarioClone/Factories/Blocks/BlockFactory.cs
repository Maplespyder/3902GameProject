using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.Factories
{
    public enum BlockType
    {
        StairBlock,
        UsedBlock,
        QuestionBlock,
        BreakableBrick,
        FloorBlock,
        BrickPiece,
        HiddenBlock,
		PipeTop,
		PipeSegment,
        Flagpole,
        Flag
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
                    return new BreakableBrickObject(SpriteFactory.Create(type), position);
                case BlockType.BrickPiece:
                    return new BrickPieceObject(SpriteFactory.Create(type),position);
                case BlockType.FloorBlock:
                    return new StaticBlockObject(SpriteFactory.Create(type), position);
                case BlockType.QuestionBlock:
                    return new QuestionBlockObject(SpriteFactory.Create(type), position);
                case BlockType.StairBlock:
                    return new StaticBlockObject(SpriteFactory.Create(type), position);
                case BlockType.UsedBlock:
                    return new StaticBlockObject(SpriteFactory.Create(type),  position);
                case BlockType.HiddenBlock:
                    return new HiddenBrickObject(SpriteFactory.Create(type), position);
				case BlockType.PipeTop:
					return new PipeTop(SpriteFactory.Create(type), position);
				case BlockType.PipeSegment:
					return new PipeSegment(SpriteFactory.Create(type), position);
                case BlockType.Flagpole:
                    return new Flagpole(SpriteFactory.Create(type), position);
                
                default:
                    return new BreakableBrickObject(SpriteFactory.Create(type),  position);
            }
        }
    }
}
