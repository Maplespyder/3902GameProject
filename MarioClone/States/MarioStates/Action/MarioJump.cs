using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

namespace MarioClone.States
{
    public class MarioJump : MarioActionState
    {
        static MarioJump _state;

        private MarioJump(Mario context) : base(context)
        {
            Action = MarioAction.Jump;
        }

        public static MarioActionState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioJump(Mario.Instance);
                }
                return _state;
            }
        }

        public override void BecomeCrouch()
        {
            Context.ActionState = Context.PreviousActionState;
            Context.Sprite = Context.SpriteFactory.Create(Context.PreviousActionState.Action);
            Context.PreviousActionState = this;
        }

        public override void BecomeJump()
        {
        }

        public override void BecomeWalk(Facing orientation)
        {
            Context.Orientation = orientation;
        }
    }
}
