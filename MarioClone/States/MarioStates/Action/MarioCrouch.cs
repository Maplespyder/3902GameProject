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
