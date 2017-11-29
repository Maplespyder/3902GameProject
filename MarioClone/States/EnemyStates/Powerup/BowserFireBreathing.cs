﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System.Security.Cryptography;

namespace MarioClone.States.EnemyStates.Powerup
{
    class BowserFireBreathing : BowserActionState
    {
        private byte[] result;

        public BowserFireBreathing(BowserObject context) : base(context)
        {
            Action = BowserAction.BreatheFire;
        }

        public override void BreatheFire()
        {
        }

        public override void BecomeIdle()
        {
            Context.Velocity = new Vector2(0, 0);
            Context.ActionStateBowser = BowserIdle.Instance;
            Context.Sprite = Context.SpriteFactory.Create(BowserAction.Idle);
        }

        public override void BecomeWalk(Facing orientation)
        {
            Context.Velocity = orientation == Facing.Left ? new Vector2(-BowserObject.EnemyHorizontalMovementSpeed, 0) : new Vector2(BowserObject.EnemyHorizontalMovementSpeed, 0);
            Context.ActionStateBowser = BowserWalk.Instance;
            Context.Sprite = Context.SpriteFactory.Create(BowserAction.Walk);
            Context.Orientation = orientation;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Context.TimeFire += gameTime.ElapsedGameTime.Milliseconds;
            if (Context.TimeFire >= BowserObject.MaxTimeFire)
            {
                Context.TimeFire = 0;

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(random);
                randomResult = random[0] % 2;

                if (randomResult == 0)
                {
                    BecomeWalk(Context.Orientation);
                }
                else if (randomResult == 1)
                {
                    BecomeIdle();
                }

            }
            return false;

        }
    }
}
