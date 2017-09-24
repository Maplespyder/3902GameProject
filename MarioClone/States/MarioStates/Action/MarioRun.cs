using MarioClone.GameObjects;
using MarioClone.Factories;
using System;

namespace MarioClone.States
{
    public class MarioRun : MarioActionState
    {
        static MarioRun _state;

        private MarioRun(Mario context) : base(context)
        {
            Action = MarioAction.Run;
        }

        public static MarioActionState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioRun(Mario.Instance);
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

        public override void BecomeRun()
        {
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
