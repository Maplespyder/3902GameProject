using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using MarioClone.States;
using MarioClone.Collision;
using MarioClone.EventCenter; 
using MarioClone.Projectiles;

using MarioClone.States.EnemyStates.Powerup;

namespace MarioClone.GameObjects
{
    public class BowserObject: AbstractEnemy
    {
        public BigFireBallPool bigFireballPool = new BigFireBallPool(3);
        public BowserAction PreviousActionState { get; set; }

        public static int MaxTimeWalk { get { return 2000; } }
        public static int MaxTimeIdle { get { return 1500; } }
        public static int MaxTimeFire { get { return 1000; } }
		public static float BowserMovementSpeed { get { return 2f; } }

		public int TimeIdle { get; set; }
        public int TimeFire { get; set; }
        public int TimeWalk { get; set; }
        public const float GravityAcceleration = .4f;
        public BowserObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            Gravity = false;
            PowerupStateBowser = new BowserAlive(this);
            ActionStateBowser = new BowserIdle(this);
            BoundingBox.UpdateHitBox(Position, Sprite);
            BoundingBox.UpdateOffSets(-340, -20, -36, -1);
            Velocity = new Vector2(0, 0);
            Orientation = Facing.Left;
            PointValue = 500;
            Hits = 10;
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (side == Side.Bottom)
            {
                Gravity = false;   
            }

            bool retVal1 = PowerupStateBowser.CollisionResponse(gameObject, side, gameTime);
            bool retVal2 = ActionStateBowser.CollisionResponse(gameObject, side, gameTime);
            return retVal1 || retVal2;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            if (BoundingBox.Dimensions.Bottom >= MarioCloneGame.LevelAreas[LevelArea].Bottom)
            {
                if (!(PowerupStateBowser is BowserDead))
                {
                    PowerupStateBowser.BecomeDead();
                    PowerupStateBowser.BecomeDead();
                }
            }
            if (Gravity)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + BowserObject.GravityAcceleration * percent);
            }

			if (!(PowerupStateBowser is BowserDead))
			{
				Gravity = true;
			}

            bool retVal2 = ActionStateBowser.Update(gameTime, percent);
			bool retVal = PowerupStateBowser.Update(gameTime, percent);
			bigFireballPool.Update(gameTime);

			Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y * percent);
			Removed = retVal || retVal2;
            return Removed;
        }
        public override void FixClipping(Vector2 correction, AbstractGameObject obj1, AbstractGameObject obj2)
        {
            if (correction.Y > 0)
            {
                return;
            }
            if (obj1 is AbstractBlock && obj1.Visible)
            {
                Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
                BoundingBox.UpdateHitBox(Position, Sprite);

            }
            else if (obj2 is AbstractBlock && obj2.Visible)
            {
                Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
                BoundingBox.UpdateHitBox(Position, Sprite);
            }
        }
    }
}

