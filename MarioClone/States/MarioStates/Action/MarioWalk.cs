using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using Microsoft.Xna.Framework;
using MarioClone.Sounds;

namespace MarioClone.States
{
    public class MarioWalk : MarioActionState
    {
        static MarioWalk _state;

        private MarioWalk(Mario context) : base(context)
        {
            Action = MarioAction.Walk;
        }

        public static MarioActionState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new MarioWalk(Mario.Instance);
                }
                return _state;
            }
        }

        public override void Crouch()
        {
            if (Context.PowerupState is MarioSuper || Context.PowerupState is MarioFire)
            {
                Context.Velocity = new Vector2(0, 0);
                Context.ActionState = MarioCrouch.Instance;
                Context.PreviousActionState = this;
                Context.Sprite = Context.SpriteFactory.Create(MarioAction.Crouch);
            }
        }

        public override void Jump()
        {
            Context.Velocity = new Vector2(Context.Velocity.X, -Mario.VerticalMovementSpeed);
            Context.ActionState = MarioJump.Instance;
            Context.PreviousActionState = this;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Jump);
			SoundPool.Instance.GetAndPlay(SoundType.Jump);
		}

        public override void UpdateHitBox()
        {
            if (Context.PowerupState is MarioNormal)
            {
                Context.BoundingBox.UpdateOffSets(-8, -8, -4, -1);
            }
            else if (Context.PowerupState is MarioSuper || Context.PowerupState is MarioFire)
            {
                Context.BoundingBox.UpdateOffSets(-20, -20, -20, -1);
            }
        }

        public override void Walk(Facing orientation)
        {
            if (Context.Orientation != orientation)
            {
                Context.Velocity = orientation == Facing.Left ? new Vector2(-Mario.HorizontalMovementSpeed, 0) : new Vector2(Mario.HorizontalMovementSpeed, 0);
                Context.ActionState = MarioWalk.Instance;
                Context.PreviousActionState = this;
                Context.Sprite = Context.SpriteFactory.Create(MarioAction.Walk);
                Context.Orientation = orientation;
            }
        }

        public override void ReleaseWalk(Facing orientation)
        {
            if (Context.Orientation == orientation)
            {
                Context.Velocity = new Vector2(0, 0);
                Context.ActionState = MarioIdle.Instance;
                Context.PreviousActionState = this;
                Context.Sprite = Context.SpriteFactory.Create(MarioAction.Idle);
            }
        }
    }
}
