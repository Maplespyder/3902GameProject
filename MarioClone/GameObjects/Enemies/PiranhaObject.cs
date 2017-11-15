using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using MarioClone.States;
using MarioClone.Collision;
using MarioClone.States.EnemyStates.Powerup;
using MarioClone.EventCenter;

namespace MarioClone.GameObjects
{
    public class PiranhaObject : AbstractEnemy
	{
		public PiranhaObject(ISprite sprite, Vector2 position) : base(sprite, position)
		{
			BoundingBox.UpdateOffSets(-8, -8, -8, -8);
			BoundingBox.UpdateHitBox(Position, Sprite);
			DrawOrder = .52f;
			PowerupState = new PiranhaHide(this);
			Velocity = new Vector2(0, 0);
            PointValue = 500;
        }

        public override void FixClipping(Vector2 correction, AbstractGameObject obj1, AbstractGameObject obj2)
        {
            //don't delete this
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
		{
			if(gameObject is Mario)
			{
				var mario = (Mario)gameObject;
				if (mario.PowerupState is MarioStar2)
				{
					EventManager.Instance.TriggerEnemyDefeatedEvent(this, (Mario)gameObject);
					PowerupState.BecomeDead();
					return true;
				}
			}else if(gameObject is FireBall)
			{
				PowerupState.BecomeDead();
				EventManager.Instance.TriggerEnemyDefeatedEvent(this, (FireBall)gameObject);
				return true;

			}
			return false;
		}
        
		public override bool Update(GameTime gameTime, float percent)
		{
			bool retVal = PowerupState.Update(gameTime, percent);
            Removed = base.Update(gameTime, percent) || retVal;
			return Removed;
		}
	}
}
