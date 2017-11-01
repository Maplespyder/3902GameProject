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
    class KoopaAlive :EnemyPowerupState
    {
        public KoopaAlive(AbstractEnemy context) : base(context) { }

        public override void BecomeAlive() { }

        public override void BecomeDead()
        {
            Context.PowerupState = new KoopaShell(Context);
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

        public override bool Update(GameTime gameTime, float percent)
        {
            return false;
        }
    }
}
