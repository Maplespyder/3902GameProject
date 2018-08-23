using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;


namespace MarioClone.GameObjects.Other
{
	public class BigFireBall : AbstractProjectileObject
	{
		public BigFireBall(ISprite sprite, AbstractGameObject player, Vector2 position) : base(sprite, player, position)
		{
			if (Owner.Orientation == Facing.Right)
			{
				Velocity = new Vector2(8f, 0);
				Orientation = Facing.Right;
                BoundingBox.UpdateOffSets(-66, 0, -5, -5);
			}
			else
			{
				Velocity = new Vector2(-8f, 0);
				Orientation = Facing.Left;
                BoundingBox.UpdateOffSets(0, -66, -5, -5);
            }
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
