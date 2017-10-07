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
			Context.Velocity = new Vector2(0f, -1f);
        }

        public override void Bump()
        {
            // do nothing
        }

        public override bool Action()
        {
            if (Context.Position.Y > (initialPosition.Y - 10) ) //if Position hasnt reached max height
            {
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y + Context.Velocity.Y);
                if (Context.Position.Y >= (initialPosition.Y - 10))
                {
					Context.Velocity = new Vector2(0f, 1f);
                }
            }
            if (Context.Position.Y <= initialPosition.Y) //back to static position
            {
				Context.Position = initialPosition;
                Context.State = new Used(Context);
            }
            return false;
        }
    }
}
