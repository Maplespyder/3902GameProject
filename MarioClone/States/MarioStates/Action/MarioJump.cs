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
                Context.BoundingBox.UpdateOffSets(0, 0, -32, 0);
            }
            else if (Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Super || Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Fire)
            {
                if (Context.Orientation.Equals(Facing.Left)) Context.BoundingBox.UpdateOffSets(-6, -10, -12, 0);
                if (Context.Orientation.Equals(Facing.Right)) Context.BoundingBox.UpdateOffSets(-10, -6, -12, 0);
            }
        }

        public override void BecomeCrouch()
        {
            Context.ActionState = Context.PreviousActionState;
            Context.Sprite = Context.SpriteFactory.Create(Context.PreviousActionState.Action);
            Context.PreviousActionState = this;
        }

        public override void BecomeJump()
        {
            Context.Velocity = new Vector2(0, 0);
            Context.ActionState = MarioIdle.Instance;
            Context.PreviousActionState = this;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Idle);

        }

        public override void BecomeWalk(Facing orientation)
        {
            Context.Orientation = orientation;
        }

          /*if (Context.ActionState.Action == MarioActionState.MarioAction.Idle)
            {
                Context.Velocity = new Vector2(0, Mario.VerticalMovementSpeed);
    }
            else if (Context.ActionState.Action == MarioActionState.MarioAction.Jump)
            {
                Context.ActionState = MarioIdle.Instance;
                Context.PreviousActionState = this;
                Context.Sprite = Context.SpriteFactory.Create(MarioAction.Idle);
            }*/
}
}
