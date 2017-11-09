using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects.PowerUps
{
	public class StarmanObject : AbstractPowerup
	{
		public const float GravityAcceleration = 0.4f;
		public bool Gravity { get; set; }

		public StarmanObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green)
		{
			PointValue = 5000;
			DrawOrder = .51f;
			State = new StarmanMovingState(this);
		}

		public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
		{
			if (State is PowerupRevealState)
			{
				return false;
			}

			if (gameObject is Mario)
			{
				isCollided = true;
				EventManager.Instance.TriggerPowerupCollectedEvent(this, (Mario)gameObject);
			}
			else if (gameObject is AbstractBlock && gameObject.Visible)
			{
				if (side == Side.Bottom)
				{
					Velocity = new Vector2(Velocity.X, -15);
				}
				else if (side == Side.Top)
				{
					Velocity = new Vector2(Velocity.X, 15);
				}
				else if (side == Side.Left)
				{
					Velocity = new Vector2(2, Velocity.Y);
				}
				else if (side == Side.Right)
				{
					Velocity = new Vector2(-2, Velocity.Y);
				}
			}
			else
			{
				return false;
			}

			return true;
		}

		public override bool Update(GameTime gameTime, float percent)
		{
			bool retval = base.Update(gameTime, percent);

			if (Gravity && !(State is PowerupRevealState))
			{
				Velocity = new Vector2(Velocity.X, Velocity.Y + GravityAcceleration * percent);
			}
			Gravity = true;

			return retval;
		}

		public override void FixClipping(Vector2 correction, AbstractGameObject obj1, AbstractGameObject obj2)
		{
			if (State is PowerupRevealState)
			{
				return;
			}
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
