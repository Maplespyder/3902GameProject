using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

namespace MarioClone.States
{
    public class MarioCrouch : MarioActionState
    {
        static MarioCrouch _state;

        private MarioCrouch(Mario context) : base(context)
        {
            Action = MarioAction.Crouch;
        }

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
        }

        public override void BecomeJump()
        {
            Context.ActionState = Context.PreviousActionState;
            Context.Sprite = Context.SpriteFactory.Create(Context.PreviousActionState.Action);
            Context.PreviousActionState = this;
        }

        public override void BecomeWalk(Facing orientation)
        {
            Mario.Instance.Orientation = orientation;
        }
    }
}
