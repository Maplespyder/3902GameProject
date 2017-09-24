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
            RunLeft,
            RunRight,
            Jump,
            Crouch,
            Dead
        }

        protected Mario Context { get; set; }

        public MarioActionState(Mario context)
        {
            Context = context;
        }

        // Behavior/actions

        public void Move()
        {
            // mario cannot move currently
        }

        public abstract void BecomeIdle();
        public abstract void BecomeRunLeft();
        public abstract void BecomeRunRight();
        public abstract void BecomeJump();
        public abstract void BecomeCrouch();
        public abstract void BecomeDead();
    }
}
