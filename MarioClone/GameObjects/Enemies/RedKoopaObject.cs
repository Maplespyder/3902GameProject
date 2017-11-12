using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.GameObjects.Other;
using MarioClone.Sprites;
using MarioClone.States;
using MarioClone.States.EnemyStates;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class RedKoopaObject : AbstractEnemy

    {
        public RedKoopaObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            Gravity = false;
            BoundingBox.UpdateOffSets(-8, -8, -8, -8);
            BoundingBox.UpdateHitBox(Position, Sprite);

            Orientation = Facing.Left;
            PowerupState = new KoopaAlive(this);

            PointValue = 300;
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
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
            else if (gameObject is AbstractBlock)
            {
                if (side == Side.Bottom)
                {
                    Gravity = false;
                    Velocity = new Vector2(Velocity.X, 0);
                }
                else if (side == Side.Left)
                {
                    Velocity = new Vector2(EnemyHorizontalMovementSpeed, Velocity.Y);
                    Orientation = Facing.Right;
                }
                else if (side == Side.Right)
                {
                    Velocity = new Vector2(-EnemyHorizontalMovementSpeed, Velocity.Y);
                    Orientation = Facing.Left;
                }
            }
			else if (gameObject is FireBall)
			{
				PowerupState.BecomeDead();
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
