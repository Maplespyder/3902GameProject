using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System.Security.Cryptography;

namespace MarioClone.States.EnemyStates.Powerup
{
    public class BowserIdle : BowserActionState
    {
        private byte[] random = new Byte[1];
        public BowserIdle(BowserObject context) : base(context)
        {
        }

        public override void BreathFire()
        {
            /*Context.PowerupStateBowser = BowserFireBreathing.Instance;
            Context.ActionStateBowser = BowserIdle.Instance;
            Context.SpriteFactory = 
            */
            
        }

        public override void Idle()
        {
            
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Context.TimeIdle += gameTime.ElapsedGameTime.Milliseconds;
            if (Context.TimeIdle >= BowserObject.MaxTimeIdle)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(random);
                //logic for 


            }
            return false;

        }

        public override void Walk(Facing orientation)
        {
            
        }
    }
}
