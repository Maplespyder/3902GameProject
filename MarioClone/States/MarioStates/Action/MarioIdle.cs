using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using static MarioClone.States.MarioPowerupState;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MarioIdle : MarioActionState
    {
        static MarioIdle _state;

        private MarioIdle(Mario context) : base(context)
        {
            Action = MarioAction.Idle;
        }

        public static MarioActionState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioIdle(Mario.Instance);
                }
                return _state;
            }
        }

        public override void BecomeCrouch()
        {

            Context.Velocity = new Vector2(0, Mario.VerticalMovementSpeed);
            Context.ActionState = MarioCrouch.Instance;
            Context.PreviousActionState = this;

            if (Context.PowerupState.Powerup == MarioPowerup.Super || Context.PowerupState.Powerup == MarioPowerup.Fire)
            {         
                Context.Sprite = Context.SpriteFactory.Create(MarioAction.Crouch);
            }

        }
        public override void UpdateHitBox()
        {
            if (Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Normal)
            {
               Context.BoundingBox.UpdateOffSets(-4,-4, -2, 0);
            }
            else if (Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Super || Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Fire)
            {
                Context.BoundingBox.UpdateOffSets(-10, -10, -10, 0);
            }
        }

        public override void BecomeJump()
        {
            Context.Velocity = new Vector2(0, -Mario.VerticalMovementSpeed);
            Context.ActionState = MarioJump.Instance;
            Context.PreviousActionState = this;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Jump);
        }

        public override void BecomeWalk(Facing orientation)
        {
            if (Context.Orientation == orientation)
            {
                Context.Velocity = orientation == Facing.Left ? new Vector2(-Mario.HorizontalMovementSpeed, 0) : new Vector2(Mario.HorizontalMovementSpeed, 0);
                Context.ActionState = MarioWalk.Instance;
                Context.PreviousActionState = this;
                Context.Sprite = Context.SpriteFactory.Create(MarioAction.Walk);
            }
            else
            {
                Context.Orientation = orientation;
            }
        }
    }
}
