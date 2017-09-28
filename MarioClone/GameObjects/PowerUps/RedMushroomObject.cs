using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MarioClone.GameObjects
{
	public class RedMushroomObject : IGameObject, IMoveable
	{
        public Vector2 Position { get; protected set; }
		public HitBox BoundingBox { get; protected set; }

		public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; protected set; }

        public RedMushroomObject(ISprite sprite, Vector2 position)
        {
            Sprite = sprite;
            Velocity = new Vector2(0, 0);
            Position = position;
            Visible = true;
			BoundingBox = new HitBox(0, 0);
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
				Texture2D dummyTexture = new Texture2D(MarioCloneGame.ReturnGraphicsDevice.GraphicsDevice, 1, 1);
			}
        }

		public bool Update(GameTime gameTime)
        {
			BoundingBox.UpdateHitBox(Position, Sprite);
			return false;
        }
    }
}
