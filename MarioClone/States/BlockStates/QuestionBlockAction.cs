using MarioClone.GameObjects;
using MarioClone.Factories;
using Microsoft.Xna.Framework;

namespace MarioClone.States.BlockStates
{
    public class QuestionBlockAction : BlockState
    {
        private Vector2 initialPosition;
        private bool maxHeightReached;

        public QuestionBlockAction(AbstractBlock context) : base(context)
        {
            initialPosition = context.Position;
            maxHeightReached = false;
            State = BlockStates.Action;
        }

        public override void Bump()
        {
            // do nothing
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
                Context.State = new Used(Context);
            }
            return false;
        }
    }
}
