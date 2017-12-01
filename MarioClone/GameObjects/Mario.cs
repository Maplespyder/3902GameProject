using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.Factories;
using MarioClone.Projectiles;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioClone.GameObjects
{
    public class Mario : AbstractGameObject
    {
        public const float GravityAcceleration = .4f;

        public const float HorizontalMovementSpeed = 6f;
        public const float VerticalMovementSpeed = 15f;
        private bool bouncing = false;
        private List<FireBall> FireBalls = new List<FireBall>();
        private List<FireBall> RemovedFireBalls = new List<FireBall>();

        private int poleBottom;
        private int poleTop;
        private int increment;

        /// <summary>
        /// Do not instantiate Mario more than once. We have to make Mario before
        /// things that reference him use him, because I can't null check this getter.
        /// If you aren't sure what you're doing comes after Mario's creation, then
        /// null check the return on instance.
        /// </summary>

        public MarioStateMachine StateMachine { get; set; }

        public MarioActionState ActionState
        {
            get { return StateMachine.CurrentActionState; }
        }

        public MarioActionState PreviousActionState
        {
            get { return StateMachine.PreviousActionState; }
        }

        public MarioPowerupState PowerupState
        {
            get { return StateMachine.CurrentPowerupState; }
        }

        public MarioPowerupState PreviousPowerupState
        {
            get { return StateMachine.PreviousPowerupState; }
        }

        public MarioSpriteFactory SpriteFactory { get; set; }
        public Color PlayerTint { get; set; }

        public FireballPool _FireBallPool { get; set; }

        public int BounceCount { get; set; }
        public bool Gravity { get; set; }

        public int Spawn { get; set; }
        public List<Vector2> Spawns { get; set; }
        public Vector2 ActiveSpawn { get; set; }

        public bool HasAirDash { get; set; }
        public bool IsGroundDash { get; set; }
        
        public int Lives { get; set; }
        public int CoinCount { get; set; }
        public int Score
        {
            get;
            internal set;
        }
        public int Time { get; internal set; }
        public bool Winner { get; set; }
        public bool LevelCompleted { get; set; }

        public int height { get; set; }
        public int poleHeight { get; private set; }


        //passing null sprite because mario's states control his sprite
        public Mario(Vector2 position) : base(null, position, Color.Yellow)
        {
            Spawns = new List<Vector2>();
            Spawns.Add(new Vector2(position.X, position.Y));
            ActiveSpawn = Spawns[0];

            HasAirDash = true;

            Orientation = Facing.Right;
            Gravity = true;
            BounceCount = 0;
            Lives = 3;
            CoinCount = 0;
            Winner = true;
            LevelCompleted = false;

            StateMachine = new MarioStateMachine(this);
            StateMachine.Begin();
            _FireBallPool = new FireballPool(2);
            
            BoundingBox.UpdateHitBox(position, Sprite);

            EventManager.Instance.RaisePowerupCollectedEvent += ReceivePowerup;
        }

        /// <summary>
        /// should be used when "r" is pressed or the game ends (i.e. mario runs out of lives or wins)
        /// </summary>
        /// <param name="position">initial position</param>
        public void ResetMario(Vector2 position)
        {
            Spawns = new List<Vector2>();
            Spawns.Add(new Vector2(position.X, position.Y));
            ActiveSpawn = Spawns[0];

            HasAirDash = true;

            Winner = true;
            LevelCompleted = false;

            Orientation = Facing.Right;
            Gravity = true;
            BounceCount = 0;
            Lives = 3;
            CoinCount = 0;

            ResetToCheckpoint();
        }

        public void MoveLeft()
        {
            ActionState.Walk(Facing.Left);
        }

        /// <summary>
        /// should be called when resetting mario to a checkup i.e. he dies but still has lives
        /// </summary>
        public void ResetToCheckpoint()
        {
            StateMachine.Reset();
            StateMachine.Begin();
            Position = new Vector2(ActiveSpawn.X, ActiveSpawn.Y);
            if (BoundingBox != null)
            {
                BoundingBox.UpdateHitBox(Position, Sprite);
            }
        }

        public void MoveRight()
        {
            ActionState.Walk(Facing.Right);
        }

        public void Jump()
        {
            ActionState.Jump();
        }

        public void Crouch()
        {
            ActionState.Crouch();
        }

        public void ReleaseCrouch()
        {
            ActionState.ReleaseCrouch();
        }

        public void ReleaseMoveLeft()
        {
            ActionState.ReleaseWalk(Facing.Left);
        }

        public void ReleaseMoveRight()
        {
            ActionState.ReleaseWalk(Facing.Right);
        }

        public void BecomeWarp()
        {
            ActionState.Warp();
        }

        public void Dash()
        {
            ActionState.Dash();
        }

		public void FireBall()
		{
			if (PowerupState is MarioFire2)
			{
				_FireBallPool.GetAndRelease(this);
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
        
        private void ManageFlagPoleCoint(AbstractGameObject gameObject, Side side)
        {
            if (gameObject is Flagpole && side.Equals(Side.Right))
            {
                poleBottom = gameObject.BoundingBox.Dimensions.Bottom;
                poleTop = gameObject.BoundingBox.Dimensions.Top;
                poleHeight = poleBottom - poleTop;

                increment = poleHeight / 5;
                
                if (Position.Y == poleTop)
                { 
                    Lives++;
                }
                else if (Position.Y > poleTop && Position.Y <= poleTop + increment)
                {
                    height = 4000;
                }
                else if (Position.Y > poleTop + increment && Position.Y <= poleTop + (increment + increment))
                {
                    height = 2000;
                }
                else if ((Position.Y  > (poleTop + (increment + increment))) && (Position.Y <= poleTop + (increment + increment + increment)))
                {
                    height = 800;
                }
                else if (Position.Y <= poleBottom - increment && Position.Y > poleTop + (increment + increment + increment))
                {
                    height = 400;
                }
                else if (Position.Y <= poleBottom + 5 && Position.Y > poleBottom - increment)
                {
                    height = 100;
                }
                
                EventManager.Instance.TriggerPlayerHitPoleEvent(height, this);
            }
        }


        private void ManageBouncing(AbstractGameObject gameObject, Side side)
        {
            if (gameObject is AbstractEnemy && side.Equals(Side.Bottom))
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
            //TODO move this into states?
            if (!ReferenceEquals(e.Collector, this))
            {
                return;
            }

            if (e.Sender is CoinObject)
            {
                CoinCount++;
                if (CoinCount >= 100)
                {
                    CoinCount = 0;
                    Lives++;
                }
            }
            else if (e.Sender is GreenMushroomObject)
            {
                Lives++;
            }
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            ManageBouncing(gameObject, side);
            ManageFlagPoleCoint(gameObject, side);

            if ((((gameObject is HiddenBrickObject && side != Side.Top && !gameObject.Visible)
                || (gameObject is HiddenBrickObject && side == Side.Top && !gameObject.Visible && (ActionState is MarioFall2)))
                || gameObject is CoinObject || gameObject is GreenMushroomObject))
            {
                return false;
            }

            bool retVal1 = PowerupState.CollisionResponse(gameObject, side, gameTime);
            bool retVal2 = ActionState.CollisionResponse(gameObject, side, gameTime);

            return retVal1 || retVal2;
        }

        public override void FixClipping(Vector2 correction, AbstractGameObject obj1, AbstractGameObject obj2)
        {
            //TODO move this into states? poweurp
            if (!(obj1 is AbstractEnemy) || (obj1 is PiranhaObject))
            {
                Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
                BoundingBox.UpdateHitBox(Position, Sprite);
            }
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            var newSpawns = new List<Vector2>();
            foreach (var spawn in Spawns)
            {
                if (spawn.X > Position.X)
                {
                    newSpawns.Add(spawn);
                }
                else
                {
                    ActiveSpawn = new Vector2(spawn.X, spawn.Y);
                }
            }
            Spawns.Clear();
            Spawns.AddRange(newSpawns);

            Position = new Vector2(Position.X + Velocity.X * percent, Position.Y + Velocity.Y * percent);
            ActionState.UpdateHitBox();

            if (BoundingBox.Dimensions.Bottom >= MarioCloneGame.LevelAreas[LevelArea].Bottom)
            {
                if(!(PowerupState is MarioDead2))
                {
                    BecomeDead();
                }
            }
            else
            {
                if (Gravity)
                {
                    Velocity = new Vector2(Velocity.X, Velocity.Y + GravityAcceleration * percent);
                }
                Gravity = true;
            }

            //TODO fix update to be inside the states or smth, or give mario a BecomeFall() method
            if (!(ActionState is MarioFall2 || ActionState is MarioDash) && Velocity.Y > 1.5)
            {
                StateMachine.TransitionFall();
            }

            if (IsGroundDash)
            {
                SpriteTint = Color.Blue;
            }
            else
            {
                SpriteTint = Color.White;
            }

            StateMachine.UpdateDash();

            PowerupState.Update(gameTime);    
            _FireBallPool.Update(gameTime);
			return base.Update(gameTime, percent);    
        }

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			base.Draw(spriteBatch, gameTime);
		}
	}
}