using MarioClone.Collision;
using MarioClone.GameObjects.Other;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class FireBall : AbstractProjectileObject
	{
		public bool Gravity { get; set; }

        private int BounceCount = 0;
		private int MaxBounce = 6;
		public FireBall(ISprite sprite, AbstractGameObject player, Vector2 position) : base(sprite, player, position)
		{
			Gravity = true;
			if(Owner.Orientation == Facing.Right)
			{
				Velocity = new Vector2(5f, 0);
			}
			else
			{
				Velocity = new Vector2(-5f, 0);
			}
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
            }else if(gameObject is AbstractEnemy && Owner is Mario)
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
				retval = true;	
			}
			Gravity = true;
			return retval;
		}

	}
}
