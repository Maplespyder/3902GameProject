using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MarioClone.GameObjects;

namespace MarioClone.States.EnemyStates.Powerup
{
    public class BowserInvincibility : BowserPowerupState
    {
        public int InvincibleTime { get; private set; }
        public int MaxInvincibleDuration { get; private set; }

        public BowserInvincibility(BowserObject context) : base(context)
        {
            Powerup = BowserPowerup.Invincible;
        }

        public override void BecomeDead(){ }

        public override void BecomeInvincible() { }

        public override bool Update(GameTime gameTime, float percent)
        {
            InvincibleTime += gameTime.ElapsedGameTime.Milliseconds;
            if (InvincibleTime >= MaxInvincibleDuration)
            {
                //BecomeWalk();

            }
            return false;
        }
    }
}

