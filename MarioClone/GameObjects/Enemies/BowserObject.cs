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
        public BigFireBallPool bigFireballPool = new BigFireBallPool(1);
        public BowserAction PreviousActionState { get; set; }

        public static int MaxTimeWalk { get { return 300; } }
        public static int MaxTimeIdle { get { return 300; } }
        public static int MaxTimeFire { get { return 200; } }
        public int TimeIdle { get; set; }
        public int TimeFire { get; set; }
        public int TimeWalk { get; set; }
        public const float GravityAcceleration = .4f;
        public BowserObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            Gravity = false;
            PowerupStateBowser = new BowserAlive(this);
            ActionStateBowser = new BowserIdle(this);
            BoundingBox.UpdateOffSets(-8, -8, -8, -1);
            BoundingBox.UpdateHitBox(Position, Sprite);
            Velocity = new Vector2(-EnemyHorizontalMovementSpeed, 0);
            Orientation = Facing.Right;
            PointValue = 500;
            Hits = 3;
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
            if (Gravity)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + BowserObject.GravityAcceleration * percent);
            }
			if (!(PowerupStateBowser is BowserDead))
			{
				Gravity = true;
			}

            ActionStateBowser.Update(gameTime, percent);
            PowerupStateBowser.Update(gameTime, percent);

            Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y * percent);
            bool retVal = PowerupStateBowser.Update(gameTime, percent);
            Removed = retVal;
            Removed = base.Update(gameTime, percent) || retVal;
            return Removed;
        }
    }
}

