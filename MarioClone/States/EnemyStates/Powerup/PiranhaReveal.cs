using MarioClone.Factories;
using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarioClone.States.EnemyStates.Powerup
{
	public class PiranhaReveal : EnemyPowerupState
	{

		Vector2 initialPosition;
		public PiranhaReveal(AbstractEnemy context) : base(context)
		{
			initialPosition = Context.Position;
			Context.PiranhaCycleTime = 0;
		}
        
		public override void BecomeDead()
		{
			Context.PowerupState = new PiranhaDead(Context);
			Context.Sprite = DeadEnemySpriteFactory.Create(EnemyType.Piranha);
			Context.BoundingBox = null;
		}

		public override void BecomeHide()
		{
			Context.PowerupState = new PiranhaHide(Context);
		}

		public override bool Update(GameTime gameTime, float percent)
		{

			Context.PiranhaCycleTime += gameTime.ElapsedGameTime.Milliseconds;
			if (Context.PiranhaCycleTime >= AbstractEnemy.MaxPiranhaReveal)
			{
				Context.Velocity = new Vector2(0, 1f);
				Context.Position = new Vector2(Context.Position.X, Context.Position.Y + Context.Velocity.Y * percent);
				if (Context.Position.Y >= initialPosition.Y && percent != 0) //back to static position
				{
					Context.Position = initialPosition;
					Context.Velocity = new Vector2(0, 0);
					BecomeHide();
				}
			}
			else
			{
				if (Context.Position.Y >= (initialPosition.Y - Context.Sprite.SourceRectangle.Height)) //if Position hasnt reached max height
				{
					Context.Position = new Vector2(Context.Position.X, Context.Position.Y + Context.Velocity.Y * percent);
					if (Context.Position.Y <= (initialPosition.Y - Context.Sprite.SourceRectangle.Height))
					{
						Context.Velocity = new Vector2(0f, 0f);
					}
				}
				else
				{
					Context.Position = new Vector2(Context.Position.X, Context.Position.Y + Context.Velocity.Y * percent);
					Context.Velocity = new Vector2(0f, 0f);
				}
			}
				return false;
		}
	}
}
