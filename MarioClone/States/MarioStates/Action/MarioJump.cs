using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MarioJump : MarioActionState
    {
        static MarioJump _state;

        private MarioJump(Mario context) : base(context)
        {
            Action = MarioAction.Jump;
        }

        public static MarioActionState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioJump(Mario.Instance);
                }
                return _state;
            }
        }

        public override void UpdateHitBox()
        {
            if (Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Normal)
            {
                Context.BoundingBox.UpdateOffSets(-4, -4, -2, 0);
            }
            else if (Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Super || Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Fire)
            {
                if (Context.Orientation.Equals(Facing.Left)) Context.BoundingBox.UpdateOffSets(-10, -10, -10, 0);
                if (Context.Orientation.Equals(Facing.Right)) Context.BoundingBox.UpdateOffSets(-10, -10, -10, 0);
            }
        }

        public override void BecomeCrouch()
        {
            Context.Velocity = new Vector2(0, 0);
            Context.ActionState = MarioIdle.Instance;
            Context.PreviousActionState = this;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Idle);
        }

        public override void BecomeJump()
        {
        }

        public override void BecomeWalk(Facing orientation)
        {
            Context.Orientation = orientation;
        }



    }
}
