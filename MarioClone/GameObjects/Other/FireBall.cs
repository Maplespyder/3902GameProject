using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class FireBall : AbstractGameObject
	{
		public bool Gravity { get; set; }
		public bool Destroyed { get; set; }
        public AbstractGameObject Owner { get; set; }

        private int BounceCount = 0;
		private int MaxBounce = 8;
		public FireBall(ISprite sprite, AbstractGameObject player, Vector2 position) : base(sprite, position, Color.Yellow)
		{
            Owner = player;
			Gravity = true;
			if(Owner.Orientation == Facing.Right)
			{
				Velocity = new Vector2(5f, 0);
			}
			else
			{
				Velocity = new Vector2(-5f, 0);
			}
			Destroyed = false;
		}

		public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
		{
			if(gameObject is AbstractBlock)
			{
				if (side == Side.Bottom)
				{
					Velocity = new Vector2(Velocity.X, -5);
					BounceCount++;
				}
				else
				{
					//KILL fireball
					Destroyed = true;
					Velocity = Vector2.Zero;
				}
			}else if(gameObject is Mario && !(Owner is Mario))
            {
               
                Destroyed = true;
                Velocity = Vector2.Zero;
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
			Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y * percent);

			if(BounceCount >= MaxBounce || Destroyed)
			{
				Removed = true;
				Destroyed = true;
				//GameGrid.Instance.Remove(this);
				retval = true;	
			}
			Gravity = true;
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
