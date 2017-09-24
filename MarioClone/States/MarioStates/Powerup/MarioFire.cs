using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

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
            Context.SpriteFactory = DeadMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeNormal()
        {
            Context.PowerupState = MarioNormal.Instance;
            Context.SpriteFactory = NormalMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);            
        }

        public override void BecomeSuper()
        {
            Context.PowerupState = MarioSuper.Instance;
            Context.SpriteFactory = SuperMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeFire()
        {
            // Do nothing
        }
    }
}
