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
	public class PiranhaAlive : EnemyPowerupState
	{
		public PiranhaAlive(AbstractEnemy context) : base(context) { }

		public override void BecomeAlive() { }

		public override void BecomeDead()
		{
			Context.PowerupState = new PiranhaDead(Context);
			Context.Sprite = DeadEnemySpriteFactory.Create(EnemyType.Piranha);
			Context.BoundingBox = null;
		}

		public override bool Update(GameTime gameTime, float percent)
		{
			return false;
		}
	}
}
