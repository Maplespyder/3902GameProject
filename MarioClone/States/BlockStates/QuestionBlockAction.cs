using MarioClone.GameObjects;
using MarioClone.Factories;
using Microsoft.Xna.Framework;

namespace MarioClone.States.BlockStates
{
    public class QuestionBlockAction : BlockState
    {
        private Vector2 initialPosition;

        public QuestionBlockAction(AbstractBlock context) : base(context)
        {
            initialPosition = context.Position;
            State = BlockStates.Action;
			Context.Velocity = new Vector2(0f, -1f);
        }

        public override void Bump()
        {
            // do nothing
        }

        public override bool Action(float percent)
        {
            if (Context.Position.Y >= (initialPosition.Y - 10) ) //if Position hasnt reached max height
            {
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y + Context.Velocity.Y * percent);
                if (Context.Position.Y <= (initialPosition.Y - 10))
                {
					Context.Velocity = new Vector2(0f, 1f);
                }
            }
            else
            {
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y + Context.Velocity.Y * percent);
                Context.Velocity = new Vector2(0f, 1f);
            }
            if (Context.Position.Y >= initialPosition.Y) //back to static position
            {
				Context.Position = initialPosition;
                Context.Velocity = new Vector2(0, 0);
                Context.State = new Used(Context);
            }
            return false;
        }
    }
}
