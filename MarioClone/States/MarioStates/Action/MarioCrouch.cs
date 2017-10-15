using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MarioCrouch : MarioActionState
    {
        static MarioCrouch _state;

        private MarioCrouch(Mario context) : base(context)
        {
            Action = MarioAction.Crouch;
        }

        public static MarioActionState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioCrouch(Mario.Instance);
                }
                return _state;
            }
        }

        public override void BecomeCrouch()
        {
        }

        public override void UpdateHitBox()
        {
            if (Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Normal)
            {
                Context.BoundingBox.UpdateOffSets(-4, -4, -2, 0);
            }
            else if (Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Super || Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Fire)
            {
                Context.BoundingBox.UpdateOffSets(-10, -10, -28, 0);
            }
        }

        public override void BecomeJump()
        {
            Context.Velocity = new Vector2(0, 0);
            Context.ActionState = MarioIdle.Instance;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Idle);
            Context.PreviousActionState = this;
        }

        public override void BecomeWalk(Facing orientation)
        {
            Mario.Instance.Orientation = orientation;
        }
    }
}
