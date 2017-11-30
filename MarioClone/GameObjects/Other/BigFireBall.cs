using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;


namespace MarioClone.GameObjects.Other
{
	public class BigFireBall : AbstractGameObject
	{
		public bool Destroyed { get; set; }
		public AbstractGameObject Owner { get; set; }
		public int CoolDown { get; set; }

		public BigFireBall(ISprite sprite, AbstractGameObject player, Vector2 position) : base(sprite, position, Color.Red)
		{
			Owner = player;
			if (Owner.Orientation == Facing.Right)
			{
				Velocity = new Vector2(8f, 0);
				Orientation = Facing.Right;
			}
			else
			{
				Velocity = new Vector2(-8f, 0);
				Orientation = Facing.Left;
			}
			Destroyed = false;
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

		public override void FixClipping(Vector2 correction, AbstractGameObject obj1, AbstractGameObject obj2)
		{
			if ((obj1 is AbstractBlock && obj1.Visible) || obj1 is Mario)
			{
				Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
				BoundingBox.UpdateHitBox(Position, Sprite);

			}
			else if ((obj2 is AbstractBlock && obj2.Visible) || obj2 is Mario)
			{
				Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
				BoundingBox.UpdateHitBox(Position, Sprite);
			}
		}
	}
}
