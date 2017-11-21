using MarioClone.GameObjects;
using MarioClone.Factories;

namespace MarioClone.States.BlockStates
{
    public class Used : BlockState
    {
        public Used(AbstractBlock context) : base(context)
        {
            context.Sprite = BlockFactory.SpriteFactory.Create(BlockType.UsedBlock);
            context.Bumper = null;
        }
    }
}
