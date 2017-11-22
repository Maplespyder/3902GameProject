using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States
{
    public class MarioWalk2 : MarioActionState
    {
        public MarioWalk2(Mario context) : base(context)
        {
            Action = MarioAction.Walk;
        }

        public override void Enter()
        {
            Context.Velocity = (Context.Orientation == Facing.Left) ? new Vector2(-Mario.HorizontalMovementSpeed, 0) 
                : new Vector2(Mario.HorizontalMovementSpeed, 0);
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Walk);
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
            if (Context.Orientation != orientation)
            {
                Context.Velocity = (orientation == Facing.Left) ? new Vector2(-Mario.HorizontalMovementSpeed, 0) 
                    : new Vector2(Mario.HorizontalMovementSpeed, 0);
                Context.Orientation = orientation;
            }
        }

        public override void ReleaseWalk(Facing orientation)
        {
            if (Context.Orientation == orientation)
            {
                Context.StateMachine.TransitionIdle();
            }
        }

        public override void UpdateHitBox()
        {
            if (Context.PowerupState is MarioNormal2)
            {
                if (Context.Orientation is Facing.Left) Context.BoundingBox.UpdateOffSets(-20, -28, -16, -1);
                if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-28, -20, -16, -1);
            }
            else if (Context.PowerupState is MarioSuper2 || Context.PowerupState is MarioFire2)
            {
                if (Context.Orientation is Facing.Left) Context.BoundingBox.UpdateOffSets(-20, -28, -20, -1);
                if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-28, -20, -20, -1);
            }
            else if (Context.PreviousPowerupState is MarioNormal2)
            {
                if (Context.Orientation is Facing.Left) Context.BoundingBox.UpdateOffSets(-20, -28, -16, -1);
                if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-28, -20, -16, -1);
            }
            else if (Context.PreviousPowerupState is MarioSuper2 || Context.PreviousPowerupState is MarioFire2)
            {
                if (Context.Orientation is Facing.Left) Context.BoundingBox.UpdateOffSets(-20, -28, -20, -1);
                if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-28, -20, -20, -1);
            }
        }
    }
}
