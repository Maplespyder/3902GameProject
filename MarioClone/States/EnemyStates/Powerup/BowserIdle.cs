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
    public class BowserIdle : BowserActionState
    {
        public BowserIdle(BowserObject context) : base(context)
        {
            Action = BowserAction.Idle;
        }

        public object SpriteFactory { get; private set; }

        public override void BreatheFire()
        {
			Context.ActionStateBowser = new BowserFireBreathing(Context);
            Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.BowserFire);
            Context.bigFireballPool.GetAndRelease(Context);
        }

        public override void BecomeIdle()
        {
        }

        public override void BecomeWalk(Facing orientation)
        {
            Context.Velocity = orientation == Facing.Left ? new Vector2(-BowserObject.EnemyHorizontalMovementSpeed, 0) : new Vector2(BowserObject.EnemyHorizontalMovementSpeed, 0);
            Context.ActionStateBowser = new BowserWalk(Context);
            Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.BowserWalk);
            Context.Orientation = orientation;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Context.TimeIdle += gameTime.ElapsedGameTime.Milliseconds;
            if (Context.TimeIdle >= BowserObject.MaxTimeIdle)
            {
                Context.TimeIdle = 0;

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(random);
                randomResult = random[0] % 2;

				if(MarioCloneGame.Player1.Position.X > Context.Position.X)
				{
					Context.Orientation = Facing.Right;
				}
				else
				{
					Context.Orientation = Facing.Left;
				}

                if (randomResult == 0)
                {
                    BecomeWalk(Context.Orientation);
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
