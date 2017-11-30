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
			Context.Velocity = Vector2.Zero;
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
			Context.Orientation = orientation;
			Context.Velocity = orientation == Facing.Left ? new Vector2(-BowserObject.BowserMovementSpeed, 0) : new Vector2(BowserObject.BowserMovementSpeed, 0);
			Context.ActionStateBowser = new BowserWalk(Context);
			Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.BowserWalk);
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
					BreatheFire();
				}
			}
            UpdateHitBox();
			return false;
		}

        public void UpdateHitBox()
        {
            if(Context.Orientation is Facing.Left) Context.BoundingBox.UpdateOffSets(-20, -340, -36, -1);
            if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-340, -20, -36, -1);
        }
	}
}
