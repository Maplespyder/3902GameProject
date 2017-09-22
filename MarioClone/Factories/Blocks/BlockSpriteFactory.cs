using MarioClone.Sprites;
using Microsoft.Xna.Framework;

namespace MarioClone.Factories
{

    public abstract class BlockSpriteFactory
    {
        protected BlockSpriteFactory() { }

        public abstract Sprite Create(BlockType type);
    }
}
