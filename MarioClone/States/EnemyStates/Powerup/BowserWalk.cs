using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System.Security.Cryptography;

namespace MarioClone.States.EnemyStates.Powerup
{
    class BowserWalk : BowserActionState
    {
        protected BowserWalk(BowserObject context) : base(context)
        {
            Action = BowserAction.Walk;
        }

        public override void BecomeIdle()
        {
            Context.Velocity = new Vector2(0, 0);
            Context.ActionStateBowser = BowserIdle.Instance;
            Context.Sprite = Context.SpriteFactory.Create(BowserAction.Idle);
        }

        public override void BecomeWalk(Facing orientation)
        {
        }

        public override void BreatheFire()
        {
            Context.ActionStateBowser = BowserFireBreathing.Instance;
            Context.PowerupStateBowser = BowserIdle.Instance;
            Context.Sprite = Context.SpriteFactory.Create(BowserAction.BreatheFire);
            bigFireballPool.GetAndRelease(BowserObject);
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Context.TimeWalk += gameTime.ElapsedGameTime.Milliseconds;
            if (Context.TimeWalk >= BowserObject.MaxTimeWalk)
            {
                Context.TimeWalk = 0;

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(random);
                randomResult = random[0] % 2;

                if (randomResult == 0)
                {
                    BecomeIdle();
                }
                else if (randomResult == 1)
                {
                    BreatheFire();
                }


            }
            return false;
        }
    }
}
