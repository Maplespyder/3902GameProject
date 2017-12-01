using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;


namespace MarioClone.GameObjects.Other
{
	public class CannonFireBall : AbstractProjectileObject
	{
		public CannonFireBall(ISprite sprite, AbstractGameObject player, Vector2 position) : base(sprite, player, position)
		{
			Velocity = new Vector2(0, 3f);
            BoundingBox.UpdateOffSets(0, 0, -36, -1);
            BoundingBox.UpdateHitBox(position, Sprite);
		}

		public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
		{
			if (gameObject is AbstractBlock)
			{
				Destroyed = true;
				Velocity = Vector2.Zero;
			}
			else if (gameObject is Mario && !(Owner is Mario))
			{
				Destroyed = true;
				Velocity = Vector2.Zero;
			}
			return false;
		}
		public override bool Update(GameTime gameTime, float percent)
		{
			bool retval = base.Update(gameTime, percent);

			Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
			if (Destroyed)
			{
				Removed = true;
				retval = true;
			}
			return retval;
		}
	}
}
