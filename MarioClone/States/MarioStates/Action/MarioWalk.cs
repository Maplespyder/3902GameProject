using MarioClone.GameObjects;
using MarioClone.Factories;
using System;
using Microsoft.Xna.Framework;

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

        public override void BecomeCrouch()
        {
        }

        public override void BecomeJump()
        { 
        }

        public override void UpdateHitBox()
        {
            if (Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Normal)
            {
                Context.BoundingBox.UpdateOffSets(-8, -8, -4, 0);
            }
            else if (Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Super || Context.PowerupState.Powerup == MarioPowerupState.MarioPowerup.Fire)
            {
                Context.BoundingBox.UpdateOffSets(-20, -20, -20, 0);
            }
        }

        public override void BecomeWalk(Facing orientation)
        {
            if (Context.Orientation != orientation)
            {
                Context.Velocity = new Vector2(0, 0);
                Context.ActionState = MarioIdle.Instance;
                Context.PreviousActionState = this;
                Context.Sprite = Context.SpriteFactory.Create(MarioAction.Idle);
            }
        }
    }
}
