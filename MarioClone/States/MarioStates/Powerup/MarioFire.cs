using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using static MarioClone.States.MarioActionState;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MarioFire : MarioPowerupState
    {
        static MarioFire _state;

        private MarioFire(Mario context) : base(context)
        {
            Powerup = MarioPowerup.Fire;
        }

        public static MarioPowerupState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioFire(Mario.Instance);
                }
                return _state;
            }
        }

        public override void BecomeDead()
        {
            Context.PowerupState = MarioDead.Instance;
            Context.ActionState = MarioIdle.Instance;
            Context.SpriteFactory = DeadMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeNormal()
        {
            Context.PowerupState = MarioNormal.Instance;
            Context.SpriteFactory = NormalMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
            if (Context.ActionState.Action == MarioAction.Crouch)
            {
                return;
            }
            if (Context.Orientation == Facing.Left)
            {
                Context.Position = new Vector2(Context.Position.X + 10, Context.Position.Y + 8);
            }
            else
            {
                Context.Position = new Vector2(Context.Position.X + 6, Context.Position.Y + 8);
            }
        }

        public override void BecomeSuper()
        {
            Context.PowerupState = MarioSuper.Instance;
            Context.SpriteFactory = SuperMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeFire()
        {

        }

        public override void TakeDamage()
        {
            Context.Velocity = new Vector2(0, 0);
            Context.ActionState = MarioIdle.Instance;
            BecomeSuper();
        }
    }
}
