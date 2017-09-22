using MarioClone.GameObjects;
using MarioClone.Factories;

namespace MarioClone.States
{
    public class MarioNormal : MarioPowerupState
    {
        public MarioNormal(Mario context) : base(context)
        {
            context.SpriteFactory = NormalMarioSpriteFactory.Instance;
        }

        public override void BecomeNormal()
        {
            CheckNextState();
        }

        public override void BecomeSuper()
        {
            Context.PowerupState = new MarioSuper(Context);
            CheckNextState();
        }

        protected override void CheckNextState()
        {
            // check to see if mario is hit maybe? not sure what goes in here
        }
    }
}
