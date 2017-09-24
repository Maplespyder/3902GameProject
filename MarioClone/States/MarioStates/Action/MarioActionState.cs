using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;

namespace MarioClone.States
{
    public abstract class MarioActionState
    {
        public enum MarioAction
        {
            Idle,
            Walk,
            Fall,
            Run,
            Jump,
            Crouch,
            Dead
        }

        protected Mario Context { get; set; }

        public MarioActionState(Mario context)
        {
            Context = context;
        }

        public MarioAction Action { get; set; }

        // Behavior/actions

        public void Move()
        {
            // mario cannot move currently
        }

        public abstract void BecomeIdle();
        public abstract void BecomeRun();
        public abstract void BecomeWalk();
        public abstract void BecomeFall();
        public abstract void BecomeJump();
        public abstract void BecomeCrouch();
    }
}
