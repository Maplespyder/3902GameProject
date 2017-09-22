using MarioClone.GameObjects;
using MarioClone.Factories;

namespace MarioClone.States
{
    public class MarioSuper : MarioPowerupState
    {
        static MarioSuper _state;

        private MarioSuper(Mario context) : base(context) { }

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

        public override void BecomeNormal()
        {
            Context.PowerupState = MarioNormal.Instance;
            Context.SpriteFactory = NormalMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState);
        }

        public override void BecomeSuper()
        {
            // Do nothing
        }

        public override void BecomeFire()
        {
            Context.PowerupState = MarioFire.Instance;
            Context.SpriteFactory = FireMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState);
        }
    }
}
