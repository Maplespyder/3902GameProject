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
    public class MarioDash : MarioActionState
    {
        private float _initialX;

        public const int DashLength = 200;
        public const float DashSpeed = 20;

        public bool DashFinished
        {
            get
            {
                return Math.Abs(_initialX - Context.Position.X) > DashLength || Context.Velocity.X == 0;
            }
        }

        public MarioDash(Mario context) : base(context)
        {
            Action = MarioAction.Dash;
        }

        public override void Enter()
        {
            _initialX = Context.Position.X;

            if (Context.Orientation == Facing.Right)
            {
                Context.Velocity = new Vector2(DashSpeed, 0);
            }
            else
            {
                Context.Velocity = new Vector2(-DashSpeed, 0);
            }
            
            Context.Sprite = Context.SpriteFactory.Create(MarioAction.Dash);
            UpdateHitBox();
        }

        public override void Leave()
        {
            Context.Velocity = new Vector2(0, Context.Velocity.Y);
        }

        public override void Jump()
        {
            if (Math.Abs(Context.Velocity.Y) > 0)
            {
                Context.StateMachine.TransitionJump(); 
            }
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is AbstractBlock)
            {
                if (side == Side.Left || side == Side.Right)
                {
                    Context.StateMachine.TransitionIdle();
                }
            }
            return base.CollisionResponse(gameObject, side, gameTime);
        }

        public override void UpdateHitBox()
        {
            if (Context.PowerupState is MarioNormal2)
            {
                if (Context.Orientation is Facing.Left) Context.BoundingBox.UpdateOffSets(-10, -28, -20, -1);
                if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-28, -10, -20, -1);
            }
            else if (Context.PowerupState is MarioSuper2 || Context.PowerupState is MarioFire2)
            {
                if (Context.Orientation is Facing.Left) Context.BoundingBox.UpdateOffSets(-11, -32, -25, -1);
                if (Context.Orientation is Facing.Right) Context.BoundingBox.UpdateOffSets(-32, -11, -25, -1);
            }
        }
    }
}
