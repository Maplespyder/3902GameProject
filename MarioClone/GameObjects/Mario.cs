using MarioClone.Collision;
using MarioClone.Factories;
using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarioClone.Collision.GameGrid;
using static MarioClone.States.MarioActionState;

namespace MarioClone.GameObjects
{
    public class Mario : AbstractGameObject
    {
        public const float HorizontalMovementSpeed = 1f;
        public const float VerticalMovementSpeed = 1f;
        private static Mario _mario;

        /// <summary>
        /// Do not instantiate Mario more than once. We have to make Mario before
        /// things that reference him use him, because I can't null check this getter.
        /// If you aren't sure what you're doing comes after Mario's creation, then
        /// null check the return on instance.
        /// </summary>
        public static Mario Instance
        {
            get
            {
                return _mario;
            }
        }

        public MarioActionState ActionState { get; set; }

        public MarioActionState PreviousActionState { get; set; }

        public MarioPowerupState PowerupState { get; set; }
        
        public MarioSpriteFactory SpriteFactory { get; set; }
        
        //passing null sprite because mario's states control his sprite
        public Mario(Vector2 position) : base(null, position, Color.Yellow)
        {
            _mario = this;
            PowerupState = MarioNormal.Instance;
            SpriteFactory = NormalMarioSpriteFactory.Instance;
            ActionState = MarioIdle.Instance;
            Sprite = SpriteFactory.Create(MarioAction.Idle);
            Orientation = Facing.Right;

            PreviousActionState = MarioIdle.Instance;
            
            ActionState.UpdateHitBox();
            BoundingBox.UpdateHitBox(position, Sprite);
        }

		public void MoveLeft()
		{
			ActionState.BecomeWalk(Facing.Left);
		}

        public void MoveRight()
        {
            ActionState.BecomeWalk(Facing.Right);
        }

		public void BecomeJump()
        {
            ActionState.BecomeJump();
        }

        public void BecomeCrouch()
        {
            ActionState.BecomeCrouch();
        }

        public void BecomeDead()
        {
            PowerupState.BecomeDead();
        }

        public void BecomeNormal()
        {
            PowerupState.BecomeNormal();
        }

        public void BecomeSuper()
        {
            PowerupState.BecomeSuper();
        }

        public void BecomeFire()
        {
            PowerupState.BecomeFire();
        }

        public override void CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is GoombaObject || gameObject is GreenKoopaObject || gameObject is RedKoopaObject)
            {
                if (side.Equals(Side.Top) || side.Equals(Side.Left) || side.Equals(Side.Right) && PowerupState is MarioNormal)
                {
                    BecomeDead();
                    Velocity = new Vector2(0, 0);
                }
                else if (side.Equals(Side.Top) || side.Equals(Side.Left) || side.Equals(Side.Right) && PowerupState is MarioSuper)
                {
                    BecomeNormal();
                    Velocity = new Vector2(0, 0);
                }
            }
            else if (gameObject is RedMushroomObject)
            {
               if (side.Equals(Side.Top) || side.Equals(Side.Left) || side.Equals(Side.Right) || side.Equals(Side.Bottom))
                {
                    BecomeSuper();
                }
            }
            else if (gameObject is FireFlowerObject)
            {
                if(side.Equals(Side.Top) || side.Equals(Side.Left) || side.Equals(Side.Right) || side.Equals(Side.Bottom))
                {
                    BecomeFire();
                }
            }
            else
            {
                Velocity = new Vector2(0, 0);
            }
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Position = new Vector2(Position.X + Velocity.X * percent, Position.Y + Velocity.Y * percent);
            ActionState.UpdateHitBox();
            return base.Update(gameTime, percent);
        }
    }
}