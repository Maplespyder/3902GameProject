using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using MarioClone.States;
using MarioClone.Collision;
using MarioClone.EventCenter;

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
            PointValue = 200;
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
                    EventManager.Instance.TriggerEnemyDefeatedEvent(this, (Mario)gameObject);
                    PowerupState.BecomeDead();
                    return true;
                }
				var mario = (Mario)gameObject;
				if (mario.PowerupState is MarioStar)
				{
					EventManager.Instance.TriggerEnemyDefeatedEvent(this, (Mario)gameObject);
					PowerupState.BecomeDead();
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
            bool retval = base.Update(gameTime, percent);

            if (Gravity)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + Mario.GravityAcceleration * percent);
            }
            Gravity = true;

            return retval;
        }
    }
}
