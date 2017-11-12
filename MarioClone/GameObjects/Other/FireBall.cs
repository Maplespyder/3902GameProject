using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects.Other
{
	public class FireBall : AbstractGameObject
	{
		public bool Gravity { get; set; }
		public bool Destroyed { get; set; }
		private int BounceCount = 0;
		private int MaxBounce = 5;
		private bool BecomeDestroyed = false;
		public FireBall(ISprite sprite, Vector2 position) : base(sprite, position, Color.Yellow)
		{
			Gravity = true;
			if(Mario.Instance.Orientation == Facing.Right)
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
					BecomeDestroyed = true;
					Velocity = Vector2.Zero;
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
			Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y * percent);

			if(BounceCount >= MaxBounce || BecomeDestroyed)
			{
				Destroyed = true;
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
