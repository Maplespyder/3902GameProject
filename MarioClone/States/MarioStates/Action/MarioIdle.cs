using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

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
            Context.ActionState = MarioCrouch.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Crouch);
        }

        public override void BecomeIdle()
        {
        }

        public override void BecomeJump()
        {
            Context.ActionState = MarioJump.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Jump);
        }

        public override void BecomeRun()
        {
            Context.ActionState = MarioRun.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Run);
        }

        public override void BecomeWalk()
        {
            Context.ActionState = MarioWalk.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Walk);
        }

        public override void BecomeFall()
        {
            Context.ActionState = MarioFall.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Fall);
        }
    }
}
