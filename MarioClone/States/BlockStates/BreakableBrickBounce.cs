using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States.BlockStates
{
    public class BreakableBrickBounce : BlockState
    {
        private Vector2 initialPosition;

        public BreakableBrickBounce(AbstractBlock context) : base(context)
        {
            initialPosition = Context.Position;
			Context.Velocity = new Vector2(0f, -1f);
        }
        
        public override bool Action(float percent, GameTime gameTime)
        {
            if (Context.Position.Y >= (initialPosition.Y - 10)) //if Position hasnt reached max height
            {
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y + Context.Velocity.Y * percent);
                if (Context.Position.Y <= (initialPosition.Y - 10))
                {
					Context.Velocity = new Vector2(0f, 1f);
				}
            }
            else
            {
                Context.Velocity = new Vector2(0f, 1f);
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y + Context.Velocity.Y * percent);
            }

            if (Context.Position.Y >= initialPosition.Y) //back to static position
            {
				Context.Position = initialPosition;
                Context.Velocity = new Vector2(0, 0);
				Context.State = new BreakableBrickStatic(Context);
            }
            return false;
        }
    }
}
