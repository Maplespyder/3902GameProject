using MarioClone.Factories;
using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MarioClone.Collision;

namespace MarioClone.States.EnemyStates
{
    class KoopaDead : EnemyPowerupState
    {
        public KoopaDead(AbstractEnemy context) : base(context) { }

        public override void BecomeDead() { }

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
                return true;
            }
            return false;
        }

    }
}
