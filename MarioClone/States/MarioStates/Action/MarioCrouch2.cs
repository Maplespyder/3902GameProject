using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States
{
    public class MarioCrouch2 : MarioActionState
    {
        public MarioCrouch2(Mario context) : base(context)
        {
            Action = MarioAction.Crouch;
        }

        public override void Enter()
        {
            Context.Velocity = new Vector2(0, 0);
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Crouch);
            UpdateHitBox();
        }

        public override void ReleaseCrouch()
        {
            Context.StateMachine.TransitionIdle();
        }

        public override void UpdateHitBox()
        {

            if (Context.PowerupState is MarioSuper2 || Context.PowerupState is MarioFire2)
            {
                if(Context.Orientation is Facing.Left) Context.BoundingBox.UpdateOffSets(-9, -32, -16, -1);
                if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-32, -9, -16, -1);
            }
            else if (Context.PreviousPowerupState is MarioSuper2 || Context.PreviousPowerupState is MarioFire2)
            {
                if (Context.Orientation is Facing.Left)Context.BoundingBox.UpdateOffSets(-9, -32, -16, -1);
                if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-32, -9, -16, -1);
            }
        }
    }
}
