using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public enum MarioAction
    {
        Idle,
        Walk,
        Jump,
        Crouch,
        Dead,
        Falling
    }

    public abstract class MarioActionState
    {

        protected Mario Context { get; set; }

        protected MarioAction LastState { get; set; }

        protected MarioActionState(Mario context)
        {
            Context = context;
        }

        public MarioAction Action { get; set; }

        // Behavior/actions

        public virtual void Walk(Facing orientation) { }
        public virtual void Jump() { }
        public virtual void Crouch() { }
        public virtual void ReleaseWalk(Facing orientation)
        {
            Context.Velocity = new Vector2(0, Context.Velocity.Y);
        }
        public virtual void ReleaseCrouch() { }
        public abstract void UpdateHitBox();

    }
}
