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
                if (Context.Orientation is Facing.Left)
                {
                    Context.BoundingBox.UpdateOffSets(-10, -28, -25, 0);
                }
                else
                {
                    Context.BoundingBox.UpdateOffSets(-28, -10, -25, 0);
                }
            }
            else if (Context.PowerupState is MarioSuper2 || Context.PowerupState is MarioFire2)
            {         
                if (Context.Orientation is Facing.Left)
                {
                    Context.BoundingBox.UpdateOffSets(-11, -32, -22, 0);
                }
                else
                {
                    Context.BoundingBox.UpdateOffSets(-32, -11, -22, 0);
                }
            }
            else if (Context.PreviousPowerupState is MarioNormal2)
            {
                if (Context.Orientation is Facing.Left)
                {
                    Context.BoundingBox.UpdateOffSets(-10, -28, -25, 0);
                }
                else
                {
                    Context.BoundingBox.UpdateOffSets(-28, -10, -25, 0);
                }
            }
            else if (Context.PreviousPowerupState is MarioSuper2 || Context.PreviousPowerupState is MarioFire2)
            {
                if (Context.Orientation is Facing.Left)
                {
                    Context.BoundingBox.UpdateOffSets(-11, -32, -22, 0);
                }
                else
                {
                    Context.BoundingBox.UpdateOffSets(-32, -11, -22, 0);
                }
            }
        }
    }
}
