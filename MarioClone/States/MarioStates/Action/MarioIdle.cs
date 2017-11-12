using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using static MarioClone.States.MarioPowerupState;
using Microsoft.Xna.Framework;
using MarioClone.Sounds;
using Microsoft.Xna.Framework.Audio;

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

        public override void Crouch()
        {
            if (!(Context.PowerupState is MarioNormal))
            {
                Context.ActionState = MarioCrouch.Instance;
                Context.PreviousActionState = this;
                Context.Sprite = Context.SpriteFactory.Create(MarioAction.Crouch);
            }
        }

        public override void UpdateHitBox()
        {
            if (Context.PowerupState is MarioNormal)
            {
               Context.BoundingBox.UpdateOffSets(-8,-8, -4, -1);
            }
            else if (Context.PowerupState is MarioSuper || Context.PowerupState is MarioFire)
            {
                Context.BoundingBox.UpdateOffSets(-20, -20, -20, -1);
            }
        }

        public override void Jump()
        {
            Context.Velocity = new Vector2(0, -Mario.VerticalMovementSpeed);
            Context.ActionState = MarioJump.Instance;
            Context.PreviousActionState = this;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Jump);
		}

        public override void Walk(Facing orientation)
        {
            Context.Velocity = orientation == Facing.Left ? new Vector2(-Mario.HorizontalMovementSpeed, 0) : new Vector2(Mario.HorizontalMovementSpeed, 0);
            Context.ActionState = MarioWalk.Instance;
            Context.PreviousActionState = this;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Walk);
            Context.Orientation = orientation;
        }
    }
}
