using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

namespace MarioClone.States
{
    public class MarioDead : MarioActionState
    {
        static MarioDead _state;

        private MarioDead(Mario context) : base(context) { }

        public static MarioActionState Instance
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

        public override void BecomeCrouch()
        {
            Context.ActionState = MarioCrouch.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Crouch);
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
            // Do nothing
        }
    }
}
