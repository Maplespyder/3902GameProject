using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System.Security.Cryptography;
using MarioClone.Factories;
using MarioClone.EventCenter;

namespace MarioClone.States.EnemyStates.Powerup
{
    public class BowserFireBreathing : BowserActionState
    {
        public BowserFireBreathing(BowserObject context) : base(context)
        {
            Action = BowserAction.BreatheFire;
			Context.Velocity = Vector2.Zero;
        }

        public override void BreatheFire()
        {
        }

        public override void BecomeIdle()
        {
            Context.Velocity = new Vector2(0, 0);
			Context.ActionStateBowser = new BowserIdle(Context);
            Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.BowserIdle);
        }

        public override void BecomeWalk(Facing orientation)
        {
            Context.Velocity = orientation == Facing.Left ? new Vector2(-BowserObject.BowserMovementSpeed, 0) : new Vector2(BowserObject.BowserMovementSpeed, 0);
			Context.ActionStateBowser = new BowserWalk(Context);
            Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.BowserWalk);
            Context.Orientation = orientation;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Context.TimeFire += gameTime.ElapsedGameTime.Milliseconds;
            if (Context.TimeFire >= BowserObject.MaxTimeFire)
            {
                Context.TimeFire = 0;

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
                    BecomeWalk(Context.Orientation);
                }
                else if (randomResult == 1)
                {
                    BecomeIdle();
                }

            }
            UpdateHitBox();
            return false;

        }
        public void UpdateHitBox()
        {
            if (Context.Orientation is Facing.Left) Context.BoundingBox.UpdateOffSets(-20, -340, -36, -1);
            if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-340, -20, -36, -1);
        }
    }
}
