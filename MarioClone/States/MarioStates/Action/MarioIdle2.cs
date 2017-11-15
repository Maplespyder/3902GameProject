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
            //TODO add case for if you're star or invincible or dead
            if (Context.PowerupState is MarioNormal2)
            {
                Context.BoundingBox.UpdateOffSets(-8, -8, -4, -1);
            }
            else if (Context.PowerupState is MarioSuper2 || Context.PowerupState is MarioFire2)
            {
                Context.BoundingBox.UpdateOffSets(-20, -20, -20, -1);
            }
        }
    }
}
