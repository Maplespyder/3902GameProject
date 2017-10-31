using MarioClone.Collision;
using MarioClone.Factories;
using MarioClone.States;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class Mario : AbstractGameObject
    {
        public const float GravityAcceleration = .4f;

        public const float HorizontalMovementSpeed = 3f;
        public const float VerticalMovementSpeed = 15f;
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

        public bool Gravity { get; set; }

        //passing null sprite because mario's states control his sprite
        public Mario(Vector2 position) : base(null, position, Color.Yellow)
        {
            _mario = this;
            PowerupState = MarioNormal.Instance;
            SpriteFactory = NormalMarioSpriteFactory.Instance;
            ActionState = MarioFall.Instance;
            Sprite = SpriteFactory.Create(MarioAction.Falling);
            Orientation = Facing.Right;
            Gravity = true;

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

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {

            if ((gameObject is AbstractEnemy) && (side.Equals(Side.Top) || side.Equals(Side.Left) || side.Equals(Side.Right)))
            {
                TakeDamage();
            }
            else if ((gameObject is HiddenBrickObject && side != Side.Top && !gameObject.Visible) || gameObject is CoinObject || gameObject is GreenMushroomObject)
            {
                return false;
            }
            else if (gameObject is AbstractBlock)
            {
                if (side == Side.Bottom)
                {
                    Gravity = false;
                    Velocity = new Vector2(Velocity.X, 0);

                    if ((ActionState is MarioFall || ActionState is MarioJump))
                    {
                        if (Velocity.X > 0)
                        {
                            Sprite = SpriteFactory.Create(MarioAction.Walk);
                            PreviousActionState = ActionState;
                            ActionState = MarioWalk.Instance;
                        }
                        else if (Velocity.X < 0)
                        {
                            Sprite = SpriteFactory.Create(MarioAction.Walk);
                            PreviousActionState = ActionState;
                            ActionState = MarioWalk.Instance;
                        }
                        else
                        {
                            Sprite = SpriteFactory.Create(MarioAction.Idle);
                            PreviousActionState = ActionState;
                            ActionState = MarioIdle.Instance;
                        } 
                    }
                }
                else if (side == Side.Left || side == Side.Right)
                {
                    Velocity = new Vector2(0, Velocity.Y);
                }
                else if (side == Side.Top)
                {
                    Velocity = new Vector2(Velocity.X, 0);
                    Sprite = SpriteFactory.Create(MarioAction.Falling);
                    PreviousActionState = ActionState;
                    ActionState = MarioFall.Instance;
                }
            }
            else if (gameObject is RedMushroomObject)
            {
                BecomeSuper();
            }
            else if (gameObject is FireFlowerObject)
            {
                BecomeFire();
            }         
            else
            {
                Velocity = new Vector2(0, 0);
                Sprite = SpriteFactory.Create(MarioAction.Idle);
                PreviousActionState = ActionState;
                ActionState = MarioIdle.Instance;
            }
            return true;
        }

        public override void FixClipping(Vector2 correction, AbstractGameObject obj1, AbstractGameObject obj2)
        {
            Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
            BoundingBox.UpdateHitBox(Position, Sprite);
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            Position = new Vector2(Position.X + Velocity.X * percent, Position.Y + Velocity.Y * percent);
            ActionState.UpdateHitBox();

            if (Gravity)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + GravityAcceleration * percent);
            }
            Gravity = true;

            if (!(ActionState is MarioFall) && Velocity.Y > 1.5)
            {
                Sprite = SpriteFactory.Create(MarioAction.Falling);
                PreviousActionState = ActionState;
                ActionState = MarioFall.Instance;
            }

            return base.Update(gameTime, percent);    
        }
    }
}