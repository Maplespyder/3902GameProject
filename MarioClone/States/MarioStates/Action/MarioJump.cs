﻿using MarioClone.GameObjects;
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
