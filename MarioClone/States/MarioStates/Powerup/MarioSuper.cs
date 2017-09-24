using MarioClone.GameObjects;
using MarioClone.Factories;
using static MarioClone.States.MarioActionState;

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
            Context.SpriteFactory = DeadMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeNormal()
        {
            Context.PowerupState = MarioNormal.Instance;

            if (Context.ActionState.Action == MarioAction.Crouch)
            {
                Context.ActionState = MarioIdle.Instance;
            }

            Context.SpriteFactory = NormalMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }

        public override void BecomeSuper()
        {
            // Do nothing
        }

        public override void BecomeFire()
        {
            Context.PowerupState = MarioFire.Instance;
            Context.SpriteFactory = FireMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState.Action);
        }
    }
}
