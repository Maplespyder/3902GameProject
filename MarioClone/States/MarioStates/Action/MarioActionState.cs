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
            Jump,
            Crouch,
            Dead
        }

        protected Mario Context { get; set; }

        protected MarioAction LastState { get; set; }

        protected MarioActionState(Mario context)
        {
            Context = context;
        }

        public MarioAction Action { get; set; }

        // Behavior/actions

    

        public abstract void BecomeWalk(Facing orientation);
        public abstract void BecomeJump();
        public abstract void BecomeCrouch();
    }
}
