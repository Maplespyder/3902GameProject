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

        public override void ReleaseCrouch()
        {
            Context.ActionState = MarioIdle.Instance;
            Context.PreviousActionState = this;
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Idle);
        }

        public override void UpdateHitBox()
        {
            if (Context.PowerupState is MarioNormal)
            {
                Context.BoundingBox.UpdateOffSets(-8, -8, -4, -1);
            }
            else if (Context.PowerupState is MarioSuper || Context.PowerupState is MarioFire)
            {
                Context.BoundingBox.UpdateOffSets(-20, -20, -56, -1);
            }
        }
    }
}
