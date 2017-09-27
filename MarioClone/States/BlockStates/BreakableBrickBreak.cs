using MarioClone.GameObjects;
using static MarioClone.States.MarioPowerupState;

namespace MarioClone.States.BlockStates
{
    public class BreakableBrickBreak : BlockState
    {

        public BreakableBrickBreak(AbstractBlock context) : base(context)
        {
            State = BlockStates.Action;
            (Context as BreakableBrickObject).Break();
        }

        public override void Bump()
        {
            // do nothing
        }

        public override bool Action()
        {
            
            return true;
        }
    }
}
