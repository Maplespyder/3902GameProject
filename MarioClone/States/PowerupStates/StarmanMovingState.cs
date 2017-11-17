using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
	public class StarmanMovingState : PowerupState
	{
		public StarmanMovingState(AbstractPowerup star) : base(star)
		{
		    Context.Velocity = new Vector2(-2, 0);
		}
	}
}
