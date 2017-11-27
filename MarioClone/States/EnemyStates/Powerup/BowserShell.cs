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
    class BowserShell : EnemyPowerupState
    {
        public BowserShell(AbstractEnemy context) : base(context)
        {
            Context.PointValue -= 100;
            Context.PointValue = Context.PointValue >= 0 ? Context.PointValue : 0;

            Context.TimeDead = 0;
            Context.Velocity = new Vector2(0, 0);
            
            Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.Bowser);
                     
        }

        public override void BecomeDead()
        {
            Context.PowerupState = new KoopaDead(Context);
        }

        public override void BecomeAlive()
        {
            Context.PowerupState = new KoopaAlive(Context);
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Context.TimeDead += gameTime.ElapsedGameTime.Milliseconds;
            Context.Hits += 1;
            if (Context.TimeDead >= AbstractEnemy.MaxTimeShell)
            {
                if (Context.Hits == 3)
                {
                    BecomeDead();
                }
                BecomeAlive();
            }
            return false;
        }
    }
}
