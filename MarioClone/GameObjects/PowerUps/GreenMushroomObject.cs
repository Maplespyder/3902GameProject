using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MarioClone.GameObjects
{
	public class GreenMushroomObject : IGameObject, IMoveable
	{
        public Vector2 Position { get; protected set; }
		public Rectangle BoundingBox { get; protected set; }

		public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; protected set; }

		private int offSet = 0;

        public GreenMushroomObject(ISprite sprite, Vector2 position)
        {
            Sprite = sprite;
            Velocity = new Vector2(0, 0);
            Position = position;
            Visible = true;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
				Texture2D dummyTexture = new Texture2D(MarioCloneGame.ReturnGraphicsDevice.GraphicsDevice, 1, 1);
			}
        }

		public void UpdateBoundingBox()
		{
			BoundingBox = new Rectangle((int)Position.X - offSet, (int)Position.Y - offSet, Sprite.SourceRectangle.Width + (2 * offSet),
				Sprite.SourceRectangle.Height + (2 * offSet));
		}

		public bool Update(GameTime gameTime)
        {
			UpdateBoundingBox();
			return false;
        }
    }
}
