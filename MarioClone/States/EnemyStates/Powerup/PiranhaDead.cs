using MarioClone.Factories;
using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MarioClone.Collision;

namespace MarioClone.States.EnemyStates.Powerup
{
	public class PiranhaDead : EnemyPowerupState
	{

		public PiranhaDead(AbstractEnemy context) : base(context)
        {
            Context.IsDead = true;
        }
        
		public override void BecomeAlive()
		{
			Context.PowerupState = new PiranhaReveal(Context);
            Context.IsDead = false;
			Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.Piranha);
            Context.BoundingBox = null;
            Context.Gravity = false;
        }

		public override bool Update(GameTime gameTime, float percent)
		{
			if (Context.Sprite.Finished)
			{
                int x = Context.Sprite.SourceRectangle.Width / 2;
                int y = Context.Sprite.SourceRectangle.Height / 2;
                Context.BoundingBox = new HitBox(-x, -x, -y, -y, Color.Red);
                return true;
			}
			return false;
		}
	}
}
