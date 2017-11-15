using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using MarioClone.Collision;

namespace MarioClone.States
{
    public enum MarioAction
    {
        Idle,
        Walk,
        Jump,
        Crouch,
        Dead,
        Fall
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
        public virtual bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is AbstractBlock)
            {
                if (side == Side.Bottom)
                {
                    Context.Gravity = false;
                    Context.Velocity = new Vector2(Context.Velocity.X, 0);
                    return true;
                }
                else if (side == Side.Left || side == Side.Right)
                {
                    Context.Velocity = new Vector2(0, Context.Velocity.Y);
                    return true;
                }
                else if (side == Side.Top)
                {
                    Context.Velocity = new Vector2(Context.Velocity.X, 0);
                    Context.StateMachine.TransitionFall();
                    return true;
                }
            }

            return false;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
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
