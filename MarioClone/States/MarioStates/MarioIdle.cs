using MarioClone.GameObjects;
using MarioClone.Factories;

namespace MarioClone.States
{
    public class MarioIdle : MarioActionState
    {
        public MarioIdle(Mario context) : base(context)
        {
            context.SpriteFactory.Create(this);
        }

        public override void BecomeDead()
        {
            // change values of mario like velocity
            // then do something like Context.MarioActionState = new MarioDying(this);
            CheckNextState();
        }

        public override void BecomeIdle()
        {
            CheckNextState();
        }

        protected override void CheckNextState()
        {
            
        }
    }
}
