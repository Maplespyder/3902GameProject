using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

namespace MarioClone.States
{
    public class MarioCrouch : MarioActionState
    {
        static MarioCrouch _state;

        private MarioCrouch(Mario context) : base(context) { }

        public static MarioActionState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioCrouch(Mario.Instance);
                }
                return _state;
            }
        }

        public override void BecomeCrouch()
        {
            // Do nothing
        }

        public override void BecomeIdle()
        {
            Context.ActionState = MarioIdle.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Idle);
        }

        public override void BecomeJump()
        {
            Context.ActionState = MarioJump.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Jump);
        }

        public override void BecomeRunLeft()
        {
            Context.ActionState = MarioRunLeft.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.RunLeft);
        }

        public override void BecomeRunRight()
        {
            Context.ActionState = MarioRunRight.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.RunRight);
        }

        public override void BecomeDead()
        {
            Context.ActionState = MarioDead.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Dead);
        }
    }
}
