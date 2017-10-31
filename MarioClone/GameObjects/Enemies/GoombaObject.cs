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
            PowerupState = new GoombaAlive(this);
            BoundingBox.UpdateOffSets(-8, -8, -8, -8);
            BoundingBox.UpdateHitBox(Position, Sprite);
			Velocity = new Vector2(-EnemyHorizontalMovementSpeed, Velocity.Y);

		}

		public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
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
			}
            return false;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
			Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
			bool retVal = PowerupState.Update(gameTime, percent);
            base.Update(gameTime, percent);
            return retVal;
        }
    }
}
