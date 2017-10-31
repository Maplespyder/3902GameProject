using MarioClone.Factories;
using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class GoombaAlive : EnemyPowerupState
    {
        public GoombaAlive(AbstractEnemy context) : base(context) { }

        public override void BecomeAlive() { }

        public override void BecomeDead()
        {
            Context.PowerupState = new GoombaDead(Context);
            Context.Sprite = DeadEnemySpriteFactory.Create(EnemyType.Goomba);
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            return false;
        }
    }
}
