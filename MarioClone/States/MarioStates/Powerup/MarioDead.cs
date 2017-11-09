using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MarioDead : MarioPowerupState
    {
        static MarioDead _state;

        private MarioDead(Mario context) : base(context)
        {
            Powerup = MarioPowerup.Dead;
        }

        public static MarioPowerupState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioDead(Mario.Instance);
                }
                return _state;
            }
        }

        public override void BecomeDead()
        {
        }

        public override void BecomeNormal()
        {
            Context.PowerupState = MarioNormal.Instance;
            Context.ActionState = MarioIdle.Instance;
            Context.SpriteFactory = NormalMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeSuper()
        {
            Context.PowerupState = MarioSuper.Instance;
            Context.ActionState = MarioIdle.Instance;
            Context.SpriteFactory = SuperMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeFire()
        {
            Context.PowerupState = MarioFire.Instance;
            Context.ActionState = MarioIdle.Instance;
            Context.SpriteFactory = FireMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

		public override void BecomeStar()
		{
		}

		public override void TakeDamage()
        {
        }

		public override void Update(GameTime gameTime)
		{
		}
	}
}
