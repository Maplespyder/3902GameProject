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
   public class BowserAlive : BowserPowerupState
    {
        public BowserAlive(BowserObject context) : base(context)
        {
            if (Context.Orientation == Facing.Right)
            {
                Context.Velocity = new Vector2(1f, Context.Velocity.Y);
            }
            else if (Context.Orientation == Facing.Left)
            {
                Context.Velocity = new Vector2(-1f, Context.Velocity.Y);
            } 
                Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.BowserIdle);            
        }

        public override void BecomeDead()
        {
            Context.PowerupStateBowser = new BowserDead(Context);

        }

		public override void BecomeAlive() { }

		public override void BecomeInvincible()
        {
        }
    
        public override bool Update(GameTime gameTime, float percent)
        {
            return false;
        }

    }
}
