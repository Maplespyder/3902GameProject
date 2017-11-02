using MarioClone.GameObjects;
using MarioClone.Factories;
using static MarioClone.States.MarioActionState;
using System;
using Microsoft.Xna.Framework;
using MarioClone.Sounds;

namespace MarioClone.States
{
    public class MarioSuper : MarioPowerupState
    {
        static MarioSuper _state;

        private MarioSuper(Mario context) : base(context)
        {
            Powerup = MarioPowerup.Super;
        }

        public static MarioPowerupState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioSuper(Mario.Instance);
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
			SoundPool.Instance.GetAndPlay(SoundType.Down);
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

        }

        public override void BecomeFire()
        {
            Context.PowerupState = MarioFire.Instance;
            Context.SpriteFactory = FireMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
			SoundPool.Instance.GetAndPlay(SoundType.PowerUp);
		}

        public override void TakeDamage()
        {
            Context.Velocity = new Vector2(0, 0);
            Context.ActionState = MarioIdle.Instance;
            BecomeNormal();
        }
    }
}
