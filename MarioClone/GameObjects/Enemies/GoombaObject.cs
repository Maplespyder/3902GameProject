using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using MarioClone.States;
using MarioClone.Collision;
using MarioClone.EventCenter;
using System;
using MarioClone.Projectiles;

namespace MarioClone.GameObjects
{
    public class GoombaObject : AbstractEnemy 
    {
        FireballPool fireballPool = new FireballPool(1);
        public GoombaObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            Gravity = false;
            PowerupState = new GoombaAlive(this);
            BoundingBox.UpdateOffSets(-8, -8, -20, -1);
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
                else if (side == Side.Bottom)
                {
                    if(((AbstractBlock)gameObject).Bumper != null)
                    {
                        EventManager.Instance.TriggerEnemyDefeatedEvent(this, ((AbstractBlock)gameObject).Bumper);
                        PowerupState.BecomeDead();
                    }
                    Velocity = new Vector2(Velocity.X, 0);
                }

                return true;
			}
			else if (gameObject is FireBall)
			{

                var fireball = (FireBall)gameObject;
                if (fireball.Owner is Mario)
                {
                    EventManager.Instance.TriggerEnemyDefeatedEvent(this, (Mario)fireball.Owner);
                    PowerupState.BecomeDead();
                    return true;
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
			if(!(PowerupState is GoombaDead))
			{
				Gravity = true;
                if (((Position.X > MarioCloneGame.Player1.Position.X && Orientation is Facing.Left) ||
                    (Position.X < MarioCloneGame.Player1.Position.X && Orientation is Facing.Right)) &&
                    (Math.Abs(MarioCloneGame.Player1.Position.X - Position.X) < 600 && Math.Abs(MarioCloneGame.Player1.Position.X - Position.X) > 100  
                    && Math.Abs(MarioCloneGame.Player1.Position.Y - Position.Y) < 100))
                {
                    fireballPool.GetAndRelease(this);
                }
                if (((Position.X > MarioCloneGame.Player2.Position.X && Orientation is Facing.Left) ||
                    (Position.X < MarioCloneGame.Player2.Position.X && Orientation is Facing.Right)) &&
                    (Math.Abs(MarioCloneGame.Player2.Position.X - Position.X) < 600 && Math.Abs(MarioCloneGame.Player1.Position.X - Position.X) > 100
                    && Math.Abs(MarioCloneGame.Player2.Position.Y - Position.Y) < 100))
                {
                    fireballPool.GetAndRelease(this);
                }

            }
         
            fireballPool.Update(gameTime);
            return retval;
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
