using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.Factories;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States.EnemyStates.Powerup
{
    public class BowserDead : BowserPowerupState
    {
        public BowserDead(BowserObject context) : base(context)
        {
            Context.IsDead = true;
            Context.PointValue = 0;
            Context.BoundingBox = null;
            Context.Gravity = false;
            Context.Sprite = DeadEnemySpriteFactory.Create(EnemyType.Bowser);   
        }

        public override void BecomeDead()
        {
        }

		public override void BecomeAlive()
		{
		}

		public override void BecomeInvincible()
        {
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            if (!Context.Sprite.Finished)
            {
                EventManager.Instance.TriggerPlayerKilledBowserEvent(Context, Killer);
                int x = Context.Sprite.SourceRectangle.Width / 2;
                int y = Context.Sprite.SourceRectangle.Height / 2;
                Context.BoundingBox = new HitBox(-x, -x, -y, -y, Color.Red);
                return true;
            }
            return false;
        }
    }
}
