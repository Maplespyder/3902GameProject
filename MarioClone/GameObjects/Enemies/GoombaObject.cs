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
			Orientation = Facing.Left;
			PointValue = 200;
		}

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (side == Side.Bottom)
            {
                Gravity = false;
			}

            if (gameObject is Mario && !(((Mario)gameObject).PowerupState is MarioInvincibility2))
            {
                if (side.Equals(Side.Top))
                {
                    EventManager.Instance.TriggerEnemyDefeatedEvent(this, (Mario)gameObject);
                    PowerupState.BecomeDead();
                    return true;
                }
				var mario = (Mario)gameObject;
				if (mario.PowerupState is MarioStar2)
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
					Orientation = Facing.Right;
				}
				else if(side == Side.Right)
				{
					Velocity = new Vector2(-EnemyHorizontalMovementSpeed, Velocity.Y);
					Orientation = Facing.Left;
				}
                else if (side == Side.Bottom || side == Side.Top)
                {
                    Velocity = new Vector2(Velocity.X, 0);
                }
			}
			else if (gameObject is FireBall)
			{
				EventManager.Instance.TriggerEnemyDefeatedEvent(this, ((FireBall)gameObject).Owner);
				PowerupState.BecomeDead();
				return true;
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
			if(!(PowerupState is GoombaDead))
			{
				Gravity = true;
			}
            return retval;
        }
    }
}
