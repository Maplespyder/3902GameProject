using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects.Bricks
{
	public class BrickPieceObject : IGameObject, IDraw, IMoveable
	{
		public Vector2 Position { get; protected set; }

		public Vector2 Velocity { get; }

		public int DrawOrder { get; }

		public bool Visible { get; protected set; }

		public ISprite Sprite { get; protected set; }

		public BrickPieceObject(ISprite sprite, Vector2 velocity, Vector2 position)
		{
			Sprite = sprite;
			Velocity = velocity;
			Position = position;
			Visible = true;
		}

		public void Update(GameTime gameTime)
		{
			Move();

			//Nugget off screen?
			if(Position.Y < MarioCloneGame.GraphicsDevice.PreferredBackBufferHeight || Position.Y > MarioCloneGame.GraphicsDevice.PreferredBackBufferHeight)
			{
				Visible = false;
			}
		}

		public void Move()
		{
			//Movement will also need to be tested and likely refactored later
			Position = new Vector2(Position.X + .1f, Position.Y + 5);
		}

		public void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
		{
			if (Visible)
			{
				Sprite.Draw(spriteBatch, Position, layer, gameTime);
			}
		}
	}
}
