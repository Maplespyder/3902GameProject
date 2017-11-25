using MarioClone.GameObjects;
using MarioClone.Factories;

namespace MarioClone.States.BlockStates
{
    public class Used : BlockState
    {
        public Used(AbstractBlock context) : base(context)
        {
            var oldFactory = BlockFactory.SpriteFactory;
            if (Context.LevelArea == 0)
            {
                BlockFactory.SpriteFactory = NormalThemedBlockSpriteFactory.Instance;
            }
            else
            {
                BlockFactory.SpriteFactory = SubThemedBlockSpriteFactory.Instance;
            }
            context.Sprite = BlockFactory.SpriteFactory.Create(BlockType.UsedBlock);
            context.Bumper = null;
            BlockFactory.SpriteFactory = oldFactory;
        }
    }
}
