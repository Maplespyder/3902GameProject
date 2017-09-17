using MarioClone.ISprite;
using Microsoft.Xna.Framework;

namespace MarioClone.Factories
{
    public enum BlockType
    {
        StairBlock,
        UsedBlock,
        QuestionBlock,
        BrickBlock,
        FloorBlock
    }

    public abstract class BlockSpriteFactory
    {
        protected BlockSpriteFactory() { }

        public abstract Sprite Create(BlockType type, Vector2 location);
    }
}
