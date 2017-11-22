using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States
{
    public class MarioIdle2 : MarioActionState
    {
        public MarioIdle2(Mario context) : base(context)
        {
            Action = MarioAction.Idle;
        }

        public override void Enter()
        {
            Context.Velocity = new Vector2(0, 0);
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Idle);
            UpdateHitBox();
        }

        public override void Crouch()
        {
            Context.StateMachine.TransitionCrouch();
        }

        public override void Jump()
        {
            Context.StateMachine.TransitionJump();
        }

        public override void Walk(Facing orientation)
        {
            Context.Orientation = orientation;
            Context.StateMachine.TransitionWalk();
        }

        public override void UpdateHitBox()
        {
            if (Context.PowerupState is MarioNormal2)
            {
                if (Context.Orientation is Facing.Left)Context.BoundingBox.UpdateOffSets(-10, -28, -20, -1);
                if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-28, -10, -20, - 1);
            }
            else if (Context.PowerupState is MarioSuper2 || Context.PowerupState is MarioFire2)
            {         
                if (Context.Orientation is Facing.Left) Context.BoundingBox.UpdateOffSets(-11, -32, -25, -1);
                if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-32, -11, -25, -1);
            }
        }
    }
}
