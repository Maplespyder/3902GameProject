using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.Factories;
using MarioClone.GameObjects.Bricks;
using MarioClone.GameObjects;
using MarioClone.States;
using Microsoft.Xna.Framework;
using MarioClone.GameObjects.PowerUps;

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

        public int height { get; set; }
        public int poleHeight { get; private set; }

        private int poleBottom;
        private int poleTop;
        private int increment;

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

            EventManager.Instance.RaisePowerupCollectedEvent += ReceivePowerup;
             
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
            Lives--;
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
		public void BecomeStar()
		{
			PowerupState.BecomeStar();
			EventManager.Instance.TriggerMarioPowerupStateChangedEvent(this);
		}

		private void TakeDamage()
        {
            PowerupState.TakeDamage();
            EventManager.Instance.TriggerMarioPowerupStateChangedEvent(this);
        }

        private void BecomeInvincible()
        {
            PowerupState.BecomeInvincible();
            EventManager.Instance.TriggerMarioPowerupStateChangedEvent(this);
        }

        private void ManageFlagPoleCoint(AbstractGameObject gameObject, Side side)
        {
            if (gameObject is Flagpole && side.Equals(Side.Right))
                {
                poleBottom = gameObject.BoundingBox.Dimensions.Bottom;
                poleTop = gameObject.BoundingBox.Dimensions.Top;
                poleHeight = poleTop - poleBottom;

                increment = poleHeight / 5;

                if (Position.Y == poleHeight)
                { 
                    Lives++;
                }
                else if (Position.Y >= poleHeight - increment && Position.Y < poleHeight)
                {
                    height = 4000;
                }
                else if (Position.Y < poleHeight - increment && Position.Y >= poleHeight - (increment - increment))
                {
                    height = 2000;
                }
                else if ((Position.Y < (poleHeight - (increment - increment))) && (Position.Y >= poleHeight - (increment - increment - increment)))
                {
                    height = 800;
                }
                else if (Position.Y >= poleBottom + increment && Position.Y < poleHeight - (increment - increment - increment))
                {
                    height = 400;
                }
                else if (Position.Y >= poleBottom && Position.Y < poleBottom + increment)
                {
                    height = 100;
                }

                EventManager.Instance.TriggerPlayerHitPoleEvent(height, this);
            }
            
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
        
        /// <summary>
        /// This method is intended for when Mario receives things that change a meta-game state, i.e. he gains a life or a coin. This is
        /// not meant for the updating of his states, that should happen in sync with Mario's update/collision response. This is less time
        /// dependent, and therefore easier to just have this happen as a response to a notification from a powerup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceivePowerup(object sender, PowerupCollectedEventArgs e)
        {
            if(!ReferenceEquals(e.Collector, this))
            {
                return;
            }

            if(e.Sender is CoinObject)
            {
                CoinCount++;
                if(CoinCount >= 100)
                {
                    CoinCount = 0;
                    Lives++;
                }
            }
            else if(e.Sender is GreenMushroomObject)
            {
                Lives++;
            }
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            ManageBouncing(gameObject, side);

            if ((gameObject is AbstractEnemy) && (side.Equals(Side.Top) || side.Equals(Side.Left) || side.Equals(Side.Right)))
            {
                
                BecomeInvincible();

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
			else if(gameObject is StarmanObject)
			{
				BecomeStar();
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

			if(PowerupState is MarioStar)
			{
				PowerupState.Update(gameTime);
			}
            return base.Update(gameTime, percent);    
        }
    }
}