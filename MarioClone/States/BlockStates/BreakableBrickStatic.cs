using MarioClone.GameObjects;
using static MarioClone.States.MarioPowerupState;

namespace MarioClone.States.BlockStates
{
    public class BreakableBrickStatic : BlockState
    {
 
        public BreakableBrickStatic(AbstractBlock context) : base(context)
        {
            Context.Bumper = null;
        }

        public override void Bump(Mario bumper)
        {
            if (bumper.PowerupState is MarioNormal2)
            {
                Context.State = new BreakableBrickBounce(Context, bumper);
            }
            else if (bumper.PowerupState is MarioSuper2 || bumper.PowerupState is MarioFire2)
            {
                Context.State = new BreakableBrickBreak((BreakableBrickObject)Context);
            }
            else if (bumper.PreviousPowerupState is MarioNormal2)
            {
                Context.State = new BreakableBrickBounce(Context, bumper);
            }
            else if (bumper.PreviousPowerupState is MarioSuper2 || bumper.PreviousPowerupState is MarioFire2)
            {
                Context.State = new BreakableBrickBreak((BreakableBrickObject)Context);
            }
        }
    }
}
