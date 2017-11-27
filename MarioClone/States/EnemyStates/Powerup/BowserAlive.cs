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
    class BowserAlive : EnemyPowerupState
    {
        public BowserAlive(AbstractEnemy context) : base(context)
        {
            if (Context.Orientation == Facing.Right)
            {
                Context.Velocity = new Vector2(1f, Context.Velocity.Y);
            }
            else if (Context.Orientation == Facing.Left)
            {
                Context.Velocity = new Vector2(-1f, Context.Velocity.Y);
            }
            if (Context is GreenKoopaObject)
            {
                Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.Bowser);
            }
        }

        public override void BecomeDead()
        {

            Context.PowerupState = new KoopaShell(Context);
        }
    }
}
