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
        public const float GravityAcceleration = .1f;

        public const float HorizontalMovementSpeed = 2f;
        public const float VerticalMovementSpeed = 2f;
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
            if (!(PowerupState is MarioDead))
            {
                ActionState.Walk(Facing.Left); 
            }
		}

        public void MoveRight()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.Walk(Facing.Right); 
            }
        }

		public void Jump()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.Jump(); 
            }
        }

        public void Crouch()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.Crouch(); 
            }
        }

        public void ReleaseCrouch()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.ReleaseCrouch();
            }
        }

        public void ReleaseMoveLeft()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.ReleaseWalk(Facing.Left);
            }
        }

        public void ReleaseMoveRight()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.ReleaseWalk(Facing.Right);
            }
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

        private void TakeDamage()
        {
            PowerupState.TakeDamage();
        }

        public override void CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if ((gameObject is GoombaObject || gameObject is GreenKoopaObject || gameObject is RedKoopaObject) && (side.Equals(Side.Top) || side.Equals(Side.Left) || side.Equals(Side.Right)))
            {
                TakeDamage();
            }
            else if ((gameObject is HiddenBrickObject && side != Side.Top && !gameObject.Visible) || gameObject is CoinObject || gameObject is GreenMushroomObject)
            {
                // do nothing
            }
            else if (gameObject is AbstractBlock)
            {
                Velocity = new Vector2(0, 0);             
                Sprite = SpriteFactory.Create(MarioAction.Idle);
                PreviousActionState = ActionState;
                ActionState = MarioIdle.Instance;
            }
            else if (gameObject is RedMushroomObject)
            {
                BecomeSuper();
            }
            else if (gameObject is FireFlowerObject)
            {
                BecomeFire();
            }
            else if (ActionState is MarioJump)
            {
                Velocity = new Vector2(0, 0);
                Sprite = SpriteFactory.Create(MarioAction.Falling);
                PreviousActionState = ActionState;
                ActionState = MarioFall.Instance;
            }
            else
            {
                Velocity = new Vector2(0, 0);
                Sprite = SpriteFactory.Create(MarioAction.Idle);
                PreviousActionState = ActionState;
                ActionState = MarioIdle.Instance;
            }
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Velocity = new Vector2(Velocity.X, Velocity.Y);
            Position = new Vector2(Position.X + Velocity.X * percent, Position.Y + Velocity.Y * percent);
            ActionState.UpdateHitBox();
            return base.Update(gameTime, percent);
        }
    }
}