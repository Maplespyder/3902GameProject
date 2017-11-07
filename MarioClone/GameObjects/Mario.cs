﻿using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.Factories;
using MarioClone.States;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class Mario : AbstractGameObject
    {
        public const float GravityAcceleration = .4f;

        public const float HorizontalMovementSpeed = 5f;
        public const float VerticalMovementSpeed = 15f;
        private static Mario _mario;
        private bool bouncing = false;

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
        
        public MarioPowerupState PreviousPowerupState { get; set; }

        public MarioSpriteFactory SpriteFactory { get; set; }

        public int BounceCount { get; set; }

        public bool Gravity { get; set; }

        public int Lives { get; set; }

        public int CoinCount { get; set; }

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
            BounceCount = 0;
            Lives = 3;
            CoinCount = 0;

            PreviousPowerupState = PowerupState;
            PreviousActionState = MarioIdle.Instance;
            
            ActionState.UpdateHitBox();
            BoundingBox.UpdateHitBox(position, Sprite);
        }

		public void MoveLeft()
		{
            if (!(PowerupState is MarioDead))
            {
                ActionState.Walk(Facing.Left);
                EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
            }
		}

        public void MoveRight()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.Walk(Facing.Right);
                EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
            }
        }

		public void Jump()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.Jump();
                EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
            }
        }

        public void Crouch()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.Crouch();
                EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
            }
        }

        public void ReleaseCrouch()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.ReleaseCrouch();
                EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
            }
        }

        public void ReleaseMoveLeft()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.ReleaseWalk(Facing.Left);
                EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
            }
        }

        public void ReleaseMoveRight()
        {
            if (!(PowerupState is MarioDead))
            {
                ActionState.ReleaseWalk(Facing.Right);
                EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
            }
        }

        public void BecomeDead()
        {
            PowerupState.BecomeDead();
            EventManager.Instance.TriggerMarioPowerupStateChangedEvent(this);
        }

        public void BecomeNormal()
        {
            PowerupState.BecomeNormal();
            EventManager.Instance.TriggerMarioPowerupStateChangedEvent(this);
        }

        public void BecomeSuper()
        {
            PowerupState.BecomeSuper();
            EventManager.Instance.TriggerMarioPowerupStateChangedEvent(this);
        }

        public void BecomeFire()
        {
            PowerupState.BecomeFire();
            EventManager.Instance.TriggerMarioPowerupStateChangedEvent(this);
        }

        private void TakeDamage()
        {
            PowerupState.TakeDamage();
            EventManager.Instance.TriggerMarioPowerupStateChangedEvent(this);
        }

        private void ManageBouncing(AbstractGameObject gameObject, Side side)
        {
            if(gameObject is AbstractEnemy && side.Equals(Side.Bottom))
            {
                if (bouncing)
                {
                    BounceCount += 1;
                }
                else
                {
                    bouncing = true;
                }
            }
            else if (side.Equals(Side.Bottom))
            {
                BounceCount = 0;
                bouncing = false;
            }
        }
        

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            ManageBouncing(gameObject, side);

            if ((gameObject is AbstractEnemy) && (side.Equals(Side.Top) || side.Equals(Side.Left) || side.Equals(Side.Right)))
            {
                TakeDamage();
            }
            else if ((gameObject is AbstractEnemy) && side.Equals(Side.Bottom))
            { 
                Velocity = new Vector2(Velocity.X, -7);
            }
            else if ((((gameObject is HiddenBrickObject && side != Side.Top && !gameObject.Visible) 
                || (gameObject is HiddenBrickObject && side == Side.Top && !gameObject.Visible && (ActionState is MarioFall)))
                || gameObject is CoinObject || gameObject is GreenMushroomObject))
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
                            EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
                        }
                        else if (Velocity.X < 0)
                        {
                            Sprite = SpriteFactory.Create(MarioAction.Walk);
                            PreviousActionState = ActionState;
                            ActionState = MarioWalk.Instance;
                            EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
                        }
                        else
                        {
                            Sprite = SpriteFactory.Create(MarioAction.Idle);
                            PreviousActionState = ActionState;
                            ActionState = MarioIdle.Instance;
                            EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
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
                    EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
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
                EventManager.Instance.TriggerMarioActionStateChangedEvent(this);
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