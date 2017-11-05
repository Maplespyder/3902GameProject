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
        public KoopaShell(AbstractEnemy context) : base(context)
        {
            Context.PointValue -= 100;
            Context.PointValue = Context.PointValue >= 0 ? Context.PointValue : 0;

            Context.TimeDead = 0;
            Context.Velocity = new Vector2(0, 0);
            if (Context is GreenKoopaObject)
            {
                Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.GreenKoopaShell);
            }
            else if (Context is RedKoopaObject)
            {
                Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.RedKoopaShell);
            }
        }

        public override void BecomeDead()
        {
            SoundPool.Instance.GetAndPlay(SoundType.Kick);
            Context.PowerupState = new KoopaDead(Context);
        }

        public override void BecomeAlive()
        {
            Context.PowerupState = new KoopaAlive(Context);
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Context.TimeDead += gameTime.ElapsedGameTime.Milliseconds;
            if (Context.TimeDead >= AbstractEnemy.MaxTimeShell)
            {
                BecomeAlive();
            }
            return false;
        }
	}
}
