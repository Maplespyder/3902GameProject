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
	public class PiranhaHide : EnemyPowerupState
	{
		public PiranhaHide(AbstractEnemy context) : base(context)
		{
			Context.PiranhaCycleTime = 0;
		}

		public override void BecomeHide()
		{
		}
		public override void BecomeReveal()
		{
			Context.PowerupState = new PiranhaReveal(Context);
			Context.Velocity = new Vector2(0, -1);
		}
		public override void BecomeAlive() { }

		public override void BecomeDead()
		{
			Context.PowerupState = new PiranhaDead(Context);
			Context.Sprite = DeadEnemySpriteFactory.Create(EnemyType.Piranha);
			Context.BoundingBox = null;
		}

		public override bool Update(GameTime gameTime, float percent)
		{
			Context.PiranhaCycleTime += gameTime.ElapsedGameTime.Milliseconds;
			if (Context.PiranhaCycleTime >= AbstractEnemy.MaxPiranhaHide)
			{
				BecomeReveal();
			}
			return false;
		}
	}
}
