using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System.Security.Cryptography;
using MarioClone.Factories;

namespace MarioClone.States.EnemyStates.Powerup
{
    public class BowserWalk : BowserActionState
    {
        public BowserWalk(BowserObject context) : base(context)
        {
            Action = BowserAction.Walk;
        }

        public override void BecomeIdle()
        {
            Context.Velocity = new Vector2(0, 0);
			Context.ActionStateBowser = new BowserIdle(Context);
			Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.BowserIdle);
		}

        public override void BecomeWalk(Facing orientation)
        {
        }

        public override void BreatheFire()
        {
			Context.ActionStateBowser = new BowserFireBreathing(Context);
			Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.BowserFire);
			Context.bigFireballPool.GetAndRelease(Context);
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

				if (MarioCloneGame.Player1.Position.X > Context.Position.X)
				{
					Context.Orientation = Facing.Right;
				}
				else
				{
					Context.Orientation = Facing.Left;
				}

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