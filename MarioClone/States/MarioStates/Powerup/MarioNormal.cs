using MarioClone.GameObjects;
using MarioClone.Factories;

namespace MarioClone.States
{
    public class MarioNormal : MarioPowerupState
    {
        static MarioNormal _state;

        private MarioNormal(Mario context) : base(context) { }

        public static MarioPowerupState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioNormal(Mario.Instance);
                }
                return _state;
            }
        }

        public override void BecomeNormal()
        {
            // Do nothing
        }

        public override void BecomeSuper()
        {
            Context.PowerupState = MarioSuper.Instance;
            Context.SpriteFactory = SuperMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState);            
        }

        public override void BecomeFire()
        {
            Context.PowerupState = MarioFire.Instance;
            Context.SpriteFactory = FireMarioSpriteFactory.Instance;
            Context.Sprite = Context.SpriteFactory.Create(Context.ActionState);            
        }
    }
}
