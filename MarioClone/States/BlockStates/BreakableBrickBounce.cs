using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States.BlockStates
{
    public class BreakableBrickBounce : BlockState
    {
        private Vector2 initialPosition;
        private bool maxHeightReached;

        public BreakableBrickBounce(AbstractBlock context) : base(context)
        {
            initialPosition = Context.Position;
            State = BlockStates.Action;
        }

        public override void Bump()
        {
            Context.Position = new Vector2(initialPosition.X, initialPosition.Y);
            Context.State = new BreakableBrickStatic(Context);
            Context.State.Bump();
        }

        public override bool Action()
        {
            if (Context.Position.Y > (initialPosition.Y - 10) && !maxHeightReached) //if Position hasnt reached max height
            {
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y - 1f);
                if (Context.Position.Y == (initialPosition.Y - 10))
                {
                    maxHeightReached = true;
                }
            }
            else //lower back down to normal height otherwise
            {
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y + 1f);
            }

            if (Context.Position.Y == initialPosition.Y) //back to static position
            {
                Context.State = new BreakableBrickStatic(Context);
            }
            return false;
        }
    }
}
