using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using static MarioClone.States.MarioPowerupState;

namespace MarioClone.States
{
    public class MarioIdle : MarioActionState
    {
        static MarioIdle _state;

        private MarioIdle(Mario context) : base(context)
        {
            Action = MarioAction.Idle;
        }

        public static MarioActionState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioIdle(Mario.Instance);
                }
                return _state;
            }
        }

        public override void BecomeCrouch()
        {
            if (Context.PowerupState.Powerup == MarioPowerup.Super || Context.PowerupState.Powerup == MarioPowerup.Fire)
            {
                Context.ActionState = MarioCrouch.Instance;
                Context.Sprite = Context.SpriteFactory.Create(MarioAction.Crouch); 
            }
        }

        public override void BecomeJump()
        {
            Context.ActionState = MarioJump.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Jump);
        }

        public override void BecomeWalk(Facing orientation)
        {
            if (Context.Orientation == orientation)
            {
                Context.ActionState = MarioWalk.Instance;
                Context.Sprite = Context.SpriteFactory.Create(MarioAction.Walk);
            }
            else if (orientation == Facing.Right)
            {
                Context.Orientation = Facing.Left;
            }
            else if (orientation == Facing.Left)
            {
                Context.Orientation = Facing.Right;
            }
        }
    }
}
