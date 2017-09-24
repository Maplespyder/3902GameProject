using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MarioClone.GameObjects
{
	public class RedMushroomObject : IGameObject, IMoveable
	{
        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; protected set; }

        public RedMushroomObject(ISprite sprite, Vector2 velocity, Vector2 position)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;
            Visible = true;
        }


        public void Draw(SpriteBatch spriteBatch, float layer, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, layer, gameTime);
            }
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public bool Update(GameTime gameTime)
        {
            return false;
        }
    }
}
