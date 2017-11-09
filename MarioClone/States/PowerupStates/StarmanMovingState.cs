using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
	public class StarmanMovingState : PowerupState
	{
		public StarmanMovingState(AbstractPowerup star) : base(star)
		{
			if (Mario.Instance.Position.X <= Context.Position.X)
			{
				Context.Velocity = new Vector2(-2, 0);
			}
			else
			{
				Context.Velocity = new Vector2(2, 0);
			}
		}
	}
}
