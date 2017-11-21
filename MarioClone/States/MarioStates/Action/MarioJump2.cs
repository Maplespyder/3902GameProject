using MarioClone.Collision;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States
{
    public class MarioJump2 : MarioActionState
    {
        public MarioJump2(Mario context) : base(context)
        {
            Action = MarioAction.Jump;
        }

        public override void Enter()
        {
            Context.Velocity = new Vector2(Context.Velocity.X, -Mario.VerticalMovementSpeed);
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Jump);
            UpdateHitBox();
        }
        
        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is AbstractBlock)
            {
                if (side == Side.Bottom)
                {
                    Context.Gravity = false;
                    Context.Velocity = new Vector2(Context.Velocity.X, 0);

                    if (Context.Velocity.X != 0)
                    {
                        Context.StateMachine.TransitionWalk();
                        return true;
                    }
                    else
                    {
                        Context.StateMachine.TransitionIdle();
                        return true;
                    }
                }
                else if (side == Side.Left || side == Side.Right)
                {
                    Context.Velocity = new Vector2(0, Context.Velocity.Y);
                    return true;
                }
                else if(side == Side.Top)
                {
                    Context.Velocity = new Vector2(Context.Velocity.X, 0);
                    Context.StateMachine.TransitionFall();
                    return true;
                }
            }
            return false;
        }

        public override void UpdateHitBox()
        {
            if (Context.PowerupState is MarioNormal2)
            {
                Context.BoundingBox.UpdateOffSets(-8, -8, -4, -1);
            }
            else if (Context.PowerupState is MarioSuper2 || Context.PowerupState is MarioFire2)
            {
                if (Context.Orientation.Equals(Facing.Left)) Context.BoundingBox.UpdateOffSets(-20, -20, -20, -1);
                if (Context.Orientation.Equals(Facing.Right)) Context.BoundingBox.UpdateOffSets(-20, -20, -20, -1);
            }
            else if (Context.PreviousPowerupState is MarioNormal2)
            {
                Context.BoundingBox.UpdateOffSets(-8, -8, -4, -1);
            }
            else if (Context.PreviousPowerupState is MarioSuper2 || Context.PreviousPowerupState is MarioFire2)
            {
                if (Context.Orientation.Equals(Facing.Left)) Context.BoundingBox.UpdateOffSets(-20, -20, -20, -1);
                if (Context.Orientation.Equals(Facing.Right)) Context.BoundingBox.UpdateOffSets(-20, -20, -20, -1);
            }
        }

        public override void Walk(Facing orientation)
        {
            Context.Velocity = (orientation == Facing.Left) ? new Vector2(-Mario.HorizontalMovementSpeed, Context.Velocity.Y) 
                : new Vector2(Mario.HorizontalMovementSpeed, Context.Velocity.Y);
            Context.Orientation = orientation;
        }

        public override void ReleaseWalk(Facing orientation)
        {
            if (Context.Orientation == orientation)
            {
                Context.Velocity = new Vector2(0, Context.Velocity.Y);
            }
        }
    }
}
