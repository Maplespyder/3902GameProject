using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    public abstract class AbstractBlock : IGameObject, IMoveable
    {

        protected AbstractBlock(ISprite sprite, Vector2 velocity, Vector2 position, int drawOrder)
        {
            Sprite = sprite;
            Velocity = velocity;
            Position = position;
            Visible = true;
            DrawOrder = drawOrder;
        }
        public abstract void Bounce();

        public abstract void Break();

        public abstract void BecomeVisible();

        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; protected set; }

        public int DrawOrder { get; protected set; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; protected set; }

        public abstract void Move();

        public abstract void Draw(SpriteBatch spriteBatch,  GameTime gameTime);

        public abstract bool Update(GameTime gameTime);
    }
}
