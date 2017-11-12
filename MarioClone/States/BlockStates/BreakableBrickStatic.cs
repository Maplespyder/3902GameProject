using MarioClone.GameObjects;
using static MarioClone.States.MarioPowerupState;

namespace MarioClone.States.BlockStates
{
    public class BreakableBrickStatic : BlockState
    {
 
        public BreakableBrickStatic(AbstractBlock context) : base(context)
        {
        }

        public override void Bump()
        {
            if (Mario.Instance.PowerupState is MarioNormal)
            {
                Context.State = new BreakableBrickBounce(Context);
            }
            else
            {
                Context.State = new BreakableBrickBreak((BreakableBrickObject)Context);
            }
        }
    }
}
