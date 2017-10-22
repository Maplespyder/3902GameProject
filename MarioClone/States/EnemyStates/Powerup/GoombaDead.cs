using MarioClone.Factories;
using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MarioClone.Collision;

namespace MarioClone.States
{
    public class GoombaDead : EnemyPowerupState
    {

        public GoombaDead(AbstractEnemy context) : base(context) { }

        public override void BecomeDead() { }

        public override void BecomeAlive()
        {
            Context.PowerupState = new GoombaAlive(Context);
            Context.Sprite = MovingEnemySpriteFactory.Create(EnemyType.Goomba);
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Context.TimeDead += gameTime.ElapsedGameTime.Milliseconds;
            if (Context.TimeDead >= AbstractEnemy.MaxTimeDead)
            {
                Context.BoundingBox = new HitBox(-4, -4, -4, -4, Color.Red);
                return true;
            }
            return false;
        }
    }
}
