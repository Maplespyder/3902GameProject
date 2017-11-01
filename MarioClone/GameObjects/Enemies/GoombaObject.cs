using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Factories;
using MarioClone.States;
using MarioClone.Collision;
using static MarioClone.Collision.GameGrid;

namespace MarioClone.GameObjects
{
    public class GoombaObject : AbstractEnemy 
    {
        public GoombaObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            Gravity = false;
            PowerupState = new GoombaAlive(this);
            BoundingBox.UpdateOffSets(-8, -8, -8, -8);
            BoundingBox.UpdateHitBox(Position, Sprite);
			Velocity = new Vector2(-EnemyHorizontalMovementSpeed, 0);

		}

		public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (side == Side.Bottom)
            {
                Gravity = false;
            }

            if (gameObject is Mario)
            {
                if (side.Equals(Side.Top))
                {
                    PowerupState.BecomeDead();
                    TimeDead = 0;
                    return true;
                }
            }
			else if(gameObject is AbstractBlock)
			{                
                if (side == Side.Left)
				{
					Velocity = new Vector2(EnemyHorizontalMovementSpeed, Velocity.Y);
				}else if(side == Side.Right)
				{
					Velocity = new Vector2(-EnemyHorizontalMovementSpeed, Velocity.Y);
				}
                else if (side == Side.Bottom || side == Side.Top)
                {
                    Velocity = new Vector2(Velocity.X, 0);
                }
			}
            return false;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            if (Gravity)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + Mario.GravityAcceleration);
            }

            Gravity = true;
			Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
			bool retVal = PowerupState.Update(gameTime, percent);
            base.Update(gameTime, percent);
            return retVal;
        }
    }
}
