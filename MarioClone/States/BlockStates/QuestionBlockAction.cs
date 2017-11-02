using MarioClone.GameObjects;
using MarioClone.Factories;
using Microsoft.Xna.Framework;
using MarioClone.Collision;
using MarioClone.Sounds;

namespace MarioClone.States.BlockStates
{
    public class QuestionBlockAction : BlockState
    {
        private Vector2 initialPosition;

        public QuestionBlockAction(AbstractBlock context) : base(context)
        {
            initialPosition = context.Position;
			Context.Velocity = new Vector2(0f, -1f);
			SoundPool.Instance.GetAndPlay(SoundType.Bump);
			if (Context.ContainedPowerup != PowerUpType.None)
            {
				//do some powerup reveal related thing
				SoundPool.Instance.GetAndPlay(SoundType.RevealPowerUp);
				GameGrid.Instance.Add(PowerUpFactory.Create(Context.ContainedPowerup, Context.Position));
                Context.ContainedPowerup = PowerUpType.None;
            }
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
                Context.Position = new Vector2(Context.Position.X, Context.Position.Y + Context.Velocity.Y * percent);
                Context.Velocity = new Vector2(0f, 1f);
            }

            if (Context.Position.Y >= initialPosition.Y && percent != 0) //back to static position
            {
				Context.Position = initialPosition;
                Context.Velocity = new Vector2(0, 0);
                Context.State = new Used(Context);
            }
            return false;
        }
    }
}
