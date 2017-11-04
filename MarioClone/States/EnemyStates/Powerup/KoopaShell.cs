using MarioClone.Factories;
using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MarioClone.Collision;
using MarioClone.Sounds;

namespace MarioClone.States.EnemyStates
{
    class KoopaShell : EnemyPowerupState
    {
        public KoopaShell(AbstractEnemy context) : base(context) { }

        public override void BecomeDead() {
            Context.PowerupState = new KoopaDead(Context);
			SoundPool.Instance.GetAndPlay(SoundType.Kick);
			if (Context is GreenKoopaObject)
            {
                Context.Sprite = DeadEnemySpriteFactory.Create(EnemyType.GreenKoopa);
            }
            else if (Context is RedKoopaObject)
            {
                Context.Sprite = DeadEnemySpriteFactory.Create(EnemyType.RedKoopa);
            }
        }

        public override void BecomeAlive()
        {
            Context.PowerupState = new KoopaAlive(Context);
            if (Context is GreenKoopaObject)
            {
                Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.GreenKoopa);
            }
            else if (Context is RedKoopaObject)
            {
                Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.RedKoopa);
            }
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Context.TimeDead += gameTime.ElapsedGameTime.Milliseconds;
            if (Context.TimeDead >= AbstractEnemy.MaxTimeShell)
            {
                BecomeAlive();
                if (Context.Orientation == Facing.Right)
                {
                    Context.Velocity = new Vector2(1f, Context.Velocity.Y);


                }
                else if (Context.Orientation == Facing.Left)
                {
                    Context.Velocity = new Vector2(-1f, Context.Velocity.Y);

                }
                Context.Position = new Vector2(Context.Position.X + Context.Velocity.X, Context.Position.Y + Context.Velocity.Y);
            }
            return false;
        }

		public override void BecomeHide()
		{
		}
		public override void BecomeReveal()
		{
		}

	}
}
